using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class SubmarineButton : MonoBehaviour, IInteractable
{
    private readonly UnityEvent _onButtonPressed = new();
    
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void Interact()
    {
        _onButtonPressed.Invoke();
    }
    
    public UnityEvent OnButtonPressed => _onButtonPressed;

    public bool DoesNeedToStopPlayerMovement => false;
    public Outline outline => _outline;
}
