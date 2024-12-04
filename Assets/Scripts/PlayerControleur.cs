using UnityEngine;

public class PlayerControleur : MonoBehaviour
{
    public PlayerDeplassement _cameraController2;
   public PlayerImput _inputManadjeur;
   
  void Update()
  {
     
    //  Debug.Log(_inputManadjeur.evenement);
     
        if (_inputManadjeur.actiont )
        {
            _cameraController2.mouve();
        }
  }
}
