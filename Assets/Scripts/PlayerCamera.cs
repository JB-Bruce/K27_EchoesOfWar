using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
     public float _sensitivity = 1f;  // Sensibilité de la souris
    public Transform _cameraBody;    // Object on which rotatin
    public Transform _playerBody;   // Object on which rotatin
    private float _xRotation = 0f;  // Rotatin on X axis ( vertical)
    private float _yRotation = 0f; // Rotatin on y axis ( Horizontale )

    public PlayerImput _playerImput ;
    
    //Transform _transform;

    void Update()
    {
        
         _yRotation += _playerImput._mouseDelta.x * _sensitivity;// Apply camera rotation on Y axis (left/right)
         _playerBody.localRotation = Quaternion.Euler(0f, _yRotation, 0f);  //Takes care of changed the angle of the cam on the Y axis (looked at the left; looked at the right)  
         _xRotation -= _playerImput._mouseDelta.y * _sensitivity;// Apply camera rotation on X axis (up/down)
         _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);  // Limit rotation on X axis to avoid reversing the camera
         _cameraBody.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);//Takes care of changed the angle of the cam on the X axis (looked at the hot; looked at the bottom )  
       
    }
}
