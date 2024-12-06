using System;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Rudder : MonoBehaviour, IInteractable
{
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _minAngle;
    
    private float _angle;
    private Transform _transform;
    private Vector3 _rotation;
    private float _rotationDirection;
    
    private Outline _outline;

    private void Awake()
    {
        _transform = transform;
        _rotation = _transform.localEulerAngles;
        
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
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

    public Outline outline => _outline;
}
