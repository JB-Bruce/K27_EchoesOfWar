using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour, IInteractable
{
    [SerializeField] private int _index = 0;
    [SerializeField] private int _correctIndex;
    [SerializeField] private bool _caseNeeded;
    private Transform _transform;
    private string _name;
    private Outline _outline;


    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _outline = GetComponent<Outline>();
        _name = gameObject.name;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(((IInteractable)this).DeactivateOutline());
    }

    public void Interact()
    {
        if (_index < 3)
        {
            _index++;
        } else
        {
            _index = 0;
        }
        _transform.eulerAngles += new Vector3(90, 0, 0);
    }

    public Outline outline => _outline;
    public string interactableName => _name;
}
