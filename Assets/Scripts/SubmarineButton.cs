using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class SubmarineButton : MonoBehaviour, IInteractable
{
    private readonly UnityEvent _onButtonPressed = new();
    
    private Outline _outline;

    [SerializeField] Animator _animator;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void Interact()
    {
        _onButtonPressed.Invoke();
        _animator.Play("Pressed", -1, 0f);
    }
    
    public UnityEvent OnButtonPressed => _onButtonPressed;
    public Outline outline => _outline;

    public string interactableName => "SubButton";
}
