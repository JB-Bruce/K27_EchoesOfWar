using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    
    private Vector3 _movement;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_movement * (_speed * Time.deltaTime));
    }
    
    public void SetMovement(Vector2 movement)
    {
        _movement.Set(movement.x, 0f, movement.y);
    }
}
