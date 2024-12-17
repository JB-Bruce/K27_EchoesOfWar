using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class SubmarineButton : MonoBehaviour, IInteractable
{
    private readonly UnityEvent _onButtonPressed = new();
    private Func<bool> _canBePressed;
    
    private Outline _outline;

    [SerializeField] Animator _animator;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = true;
    }

    private void Start()
    {
        StartCoroutine(((IInteractable)this).DeactivateOutline());
    }

    public void Interact()
    {
        if (_canBePressed?.Invoke() ?? true)
        {
            _onButtonPressed.Invoke();
            _animator.Play("Pressed", -1, 0f);
        }
    }

    public void SetCanBePressed(Func<bool> canBePressed)
    {
        _canBePressed = canBePressed;
    }
    
    public Func<bool> CanBePressed {get => _canBePressed; set => _canBePressed = value;}

    public UnityEvent OnButtonPressed => _onButtonPressed;
    public Outline outline => _outline;

    public string interactableName => "SubButton";
}
