using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent (typeof(Outline))]
public class WireEnigma : MonoBehaviour, IFinishedInteractable
{
    [SerializeField] private List<Wire> _wires;
    [SerializeField] private List<Transform> _wiresEnds;
    [SerializeField] private Transform _target;

    private Outline _outline;
    private string _name;

    private int _wiresConnected = 0;
    private readonly UnityEvent _onWiresResolved = new();

    private Camera _camera;
    private float _cameraZDistance;

    [SerializeField] bool _doesStopMovements;
    public bool doesStopMovements => _doesStopMovements;
    [SerializeField] bool _doesLockView;
    public bool doesLockView => _doesLockView;
    [SerializeField] bool _canInteractWithOtherInteractablesWhileInteracted;
    public bool canInteractWithOtherInteractablesWhileInteracted => _canInteractWithOtherInteractablesWhileInteracted;
    public bool isInteracted { get; set; }
    public Outline outline => _outline;
    public string interactableName => _name;

    public RenderTexture txt2D;
    public bool isDragging;
    public Wire _draggedWire;
    public GameObject _planeRef;
    public float _distanceRange = 0.05f;

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
        if (isInteracted)
        {
            var pos = Input.mousePosition;
            pos = new(pos.x * txt2D.width / Screen.width, pos.y * txt2D.height / Screen.height);

            Ray ray = _camera.ScreenPointToRay(pos);
            RaycastHit hitInfos;

            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hitInfos))
                {
                    if (hitInfos.transform.gameObject.TryGetComponent<Wire>(out Wire wire) && !wire.IsLinked)
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
                Vector3 distance = _draggedWire.Transform.position - _draggedWire.End.transform.position;
                if (distance.sqrMagnitude <= _distanceRange)
                {
                    _draggedWire.Transform.position = _draggedWire.End.transform.position;
                    _draggedWire.IsLinked = true;
                    Linked();
                }
                else
                {
                    ResetWire(_draggedWire);
                    _draggedWire = null;
                }
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
        List<Transform> list = new List<Transform>();
        foreach (Transform t in _wiresEnds)
        {
            list.Add(t);
        }

        foreach (var wire in _wires)
        {
            int index = Random.Range(0, list.Count);
            wire.End = list[index];
            list.RemoveAt(index);
        }
    }

    private void MoveWire(Ray ray)
    {
        //move
        Vector3 NewPosition;
        if (DrawScript.LinePlaneIntersection(out NewPosition, ray.origin, ray.direction, _planeRef.transform.position, _planeRef.transform.right))
        {
            _draggedWire.Transform.position = NewPosition;
        }

        //scale
        float distance = Vector3.Distance(_draggedWire.Transform.position,_draggedWire.InitPos);
        print(_draggedWire.InitPos + " --- " + _draggedWire.Transform.position + " = " + distance * 0.01f);
        _draggedWire.Transform.localScale = new Vector3(_draggedWire.InitScale.x, _draggedWire.InitScale.y, distance * 0.01f);

        //rotation
        Vector3 direction = NewPosition - _draggedWire.InitPos;
        _draggedWire.Transform.right = direction;


    }

    private void ResetWire(Wire wire)
    {
        wire.Transform.localPosition = wire.InitPos;
        wire.Transform.right = Vector3.zero;
        wire.Transform.localScale = wire.InitScale;
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
