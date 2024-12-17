using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _transformDoor;
    [SerializeField] private bool isOpen = false;

    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] List<Light> _lights;

    [SerializeField] Color _onColor;
    [SerializeField] Color _offColor;

    [SerializeField] Animator _animator;

    [SerializeField] private Outline _outline;
    [SerializeField] AudioClip _openDoor;
    [SerializeField] AudioClip _closeDoorr;

    private bool _isLocked = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(((IInteractable)this).DeactivateOutline());
        ApplyColor();
    }

    public void SetLocked(bool locked)
    {
        _isLocked = locked;
        ApplyColor();
    }

    private void ApplyColor()
    {
        Color selectedColor = _isLocked ? _offColor : _onColor;
        _meshRenderer.material.color = selectedColor;
        _meshRenderer.material.SetColor("_EmissionColor", selectedColor);

        foreach (var item in _lights)
        {
            item.color = selectedColor;
        }
    }

    public void Interact()
    {
        if (isOpen)
        {
            isOpen = false;
            _animator.Play("Close");
            AudioManageur.Instance.PlayClipAt(_openDoor,transform.position);
        }
        else
        {
            if (_isLocked) return;
            isOpen = true;
            _animator.Play("Open");
            AudioManageur.Instance.PlayClipAt(_closeDoorr,transform.position);

        }
    }

    public string interactableName => "Door";

    public Outline outline => _outline;
}
