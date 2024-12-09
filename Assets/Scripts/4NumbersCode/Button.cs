using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;
    [SerializeField] private Outline _outline;
    public UnityEvent OnClick;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _name = this.gameObject.name;
        _outline = this.gameObject.GetComponent<Outline>();

    }

    public void Interact()
    {
        OnClick?.Invoke();
    }
    public string interactableName => _name;
    public Outline outline => _outline;
}
