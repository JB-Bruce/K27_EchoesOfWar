using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pipe : MonoBehaviour
{
    [SerializeField] private bool _isPartOfTheSolution;
    [SerializeField] private List<int> _correctIndex;

    [HideInInspector] public bool IsCorrect;
    private int _currentIndex = 0;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _currentIndex = Random.Range(0, 4);
    }

    public void RotatePipe()
    {
        if (_currentIndex < 3)
        {
            _currentIndex++;
        }
        else
        {
            _currentIndex = 0;
        }
        _transform.Rotate(90, 0, 0);

        foreach (var index in _correctIndex)
        {
            if (!(index == _currentIndex))
            {
                IsCorrect = true;
            }
            else { IsCorrect = false; }
        }
    }

    public void ResetPipe()
    {
        float number = Random.Range(0, 4);
        for (int i = 0; i < number; i++)
        {
            RotatePipe();
        }
    }
}
