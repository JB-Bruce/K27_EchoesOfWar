using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Valve : MonoBehaviour
{
    private float _currentNumber;
    [SerializeField] private int _correctNumber;
    [SerializeField] private int _maxNumber;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private bool _isThemometer;

    private readonly UnityEvent _onValveChanged = new();

    private void Start()
    {
        ResetValve();
        _text.text = _currentNumber.ToString();
    }

    public void ResetValve()
    {
        if (_isThemometer)
        {
            float difference = 0f;
            do
            {
                _currentNumber = Random.Range(0, _maxNumber);
                difference = _correctNumber - _currentNumber;

                if (difference < 0f)
                {
                    difference = -difference;
                }
            } while (difference >= 100f); //to avoid any situation where the player has to clicked more than a thousand times
        }
        else
        {
            _currentNumber = Random.Range(0, _maxNumber);
        }

        _text.text = _currentNumber.ToString();
    }

    public void IncrementDecrementNumber(bool increment)
    {
        _currentNumber += increment ? 1 : -1;
        _currentNumber = (_currentNumber + _maxNumber) % _maxNumber;

        _onValveChanged.Invoke();

        _text.text = _currentNumber.ToString();
    }

    public bool IsCorrect => _currentNumber == _correctNumber;
    public UnityEvent OnValvePressed => _onValveChanged;
}
