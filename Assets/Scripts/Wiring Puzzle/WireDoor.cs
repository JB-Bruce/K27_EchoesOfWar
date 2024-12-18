using UnityEngine;

public class WireDoor : MonoBehaviour, IInteractable
{
    public string interactableName => "WireDoor";

    [SerializeField] Outline _outline;
    public Outline outline => _outline;

    bool opened = false;

    [SerializeField] Animator _animator;

    private void Start()
    {
        outline.enabled = false;
    }

    public void Interact()
    {
        opened = !opened;
        _animator.Play(opened ? "Open" : "Close", -1, 0f);
    }
}
