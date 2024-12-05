using UnityEngine;

public class Rudder : MonoBehaviour
{
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _minAngle;
    
    private float _angle;
    private Transform _transform;
    private Vector3 _rotation;

    private void Awake()
    {
        _transform = transform;
        _rotation = _transform.localEulerAngles;
    }

    public void Rotate(float amount)
    {
        _angle += amount;
        _angle = Mathf.Clamp(_angle, _minAngle, _maxAngle);
        _rotation.x = _angle;
        _transform.localEulerAngles = _rotation;
    }
    
    public float Angle => _angle;
}
