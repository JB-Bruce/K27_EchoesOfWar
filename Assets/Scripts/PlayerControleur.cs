using UnityEngine;

public class PlayerControleur : MonoBehaviour
{
    [SerializeField]private PlayerMovement _cameraController2;
    [SerializeField]private PlayerInput _inputManager;
   
  void Update()
  {
     
    //  Debug.Log(_inputManadjeur.evenement);
     
        if (_inputManager.action )
        {
            _cameraController2.Move();
        }
  }
}
