using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput:MonoBehaviour
{
    
    public Vector2 _mouseDelta;// Used in the PlayerCamera
    
      public  InputAction.CallbackContext evenement;
      
    public bool action=false;
        public void OnMove( InputAction.CallbackContext context )
    {
        if(context.performed)
        {
        evenement=context;
        action=true;
        }
        else if(context.canceled)
        {
            action=false;
        }
         
    }
    

    
    void Update()
    {
        _mouseDelta = Mouse.current.delta.ReadValue(); //Rcover mouse movement
         
    }
    
}
