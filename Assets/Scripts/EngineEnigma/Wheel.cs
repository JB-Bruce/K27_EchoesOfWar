using UnityEngine;
[RequireComponent (typeof(Outline))]
public class Wheel: MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Valve _valve;

    private string _name;

    private Outline _outline;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _outline = GetComponent<Outline>();
        _name = gameObject.name;

        StartCoroutine(((IInteractable)this).DeactivateOutline());

    }

    public void Interact()
    {
        _valve._currentNumber++;
        _transform.eulerAngles += new Vector3(5, 0, 0);
    }

    public string interactableName => _name;
    public Outline outline => _outline;
}
