using UnityEngine;

[RequireComponent(typeof(Outline))]
public class CabineDoor : MonoBehaviour, IInteractable
{
    public string interactableName => "CabineDoor";

    [SerializeField] Outline _outline;
    public Outline outline => _outline;

    bool _opened = false;
    bool _locked = false;

    [SerializeField] Animator _animator;

    private void Start()
    {
        outline.enabled = false;
    }

    public void SetLock(bool locked)
    {
        _locked = locked;
    }

    public void Interact()
    {
        if (_locked) return;

        _opened = !_opened;
        _animator.Play(_opened ? "Open" : "Close", -1, 0f);
    }
}
