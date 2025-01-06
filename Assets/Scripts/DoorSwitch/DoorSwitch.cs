using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorSwitch : MonoBehaviour, IInteractable
{

    [SerializeField] Animator _animator;

    public bool isOn { get; private set; } = true;

    public string interactableName => "DoorSwitch";

    [SerializeField] Outline _outline;
    public Outline outline => _outline;

    [SerializeField] AudioClip _audioSwitch;

    UnityEvent changedEvent = new();

    [SerializeField] List<ActivationLight> lights;

    public void Interact()
    {
        ChangeActivation(!isOn);
        AudioManageur.Instance.PlayClipAt(_audioSwitch,transform.position);
    }


    public void Init(UnityAction changedAction)
    {
        _outline.enabled = false;

        changedEvent.AddListener(changedAction);
    }

    private void ApplyColor()
    {
        foreach (var light in lights)
        {
            light.SetActivation(isOn);
        }
    }

    public void SetActivation(bool isActive)
    {
        isOn = isActive;
        ApplyColor();
        _animator.Play(isOn ? "ON" : "OFF", -1, 0f);
    }

    private void ChangeActivation(bool activation)
    {
        SetActivation(activation);
        changedEvent.Invoke();
    }
}
