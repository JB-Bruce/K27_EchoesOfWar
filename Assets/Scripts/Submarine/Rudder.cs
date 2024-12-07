using System;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Rudder : MonoBehaviour, IInteractable
{
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


    public void SetRotation(float direction)
    {
        transform.localRotation = Quaternion.Euler(0, 0, direction);
        _rotationDirection = direction;
    }

    public float Angle => _angle;
    
    public void Interact() { }

    public bool DoesNeedToStopPlayerMovement { get; } = true;

    public Outline outline => _outline;
}
