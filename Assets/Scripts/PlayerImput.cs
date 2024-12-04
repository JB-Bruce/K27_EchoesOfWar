using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerImput:MonoBehaviour
{
    
    public Vector2 _mouseDelta;// Used in the PlayerCamera
    
      public  InputAction.CallbackContext evenement;
      
    public bool actiont=false;
        public void OnMove( InputAction.CallbackContext context )
    {
        evenement=context;
        actiont=true;
         
    }
    

    
    void Update()
    {
        _mouseDelta = Mouse.current.delta.ReadValue(); //Rcover mouse movement
         
    }
    
}
