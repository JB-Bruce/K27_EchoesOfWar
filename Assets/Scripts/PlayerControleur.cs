using UnityEngine;

public class PlayerControleur : MonoBehaviour
{
    public PlayerMovement _cameraController2;
   public PlayerInput _inputManager;
   
  void Update()
  {
     
    //  Debug.Log(_inputManadjeur.evenement);
     
        if (_inputManager.action )
        {
            _cameraController2.Move();
        }
  }
}
