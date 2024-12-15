using System;
using UnityEngine;
using UnityEngine.Events;

public class DoorSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] MeshRenderer _meshRenderer;

    [SerializeField] Color _onColor;
    [SerializeField] Color _offColor;

    [SerializeField] Animator _animator;

    public bool isOn { get; private set; } = true;

    public string interactableName => "DoorSwitch";

    [SerializeField] Outline _outline;
    public Outline outline => _outline;

    [SerializeField] Light _light;

    UnityEvent changedEvent = new();

    public void Interact()
    {
        SetActivation(!isOn);
    }


    public void Init(bool isOn, UnityAction changedAction)
    {
        _meshRenderer.material = new Material(_meshRenderer.material);
        _outline.enabled = false;
        SetActivation(isOn);

        changedEvent.AddListener(changedAction);
    }

    private void ApplyColor()
    {
        _meshRenderer.material.color = isOn ? _onColor : _offColor;
        _meshRenderer.material.SetColor("_EmissionColor", isOn ? _onColor : _offColor);
        _light.color = isOn ? _onColor : _offColor;
    }

    public void SetActivation(bool isActive)
    {
        isOn = isActive;
        ApplyColor();
        _animator.Play(isOn ? "ON" : "OFF", -1, 0f);

        changedEvent.Invoke();
    }
}
