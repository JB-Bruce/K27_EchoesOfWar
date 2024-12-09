using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardManager : MonoBehaviour
{
    [SerializeField] public List<NumberCase> _numberCases;
    [SerializeField] public NumberCase _selectedNumberCase;
    [SerializeField] public int _selectedNumberCaseIndex = 0;
    [SerializeField] private bool _correctCode = false;

    public UnityEvent DiscoveredCode;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _selectedNumberCase = _numberCases[_selectedNumberCaseIndex];
        foreach (var numberCase in _numberCases)
        {
            int randomNumber = Random.Range(0, 9);
            numberCase._correctNumber = randomNumber;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_correctCode)
        {
            CheckCode();
        }
    }

    private void CheckCode()
    {
        int correctNumberCases = 0;
        foreach (var numberCase in _numberCases)
        {
            if (numberCase._currentNumber == numberCase._correctNumber)
            {
                correctNumberCases++;
            }
        }

        if (correctNumberCases == _numberCases.Count)
        {
            DiscoveredCode?.Invoke();
        }
    }
}
