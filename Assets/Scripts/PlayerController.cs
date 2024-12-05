using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInteractions _playerInteractions;
    
    [Header("Submarine")]
    [SerializeField] private SubmarineController _submarineController;

    private bool _movePlayer = true;

    public void Move(Vector2 movement)
    {
        if (_movePlayer)
            _playerMovement.SetMovement(movement);
        else
        {
            _submarineController.SetMovement(Mathf.RoundToInt(movement.y));
            _submarineController.Rotate(Mathf.RoundToInt(movement.x));
        }
    }

    public void Interact()
    {
        if (_playerInteractions.NeedToStopPlayerMovement())
            _movePlayer = !_movePlayer;
        
        _playerInteractions.Interact();
    }
}
