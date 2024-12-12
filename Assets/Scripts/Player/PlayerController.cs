using UnityEngine;
using System.Collections.Generic;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Player")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInteractions _playerInteractions;
    
    [Header("Submarine")]
    [SerializeField] private SubmarineController _submarineController;


    private bool _movePlayer = true;
    private bool _canMove = true;
    private bool _canLookAround = true;

    List<string> _blockingPlayerInteractables = new();

    List<string> _cameraBlockingInteractables = new();

    public void Move(Vector2 movement)
    {
        if (_movePlayer)
        {
            if (_canMove)
                _playerMovement.SetMovement(movement);
        }
        else
        {
            _submarineController.SetMovement(Mathf.RoundToInt(movement.y));
            _submarineController.SetRotation(-Mathf.RoundToInt(movement.x));
        }
    }

    private void Update()
    {
        if (_canLookAround)
            _playerMovement.UpdateCamera();
    }

    public void SwitchPlayerAndSubmarineControls(bool isPlayer)
    {
        _movePlayer = isPlayer;
        _submarineController.SetControls(!isPlayer);
    }

    public void SetPlayerBlockingInteractable(string interactable, bool doesBlock)
    {
        SetElementBlockingInteractable(interactable, doesBlock, ref _blockingPlayerInteractables, ref _canMove);
    }

    public void SetCameraBlockingInteractables(string interactable, bool doesBlock)
    {
        SetElementBlockingInteractable(interactable, doesBlock, ref _cameraBlockingInteractables, ref _canLookAround);
    }

    private void SetElementBlockingInteractable(string interactable, bool doesBlock, ref List<string> s, ref bool b)
    {
        if (s.Contains(interactable))
        {
            if (!doesBlock)
                s.Remove(interactable);
        }
        else
        {
            if (doesBlock)
                s.Add(interactable);
        }

        b = s.Count == 0;
    }

    public void Cancel()
    {
        _playerInteractions.Cancel();
        _canMove = true;
    }
    
    public void Interact()
    {
        _playerInteractions.TryInteract();
    }
}
