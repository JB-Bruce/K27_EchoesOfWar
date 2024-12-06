using System;
using UnityEngine;

public class Rudder : MonoBehaviour, IInteractable
{
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _minAngle;
    
    private float _angle;
    private Transform _transform;
    private Vector3 _rotation;
    private float _rotationDirection;

    private void Awake()
    {
        _transform = transform;
        _rotation = _transform.localEulerAngles;
    }

    private void Update()
    {
        Rotate();
    }

    public void SetRotation(float direction)
    {
        _rotationDirection = direction;
    }

    private void Rotate()
    {
        _angle += _rotationDirection;
        _angle = Mathf.Clamp(_angle, _minAngle, _maxAngle);
        _rotation.x = _angle;
        _transform.localEulerAngles = _rotation;
    }
    
    public float Angle => _angle;
    
    public void Interact() { }

    public bool DoesNeedToStopPlayerMovement { get; } = true;

    public Outline outline => throw new NotImplementedException();
}
