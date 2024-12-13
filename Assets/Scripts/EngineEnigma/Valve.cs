using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Valve : MonoBehaviour
{
    public float _currentNumber;
    public float _correctNumber;

    private readonly UnityEvent _onValveChanged = new();

    private void Start()
    {
        _currentNumber = Random.Range(0, 101);
        _correctNumber = Random.Range(0, 101);
         Debug.Log(_correctNumber + gameObject.name);
    }

    public void IncrementDecrementNumber(bool increment)
    {
        _currentNumber += increment ? 1 : -1;
        _currentNumber = (_currentNumber + 101) % 101;
        _onValveChanged.Invoke();
    }

    public bool IsCorrect => _currentNumber == _correctNumber;
    public UnityEvent OnValvePressed => _onValveChanged;
}
