using UnityEngine;

public class SubmarineBody : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotation;
    [SerializeField] private float _accelerationForce;
    
    [Header("Water")]
    [SerializeField] private float _waterDrag = 2f;
    [SerializeField] private float _waterRotationDrag = 2f;
    
    private float _thrustPower = 0f;
    private float _rotationPower = 0f;
    private float _angle = 0f;
    
    private float _actualRotation = 0f;
    
    [SerializeField] private Vector2 _position;
    private Vector2 _previousPosition = Vector2.zero;
    private Vector2 _direction = Vector2.up;
    private Vector2 _velocity = Vector2.zero;
    private Vector2 _acceleration = Vector2.zero;
    private Vector2 _drag = Vector2.zero;

    [SerializeField] WaterStream waterStream;
    [SerializeField] Transform arrow;


    public void Tick()
    {
        Move();
        AddRotation(_actualRotation);
        Rotate();

        arrow.localRotation = Quaternion.Euler(0, 0, waterStream.angle + _angle);
    }

    private void Rotate()
    {
        _angle += _rotationPower * _rotationSpeed * Time.deltaTime;
        _angle = _angle > 360 ? 0 : _angle < 0 ? 360 + _angle : _angle;
        float rad = -_angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        
        _direction = new Vector2(sin, cos).normalized;
    
        if (_rotationPower > 0)
            _rotationPower = Mathf.Max(_rotationPower - _waterRotationDrag * Time.deltaTime * _rotationPower, 0);
        if (_rotationPower < 0)
            _rotationPower = Mathf.Min(_rotationPower - _waterRotationDrag * Time.deltaTime * _rotationPower, 0);
    }

    private void Move()
    {
        float deltaTime = Time.deltaTime;
        
        _acceleration.Set(_direction.x * _thrustPower * deltaTime,
                          _direction.y * _thrustPower * deltaTime);
        
        _drag.Set(-_velocity.normalized.x * _waterDrag * _velocity.sqrMagnitude,
                  -_velocity.normalized.y * _waterDrag * _velocity.sqrMagnitude);


        (Vector2 dir, float force) s = waterStream.GetStream();

        _velocity.Set(_velocity.x + _acceleration.x * _accelerationForce + _drag.x * deltaTime,
                      _velocity.y + _acceleration.y * _accelerationForce + _drag.y * deltaTime);

        _velocity += s.dir * s.force * Time.deltaTime;


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

    public void AddRotation(float rotation)
    {
        _rotationPower = Mathf.Clamp(_rotationPower + rotation * _rotationSpeed * Time.deltaTime, -_maxRotation, _maxRotation);
    }

    public void SetRotation(float rotation)
    {
        _actualRotation = rotation;
    }

    public void SetPosition(Vector2 position)
    {
        _position = position;
    }

    public Vector2 Position => _position;
    public Vector2 Direction => _direction;
    
    public Vector2 Velocity => _velocity;

    public float Angle => _angle;

    public void OnCollision()
    {
        _acceleration.Set(0, 0);
        _velocity.Set(0, 0);
        _position = _previousPosition;
    }
}
