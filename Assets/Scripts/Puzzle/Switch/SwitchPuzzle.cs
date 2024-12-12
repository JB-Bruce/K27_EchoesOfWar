using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchPuzzle : MonoBehaviour, IInteractable
{
    [SerializeField] private List<SwitchPuzzle> _affectedSwitchButtonPuzzles;
    [SerializeField] Animator _animator;
    
    private readonly UnityEvent _onButtonPressed = new();
    private Outline _outline;
    private bool _isActive;
    
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _deactiveColor;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        
        InitSwitch();
    }

    public void InitSwitch()
    {
        _isActive = Random.Range(0, 2) == 1;
        ChangeState();
    }

    public void Interact()
    {
        OnSwitchInteracted();
        _onButtonPressed.Invoke();
        _animator.Play("Pressed", -1, 0f);
    }

    private void OnSwitchInteracted()
    {
        ChangeState();
        AffectSwitchButton();
    }

    private void AffectSwitchButton()
    {
        foreach (var button in _affectedSwitchButtonPuzzles)
        {
            button.ChangeState();
        }
    }

    private void ChangeState()
    {
        _isActive = !_isActive;
        
        GetComponent<MeshRenderer>().material.color = _isActive ? _activeColor : _deactiveColor;
    }

    public void ForceActivateSwitch()
    {
        _isActive = false;
        ChangeState();
    }
    
    public bool IsActive => _isActive;
    public UnityEvent OnButtonPressed => _onButtonPressed;

    public Outline outline => _outline;

    public string interactableName { get; }
}
