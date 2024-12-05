using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput:MonoBehaviour
{
    
    public Vector2 _mouseDelta { get; private set; }// Used in the PlayerCamera
    
    public  InputAction.CallbackContext evenement { get; private set; }
    [SerializeField] private HotBar _hotBar;
    public bool action { get; private set; }=false ;
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

    public void DropItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hotBar.DropItem();
        }
    }

    public void scroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hotBar.ScrollSelect(context);
        }
    }
    
    void Update()
    {
        _mouseDelta = Mouse.current.delta.ReadValue(); //Rcover mouse movement
    }
    
}
