using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Pipe : MonoBehaviour, IInteractable
{
    [SerializeField] private int _currentIndex = 0;
    private Transform _transform;
    private string _name;
    private Outline _outline;
    private readonly UnityEvent _onPipePressed = new();

    public int CorrectIndex;


    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _outline = GetComponent<Outline>();
        _name = gameObject.name;

        _currentIndex = Random.Range(0, 4);
    }

    void Start()
    {
        StartCoroutine(((IInteractable)this).DeactivateOutline());
    }
    private void Update()
    {
        if (_currentIndex == CorrectIndex)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        } else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
    public void Interact()
    {
        if (_currentIndex < 3)
        {
            _currentIndex++;
            Debug.Log(_currentIndex);
        } else
        {
            _currentIndex = 0;
            Debug.Log(_currentIndex);
        }
        _transform.Rotate(0, 0, 90);
        _onPipePressed.Invoke();
    }

    public Outline outline => _outline;
    public string interactableName => _name;
    public bool IsCorrect => CorrectIndex == _currentIndex;
    public UnityEvent OnPipePressed => _onPipePressed;
}
