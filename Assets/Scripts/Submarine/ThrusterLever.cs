using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ThrusterLever : MonoBehaviour, IFinishedInteractable
{
    [SerializeField] private Transform _max;
    [SerializeField] private Transform _min;
    [SerializeField] private Transform _origin;
        
    [SerializeField] private float _minThrust;
    [SerializeField] private float _maxThrust;
    
    [SerializeField] private float _movementSpeed;
        
    private Transform _transform;
    
    private float _thrust = 0f;
    private float _movementDirection;

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

        ResetThrust();

        _outline = GetComponent<Outline>();
        _outline.enabled = true;

        SetThrusterPosition();
    }
    private void Start()
    {
        StartCoroutine(((IInteractable)this).DeactivateOutline());
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (MathF.Abs(_movementDirection) < 0.1f)
            return;
        
        _thrust += _movementSpeed * Mathf.Sign(_movementDirection) * Time.deltaTime;
        _thrust = Mathf.Clamp(_thrust, 0, 1);

        SetThrusterPosition();

        /*_thrust += _movementSpeed * Mathf.Sign(_movementDirection) * Time.deltaTime;
        _thrust = Mathf.Clamp(_thrust, -_distMinToOrigin, _distMaxToOrigin);
        _transform.localPosition = new Vector3(0, 0, _thrust);*/
    }

    private void SetThrusterPosition()
    {
        _transform.localPosition = Vector3.Lerp(_min.localPosition, _max.localPosition, _thrust);
        _transform.rotation = Quaternion.Slerp(_min.localRotation, _max.localRotation, _thrust);
    }

    public void SetMovement(float direction)
    {
        _movementDirection = direction;
    }

    public void ResetThrust()
    {
        float dist = (_maxThrust - _minThrust);

        _thrust = -_minThrust / dist;

        SetThrusterPosition();
    }


    public float GetRealThrust()
    {
        return Mathf.Lerp(_minThrust, _maxThrust, _thrust);
    }

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