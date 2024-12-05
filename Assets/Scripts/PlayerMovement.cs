using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement:MonoBehaviour
{
   public int _speed=5;
  public PlayerInput _inputManadjeur;
  
  
  // public void mouve()
  // {
public void Move()
{
    Vector3 moveInput =_inputManadjeur.evenement .ReadValue<Vector3>();
            transform.Translate(moveInput*5*Time.deltaTime);
  }
}
