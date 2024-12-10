using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private SubmarineButton _increaseNumberButton;
    [SerializeField] private SubmarineButton _decreaseNumberButton;
    [SerializeField] private SubmarineButton _moveIndexToLeftButton;
    [SerializeField] private SubmarineButton _moveIndexToRightButton;
    
    [Header("Numbers Case")]
    [SerializeField] private List<NumberCase> _numberCases;
    [SerializeField] private Transform _indicator;
    
    private int _selectedNumberCaseIndex = 0;
    private readonly UnityEvent _onCodeDiscovered = new();
    private float _indicatorYOffset;

    private void Start()
    {
        _indicatorYOffset = _indicator.position.y - _numberCases[0].transform.position.y;
        
        _increaseNumberButton.OnButtonPressed.AddListener(() => IncrementDecrementSelectedNumberCase(true));
        _decreaseNumberButton.OnButtonPressed.AddListener(() => IncrementDecrementSelectedNumberCase(false));
        _moveIndexToLeftButton.OnButtonPressed.AddListener(() => MoveRightLeftIndicator(false));
        _moveIndexToRightButton.OnButtonPressed.AddListener(() => MoveRightLeftIndicator(true));
    }

    private void OnDestroy()
    {
        _increaseNumberButton.OnButtonPressed.RemoveAllListeners();
        _decreaseNumberButton.OnButtonPressed.RemoveAllListeners();
        _moveIndexToLeftButton.OnButtonPressed.RemoveAllListeners();
        _moveIndexToRightButton.OnButtonPressed.RemoveAllListeners();
    }

    private void MoveRightLeftIndicator(bool isRight)
    {
        _selectedNumberCaseIndex += isRight ? 1 : -1;
        _selectedNumberCaseIndex = (_selectedNumberCaseIndex + _numberCases.Count) % _numberCases.Count;
        
        _indicator.position = _numberCases[_selectedNumberCaseIndex].transform.position + Vector3.up * _indicatorYOffset;
    }

    private void IncrementDecrementSelectedNumberCase(bool increment)
    {
        _numberCases[_selectedNumberCaseIndex].IncrementDecrementNumber(increment);
        
        if (CheckCode())
            _onCodeDiscovered.Invoke();
    }

    private bool CheckCode()
    {
        foreach (var numberCase in _numberCases)
        {
            if (!numberCase.IsCorrect)
                return false;
        }

        return true;
    }
    
    public UnityEvent OnCodeDiscovered => _onCodeDiscovered;
}
