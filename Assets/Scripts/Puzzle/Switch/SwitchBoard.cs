using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchBoard : MonoBehaviour
{
    [SerializeField] private bool _canDiscoverCodeMultipleTimes;
    [SerializeField] private List<SwitchPuzzle> _switchPuzzles = new();
    
    private readonly UnityEvent _onCodeDiscovered = new();
    private bool _isCodeDiscovered;

    private void Start()
    {
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
    
    //public UnityEvent OnCodeDiscovered => _onCodeDiscovered;
}