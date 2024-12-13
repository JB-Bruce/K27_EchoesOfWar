using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchBoard : MonoBehaviour
{
    [SerializeField] private bool _startAllActivated;
    [SerializeField] private bool _canDiscoverCodeMultipleTimes;
    [SerializeField] private List<SwitchPuzzle> _switchPuzzles = new();
    [SerializeField] private List<bool> _switchCode;
    
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
        
        if (!Check())
            return;
        
        _isCodeDiscovered = true;
        _onCodeDiscovered.Invoke();
    }

    private bool Check()
    {
        return _switchCode.Count == 0 ? CheckWithoutCode() : CheckWithCode();
    }

    private bool CheckWithoutCode()
    {
        foreach (var button in _switchPuzzles)
        {
            if (!button.IsActive)
                return false;
        }
            
        return true;
    }

    private bool CheckWithCode()
    {
        for (int i = 0; i < _switchPuzzles.Count; i++)
        {
            if (_switchPuzzles[i].IsActive != _switchCode[i])
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
    
    public UnityEvent OnCodeDiscovered => _onCodeDiscovered;
}