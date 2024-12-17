using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _target;
    
    public Vector2 _mouseDelta { get; private set; }// Used in the PlayerCamera

    
    [SerializeField] private HotBar _hotBar;
    [SerializeField] private UseItem _useItem;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _playerController.Move(context.ReadValue<Vector2>());
        _tutorialManager.CheckAction(context.action);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _playerController.Interact();
            _tutorialManager.CheckAction(context.action);
        }
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        _playerController.Cancel();
    }
    
    public void DropItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hotBar.DropItem();
            _tutorialManager.CheckAction(context.action);
        }
    }
    
    public void OnScroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hotBar.ScrollSelect(context);
            _tutorialManager.CheckAction(context.action);
        }
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_hotBar.GetSelectedItem() != null)
            {
                _useItem.Use(_hotBar.GetSelectedItem());
                _tutorialManager.CheckAction(context.action);
            }
        }
    }

    public void SwitchPage(InputAction.CallbackContext context)
    {
        if (context.performed && _useItem.activated && _hotBar.GetSelectedItem() is Book)
        {
            _useItem.TurnPages(context, _hotBar.GetSelectedItem() as Book);
            _tutorialManager.CheckAction(context.action);
        }
    }
    
    private void Update()
    {
        _mouseDelta = Mouse.current.delta.ReadValue();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
