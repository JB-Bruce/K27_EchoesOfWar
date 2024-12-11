using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Rudder : MonoBehaviour, IFinishedInteractable
{
    private float _angle;
    private Transform _transform;
    private Vector3 _rotation;
    private float _rotationDirection;
    
    private Outline _outline;

    [SerializeField] bool _doesStopMovements;
    public bool doesStopMovements => _doesStopMovements;

    [SerializeField] bool _doesLockView;
    public bool doesLockView => _doesLockView;

    [SerializeField] bool _canInteractWithOtherInteractablesWhileInteracted;
    public bool canInteractWithOtherInteractablesWhileInteracted => _canInteractWithOtherInteractablesWhileInteracted;

    private void Awake()
    {
        _transform = transform;
        _rotation = _transform.localEulerAngles;
        
        _outline = GetComponent<Outline>();
        _outline.enabled = true;
    }

    private void Start()
    {
        StartCoroutine(((IInteractable)this).DeactivateOutline());
    }

    public void SetRotation(float direction)
    {
        transform.localRotation = Quaternion.Euler(0, 0, direction);
        _rotationDirection = direction;
    }

    public float Angle => _angle;
    
    public void Interact() 
    {
        if (isInteracted)
        {
            Uninteract();
            return;
        }

        PlayerController.Instance.SetPlayerBlockingInteractable(interactableName, true);
        PlayerController.Instance.SwitchPlayerAndSubmarineControls(false);

        isInteracted = true;
    }

    public void Uninteract()
    {
        PlayerController.Instance.SetPlayerBlockingInteractable(interactableName, false);
        PlayerController.Instance.SwitchPlayerAndSubmarineControls(true);

        isInteracted = false;
    }

    public Outline outline => _outline;

    public bool isInteracted { get; set; }

    public string interactableName => "SubControls";
}
