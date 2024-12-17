using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Valve : MonoBehaviour
{
    private float _currentNumber;
    [SerializeField] private float _correctNumber;
    [SerializeField] private float _maxNumber;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private bool _isThemometer;

    private readonly UnityEvent _onValveChanged = new();

    private void Start()
    {
        //if (_isThemometer)
        //{
        //    float difference = 0f;
        //    do
        //    {
        //        _currentNumber = Random.Range(0, _maxNumber);
        //        difference = _correctNumber - _currentNumber;

        //        if (difference < 0f)
        //        {
        //            difference = -difference;
        //        }
        //    } while (difference <= 60f || difference >= 0f);
        //} 
        //else
        //{
        //    _currentNumber = Random.Range(0, _maxNumber);
        //}

        _currentNumber = Random.Range(0, _maxNumber);
    }

    public void IncrementDecrementNumber(bool increment)
    {
        _currentNumber += increment ? 1f : -1f;
        _currentNumber = (_currentNumber + _maxNumber) % _maxNumber;
        _onValveChanged.Invoke();

        _text.text = _currentNumber.ToString();
    }

    public bool IsCorrect => _currentNumber == _correctNumber;
    public UnityEvent OnValvePressed => _onValveChanged;
}
