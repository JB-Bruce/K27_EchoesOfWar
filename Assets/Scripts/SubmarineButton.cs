using UnityEngine;
using UnityEngine.Events;

public class SubmarineButton : MonoBehaviour, IInteractable
{
    private readonly UnityEvent _onButtonPressed = new();

    public void Interact()
    {
        _onButtonPressed.Invoke();
    }
    
    public UnityEvent OnButtonPressed => _onButtonPressed;

    public bool DoesNeedToStopPlayerMovement => false;
}
