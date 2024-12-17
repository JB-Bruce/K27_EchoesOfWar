using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

[RequireComponent (typeof(Outline))]
public class WireEnigma : MonoBehaviour, IFinishedInteractable
{
    [Header("Wires")]
    [SerializeField] private List<Wire> _wires;

    [Header("Wires Destinations")]
    [SerializeField] private List<Transform> _wiresEnds;
    [SerializeField] private List<GameObject> _wiresEndsSymboles;

    [Header("Symboles")]
    [SerializeField] private List<Mesh> _meshSymboles;

    [Header("Zoom")]
    [SerializeField] private Transform _target;

    [Header("Low Resolution")]
    public RenderTexture txt2D;

    private Outline _outline;
    private string _name;

    private bool isDragging;
    private Wire _draggedWire;
    public float _distanceRange = 0.01f;
    private int _wiresConnected = 0;
    private readonly UnityEvent _onWiresResolved = new();

    private Camera _camera;
    public GameObject _planeRef;

    [SerializeField] bool _doesStopMovements;
    public bool doesStopMovements => _doesStopMovements;
    [SerializeField] bool _doesLockView;
    public bool doesLockView => _doesLockView;
    [SerializeField] bool _canInteractWithOtherInteractablesWhileInteracted;
    public bool canInteractWithOtherInteractablesWhileInteracted => _canInteractWithOtherInteractablesWhileInteracted;
    public bool isInteracted { get; set; }
    public Outline outline => _outline;
    public string interactableName => _name;
    

    void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = true;
        _name = _outline.name;
        _camera = Camera.main;
        StartCoroutine(((IInteractable)this).DeactivateOutline());

        Shuffle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) { Shuffle(); }

        if (isInteracted)
        {
            var pos = Input.mousePosition;
            pos = new(pos.x * txt2D.width / Screen.width, pos.y * txt2D.height / Screen.height);

            Ray ray = _camera.ScreenPointToRay(pos);
            RaycastHit hitInfos;

            if (Input.GetMouseButton(0) )
            {
                if ( _draggedWire==null && Physics.Raycast(ray, out hitInfos))
                {
                    if (hitInfos.transform.gameObject.TryGetComponent<Wire>(out Wire wire) && !wire.isLinked)
                    {
                        isDragging = true;
                        _draggedWire = wire;
                    }
                    else
                    {
                        isDragging = false;
                    }
                }
            }
            else
            {
                isDragging = false;
            }

            if (!isDragging && _draggedWire != null)
            {
                Vector3 distance = _draggedWire.transform.position - _draggedWire.end.transform.position;
                if (distance.sqrMagnitude <= _distanceRange)
                {
                    _draggedWire.isLinked = true;
                    Linked();
                    MoveWireToPoint(_draggedWire, _draggedWire.end.transform.position);
                }
                else if (_draggedWire != null)
                {
                    ResetWire(_draggedWire);
                }
                _draggedWire = null;
            }
            else
            {
                MoveWire(ray);
            }
        }
    }

    private void Linked()
    {
        _wiresConnected++;

        if (_wiresConnected == _wires.Count)
        {
            _onWiresResolved.Invoke ();
        }
    }

    private void Shuffle()
    {
        List<Transform> listEnds = new List<Transform>();
        List<GameObject> listEndsSymboles = new List<GameObject>();
        List<Mesh> listSymboles = new List<Mesh>();
        foreach (Transform t in _wiresEnds)
        {
            listEnds.Add(t);
        }

        foreach (GameObject g in _wiresEndsSymboles)
        {
            listEndsSymboles.Add(g);
        }

        foreach (Mesh m in _meshSymboles)
        {
            listSymboles.Add(m);
        }

        foreach (var wire in _wires)
        {
            int index = Random.Range(0, listEnds.Count);
            wire.end = listEnds[index];

            listEndsSymboles[index].GetComponent<MeshFilter>().mesh = listSymboles[index];
            wire.symbole.GetComponent<MeshFilter>().mesh = listSymboles[index];

            listEnds.RemoveAt(index);
            listEndsSymboles.RemoveAt(index);
            listSymboles.RemoveAt(index);
        }

        foreach(var wire in _wires)
        {
            wire.isLinked = false;
            ResetWire(wire);
        }

        _wiresConnected = 0;
    }

    private void MoveWire(Ray ray)
    {
        //move
        Vector3 NewPosition;
        if (DrawScript.LinePlaneIntersection(out NewPosition, ray.origin, ray.direction, _planeRef.transform.position, _planeRef.transform.right))
        {
            MoveWireToPoint(_draggedWire, NewPosition);
        }
    }

    private void MoveWireToPoint(Wire wire,  Vector3 position)
    {
        wire.transform.position = position;

        //scale
        float distance = Vector3.Distance(wire.InitPos, wire.transform.position);
        wire.mesh.transform.position = wire.InitPos + (position - wire.InitPos) / 2f;
        wire.mesh.transform.localScale = new Vector3(1, distance / wire.transform.lossyScale.y / 2f, 1);

        //rotation
        Vector3 direction = position - wire.InitPos;
        wire.transform.right = direction;
    }

    private void ResetWire(Wire wire)
    {

         wire.mesh.transform.localScale = Vector3.zero;
         wire.transform.position = wire.InitPos;
         wire.isLinked = false;
    }

    public void Interact()
    {
        if (isInteracted)
        {
            Uninteract();
            return;
        }

        PlayerController.Instance.SetPlayerBlockingInteractable("Wires", true);
        PlayerController.Instance.SetCameraBlockingInteractables("Wires", true);
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        isInteracted = true;

        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().SetTarget(_target);
        }
    }

    public void Uninteract()
    {
        PlayerController.Instance.SetPlayerBlockingInteractable("Wires", false);
        PlayerController.Instance.SetCameraBlockingInteractables("Wires", false);
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        isInteracted = false;

        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().ResetTarget();
        }
    }

    public UnityEvent OnWiresResolved => _onWiresResolved;
}
