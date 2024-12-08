using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _maxSpeed = 5;
    [SerializeField] private int _acceleration = 5;
    
    private Vector3 _movement;

    Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        //transform.Translate(_movement * (_speed * Time.deltaTime));
        Vector3 dir = transform.right * _movement.x + transform.forward * _movement.z;
        rb.AddForce(dir * _acceleration * Time.deltaTime);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, _maxSpeed);
    }
    
    public void SetMovement(Vector2 movement)
    {
        _movement.Set(movement.x, 0f, movement.y);
    }
}
