using System;
using UnityEngine;

public class ThrusterLever : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _max;
    [SerializeField] private Transform _min;
    [SerializeField] private Transform _origin;
        
    [SerializeField] private float _minThrust;
    [SerializeField] private float _maxThrust;
    
    [SerializeField] private float _movementSpeed;
        
    private Transform _transform;
    
    private float distMinToOrigin;
    private float distMinToPosition;
    private float distMaxToOrigin;
    private float distMaxToPosition;
    
    private float _positionZ;
    private float _movement;

    private void Awake()
    {
        _transform = transform;
        
        distMinToOrigin   = Vector3.Distance(_min.localPosition, _origin.localPosition);
        distMaxToOrigin   = Vector3.Distance(_max.localPosition, _origin.localPosition);
        distMinToPosition = distMinToOrigin;
        distMaxToPosition = distMaxToOrigin;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (MathF.Abs(_movement) < 0.1f)
            return;
        
        distMinToPosition = Vector3.Distance(_min.localPosition, _transform.localPosition);
        distMaxToPosition = Vector3.Distance(_max.localPosition, _transform.localPosition);

        _positionZ += _movementSpeed * Mathf.Sign(_movement) * Time.deltaTime;
        _positionZ = Mathf.Clamp(_positionZ, -distMinToOrigin, distMaxToOrigin);
        _transform.localPosition = new Vector3(0, 0, _positionZ);
    }

    public void SetMovement(float direction)
    {
        _movement = direction;
    }

    public float GetThrust()
    {
        return distMinToOrigin < distMinToPosition ? 
            Mathf.Lerp(_maxThrust, 0, distMaxToPosition / distMaxToOrigin) : 
            Mathf.Lerp(_minThrust, 0, distMinToPosition / distMinToOrigin);
    }

    public void Interact() { }

    public bool DoesNeedToStopPlayerMovement { get; } = true;
}