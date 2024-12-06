using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _target;
    
    public Vector2 _mouseDelta { get; private set; }// Used in the PlayerCamera
    
    [SerializeField] private HotBar _hotBar;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _playerController.Move(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _playerController.Interact();
    }

    public void unInteract(InputAction.CallbackContext context)
    {
        _playerController.UnInteract();
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
