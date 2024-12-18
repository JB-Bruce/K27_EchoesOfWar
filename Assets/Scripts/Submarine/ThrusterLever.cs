using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Lumin;

[RequireComponent(typeof(Outline))]
public class ThrusterLever : MonoBehaviour, IFinishedInteractable, IBreakdownReceiver
{
    [SerializeField] private Transform _max;
    [SerializeField] private Transform _min;
    [SerializeField] private Transform _origin;
        
    [SerializeField] private float _minThrust;
    [SerializeField] private float _maxThrust;
    
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _upperthreshold;
    [SerializeField] private float _lowerthreshold;
        
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

    [SerializeField] PipesBoard _pipesBoard;

    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] Light _light;
    [SerializeField] Color _onColor;
    [SerializeField] Color _offColor;

    private void Awake()
    {
        _transform = transform;

        ResetThrust();

        _outline = GetComponent<Outline>();
        _outline.enabled = true;

        SetThrusterPosition(_thrust);

        ApplyColor(!IsBroken);
    }
    private void Start()
    {
        _outline.enabled = false;
        _pipesBoard.onEnigmaFinished.AddListener(Repair);
        StartCoroutine(((IInteractable)this).DeactivateOutline());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) Break();
        Move();
    }


    private void ApplyColor(bool on)
    {
        Color selectedColor = on ? _onColor : _offColor;

        _meshRenderer.material.color = selectedColor;
        _meshRenderer.material.SetColor("_EmissionColor", selectedColor);
        _light.color = selectedColor;
    }


    private void Move()
    {
        if (MathF.Abs(_movementDirection) < 0.1f)
            return;
        
        _thrust += _movementSpeed * Mathf.Sign(_movementDirection) * Time.deltaTime;
        _thrust = Mathf.Clamp(_thrust, 0, 1);
        

        if (_thrust < _upperthreshold && _thrust > _lowerthreshold)
        {
            _transform.localPosition = _origin.localPosition;
            _transform.localRotation = _origin.localRotation;
        }
        else
        {
            SetThrusterPosition(_thrust);
        }

        /*_thrust += _movementSpeed * Mathf.Sign(_movementDirection) * Time.deltaTime;
        _thrust = Mathf.Clamp(_thrust, -_distMinToOrigin, _distMaxToOrigin);
        _transform.localPosition = new Vector3(0, 0, _thrust);*/
    }

    private void SetThrusterPosition(float thrust)
    {
        _transform.localPosition = Vector3.Lerp(_min.localPosition, _max.localPosition, thrust);
        _transform.rotation = Quaternion.Slerp(_min.localRotation, _max.localRotation, thrust);
    }

    public void SetMovement(float direction)
    {
        _movementDirection = direction;
    }

    public void ResetThrust()
    {
        float dist = (_maxThrust - _minThrust);

        _thrust = -_minThrust / dist;

        SetThrusterPosition(_thrust);
    }


    public float GetRealThrust()
    {
        if (IsBroken || _thrust < _upperthreshold && _thrust > _lowerthreshold) return 0f;

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

    public void Break()
    {
        IsBroken = true;
        _pipesBoard.ResetPuzzle();
        ApplyColor(!IsBroken);
    }

    public void Repair()
    {
        IsBroken = false;
        ApplyColor(!IsBroken);
    }

    public Outline outline => _outline;

    public bool isInteracted { get; set; }

    public string interactableName => "SubControls";



    public bool IsBroken { get; set; } = false;
}