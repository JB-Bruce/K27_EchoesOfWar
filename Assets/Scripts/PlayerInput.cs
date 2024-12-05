using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    
    public Vector2 _mouseDelta { get; private set; }// Used in the PlayerCamera
    
    [SerializeField] private HotBar _hotBar;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        _playerController.Move(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _playerController.Interact();
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

    private void Update()
    {
        _mouseDelta = Mouse.current.delta.ReadValue();
    }
    
}
