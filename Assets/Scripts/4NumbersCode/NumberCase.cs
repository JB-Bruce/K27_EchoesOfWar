using TMPro;
using UnityEngine;

public class NumberCase : MonoBehaviour
{
    [SerializeField] private int _currentNumber;
    [SerializeField] private int _correctNumber;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _currentNumber = Random.Range(0, 9);
        _correctNumber = Random.Range(0, 9);
        
        _text.text = _currentNumber.ToString();
    }

    public void IncrementDecrementNumber(bool increment)
    {
        _currentNumber += increment ? 1 : -1;
        _currentNumber = (_currentNumber + 10) % 10;
        _text.text = _currentNumber.ToString();
    }
    
    public bool IsCorrect => _currentNumber == _correctNumber;
}