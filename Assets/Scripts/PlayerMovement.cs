using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement:MonoBehaviour
{
    [SerializeField]private int _speed=5;
    [SerializeField]private PlayerInput _inputManager;
  
  
  // public void move
public void Move()
{
    Vector3 moveInput =_inputManager.evenement .ReadValue<Vector3>();
            transform.Translate(moveInput*_speed*Time.deltaTime);
  }
}
