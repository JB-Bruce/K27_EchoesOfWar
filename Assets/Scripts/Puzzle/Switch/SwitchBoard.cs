using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchBoard : MonoBehaviour
{
    [SerializeField] private bool _startAllActivated;
    [SerializeField] private bool _canDiscoverCodeMultipleTimes;
    [SerializeField] private List<SwitchPuzzle> _switchPuzzles = new();
    
    private readonly UnityEvent _onCodeDiscovered = new();
    private bool _isCodeDiscovered;

    private void Start()
    {
        if (_startAllActivated)
        {
            foreach (var switchPuzzle in _switchPuzzles)
            {
                switchPuzzle.ForceActivateSwitch();
            }
        }
        
        foreach (SwitchPuzzle button in _switchPuzzles)
        {
            button.OnButtonPressed.AddListener(OnSwitchPressed);
        }
        
        _onCodeDiscovered.AddListener(OnCodeDiscovered);
    }

    private void OnSwitchPressed()
    {
        if (_isCodeDiscovered && !_canDiscoverCodeMultipleTimes) 
            return;
        
        if (!CheckCode())
            return;
        
        _isCodeDiscovered = true;
        _onCodeDiscovered.Invoke();
    }

    private void OnCodeDiscovered()
    {
        Debug.Log("Code Discovered");
    }

    private bool CheckCode()
    {
        foreach (var button in _switchPuzzles)
        {
            if (!button.IsActive)
                return false;
        }
        
        return true;
    }

    public void ResetPuzzle()
    {
        _isCodeDiscovered = false;
        
        foreach (var switchPuzzle in _switchPuzzles)
        {
            switchPuzzle.InitSwitch();
        }
    }
    
    //public UnityEvent OnCodeDiscovered => _onCodeDiscovered;
}