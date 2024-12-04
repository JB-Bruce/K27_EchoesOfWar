using UnityEngine;

public class SubmarineBody : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxThrust = 1.0f;
    [SerializeField] private float _minThrust = -0.25f;
    
    [Header("Water")]
    [SerializeField] private float _waterDrag = 2f;
    
    private float _thrustPower = 0f;
    
    private Vector2 _position = Vector2.zero;
    private Vector2 _previousPosition = Vector2.zero;
    private Vector2 _direction = Vector2.up;
    private Vector2 _velocity = Vector2.zero;
    private Vector2 _acceleration = Vector2.zero;
    private Vector2 _drag = Vector2.zero;

    private void Update()
    {
        Move();
    }

    /*public void AddThrust(float amount)
    {
        _thrustPower = Mathf.Clamp(_thrustPower + amount * Time.deltaTime, _minThrust, _maxThrust);
    }*/
    
    public void Rotate(float force)
    {
        float rad = force * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        
        _direction.Set(cos * _direction.x - sin * _direction.y,
                       sin * _direction.x + cos * _direction.y);
        
        transform.forward = _direction;
    }

    private void Move()
    {
        float deltaTime = Time.deltaTime;
        
        _acceleration.Set(_direction.x * _thrustPower * deltaTime,
                          _direction.y * _thrustPower * deltaTime);
        
        _drag.Set(-_velocity.normalized.x * _waterDrag * _velocity.sqrMagnitude,
                  -_velocity.normalized.y * _waterDrag * _velocity.sqrMagnitude);
        
        _velocity.Set(_velocity.x + _acceleration.x * deltaTime + _drag.x * deltaTime,
                      _velocity.y + _acceleration.y * deltaTime + _drag.y * deltaTime);

        float maxSpeed = _maxSpeed / _waterDrag;
        if (_velocity.magnitude > maxSpeed)
        {
            _velocity = _velocity.normalized * maxSpeed;
        }

        _previousPosition.Set(_position.x, _position.y);
        
        _position.Set(_position.x + _velocity.x,
                      _position.y + _velocity.y);
        
        transform.position = new Vector3(_position.x, 0, _position.y);
    }

    public void SetThrust(float thrust)
    {
        _thrustPower = thrust;
    }

    private void OnCollision()
    {
        _acceleration.Set(0, 0);
        _velocity.Set(0, 0);
        _position = _previousPosition;
    }
}
