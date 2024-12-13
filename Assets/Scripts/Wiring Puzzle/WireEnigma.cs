using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Outline))]
public class WireEnigma : MonoBehaviour, IFinishedInteractable
{
    [SerializeField] private List<Wire> _wires;
    [SerializeField] private List<Transform> _wiresEnds;
    [SerializeField] private Transform _target;

    private Outline _outline;
    private string _name;

    [SerializeField] bool _doesStopMovements;
    public bool doesStopMovements => _doesStopMovements;

    [SerializeField] bool _doesLockView;
    public bool doesLockView => _doesLockView;

    [SerializeField] bool _canInteractWithOtherInteractablesWhileInteracted;
    public bool canInteractWithOtherInteractablesWhileInteracted => _canInteractWithOtherInteractablesWhileInteracted;
    public bool isInteracted { get; set; }

    private int _wiresConnected = 0;
    public Outline outline => _outline;
    public string interactableName => _name;

    private readonly UnityEvent _onWiresResolved = new();

    void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = true;
        _name = _outline.name;

        StartCoroutine(((IInteractable)this).DeactivateOutline());

        foreach (Wire wire in _wires)
        {
            wire.Linked.AddListener(() => Linked());
        }

        Shuffle();
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

    public void Interact()
    {
        if (isInteracted)
        {
            Uninteract();
            return;
        }

        PlayerController.Instance.SetPlayerBlockingInteractable("Wires", true);
        PlayerController.Instance.SetCameraBlockingInteractables("Wires", true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isInteracted = false;

        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().ResetTarget();
        }
    }

    public UnityEvent OnWiresResolved => _onWiresResolved;
}
