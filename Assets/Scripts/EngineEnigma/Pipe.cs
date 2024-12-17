using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pipe : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _isPartOfTheSolution;
    [SerializeField] private List<int> _correctIndex;
    [HideInInspector]public bool IsCorrect;
    private int _currentIndex = 0;

    private string _name;
    private Transform _transform;
    private Outline _outline;
    private readonly UnityEvent _onPipePressed = new();

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
    public void Interact()
    {
        if (_currentIndex < 3)
        {
            _currentIndex++;
            Debug.Log(_currentIndex);
        }
        else
        {
            _currentIndex = 0;
            Debug.Log(_currentIndex);
        }
        _transform.Rotate(90, 0, 0);
        _onPipePressed.Invoke();

        foreach (var index in _correctIndex)
        {
            if (!(index == _currentIndex))
            {
                IsCorrect = true;
            }
            else
            { IsCorrect = false; }
        }
    }

    public Outline outline => _outline;
    public string interactableName => _name;
    public UnityEvent OnPipePressed => _onPipePressed;
}
