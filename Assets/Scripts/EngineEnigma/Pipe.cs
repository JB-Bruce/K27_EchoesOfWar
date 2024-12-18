using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pipe : MonoBehaviour
{
    [SerializeField] private bool _isPartOfTheSolution;
    [SerializeField] private List<int> _correctIndex;

    public bool IsCorrect;
    public int _currentIndex = 0;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        IsCorrect = CheckPipeIfCorrect();
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

        IsCorrect = CheckPipeIfCorrect();
    }

    private bool CheckPipeIfCorrect()
    {
        foreach (var index in _correctIndex)
        {
            if (index == _currentIndex)
            {
                return true;
            }
        }
        return false;
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
