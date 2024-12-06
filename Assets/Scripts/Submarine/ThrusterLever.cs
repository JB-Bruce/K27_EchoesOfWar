using System;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ThrusterLever : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _max;
    [SerializeField] private Transform _min;
    [SerializeField] private Transform _origin;
        
    [SerializeField] private float _minThrust;
    [SerializeField] private float _maxThrust;
    
    [SerializeField] private float _movementSpeed;
        
    private Transform _transform;
    
    private float _distMinToOrigin;
    private float _distMinToPosition;
    private float _distMaxToOrigin;
    private float _distMaxToPosition;
    
    private float _thrust;
    private float _movementDirection;

    private Outline _outline;

    private void Awake()
    {
        _transform = transform;
        
        _distMinToOrigin   = Vector3.Distance(_min.localPosition, _origin.localPosition);
        _distMaxToOrigin   = Vector3.Distance(_max.localPosition, _origin.localPosition);
        _distMinToPosition = Vector3.Distance(_min.localPosition, _transform.localPosition);
        _distMaxToPosition = Vector3.Distance(_max.localPosition, _transform.localPosition);
        
        float distMinToMax = Vector3.Distance(_min.localPosition, _max.localPosition);

        _thrust = Mathf.Lerp(0, 1, _distMinToOrigin / distMinToMax);

        _outline = GetComponent<Outline>();
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
        _transform.localPosition = Vector3.Lerp(_min.localPosition, _max.localPosition, _thrust);
        
        _distMinToPosition = Vector3.Distance(_min.localPosition, _transform.localPosition);
        _distMaxToPosition = Vector3.Distance(_max.localPosition, _transform.localPosition);

        /*_thrust += _movementSpeed * Mathf.Sign(_movementDirection) * Time.deltaTime;
        _thrust = Mathf.Clamp(_thrust, -_distMinToOrigin, _distMaxToOrigin);
        _transform.localPosition = new Vector3(0, 0, _thrust);*/
    }

    public void SetMovement(float direction)
    {
        _movementDirection = direction;
    }

    public float GetThrust()
    {
        return _distMinToOrigin < _distMinToPosition ? 
            Mathf.Lerp(_maxThrust, 0, _distMaxToPosition / _distMaxToOrigin) : 
            Mathf.Lerp(_minThrust, 0, _distMinToPosition / _distMinToOrigin);
    }

    public void Interact() { }

    public bool DoesNeedToStopPlayerMovement { get; } = true;

    public Outline outline => _outline;
}