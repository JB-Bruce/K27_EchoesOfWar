using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeplassement:MonoBehaviour
{
   public int _speed=5;
  public PlayerImput _inputManadjeur;
  
  
  // public void mouve()
  // {
public void mouve()
{
    Vector3 moveInput =_inputManadjeur.evenement .ReadValue<Vector3>();
            transform.Translate(moveInput*5*Time.deltaTime);
  }
}
