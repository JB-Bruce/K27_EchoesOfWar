using TMPro;
using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField] private float _currentNumber;
    [SerializeField] private float _correctNumber;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _currentNumber = Random.Range(0, 101);
        _correctNumber = Random.Range(0, 101);
         Debug.Log(_correctNumber + gameObject.name);
        _text.text = _currentNumber.ToString();
    }

    public void IncrementDecrementNumber(bool increment)
    {
        _currentNumber += increment ? 1 : -1;
        _currentNumber = (_currentNumber + 101) % 101;
        _text.text = _currentNumber.ToString();
    }

    public bool IsCorrect => _currentNumber == _correctNumber;
}
