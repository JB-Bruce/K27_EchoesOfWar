using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NumberCase : MonoBehaviour
{
    [SerializeField] private int _currentNumber;
    [SerializeField] private int _correctNumber;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private SubmarineButton upBtn;
    [SerializeField] private SubmarineButton downBtn;

    public UnityEvent numberChangedEvent = new();

    private void Start()
    {
        upBtn.OnButtonPressed.AddListener(Increment);
        downBtn.OnButtonPressed.AddListener(Decrement);

        _currentNumber = Random.Range(0, 9);
        
        _text.text = _currentNumber.ToString();
    }

    public void Decrement()
    {
        IncrementDecrementNumber(false);
    }

    public void Increment()
    {
        IncrementDecrementNumber(true);
    }

    public void IncrementDecrementNumber(bool increment)
    {
        _currentNumber += increment ? 1 : -1;
        _currentNumber = (_currentNumber + 10) % 10;
        _text.text = _currentNumber.ToString();

        numberChangedEvent.Invoke();
    }
    
    public bool IsCorrect => _currentNumber == _correctNumber;
}