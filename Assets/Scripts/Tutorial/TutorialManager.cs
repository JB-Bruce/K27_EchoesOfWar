using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tutorialText;
    [SerializeField] private List<TutorialStep> _tutorialSteps;

    private bool _isFinished;
    private TutorialStep _currentTutorialStep;
    private int _currentTutorialStepIndex = -1;

    private void Start()
    {
        _tutorialText.gameObject.SetActive(true);
        _tutorialText.enabled = true;
        NextTutorialStep();
        //Test();
    }


    public void CheckAction(InputAction action)
    {
        if (_isFinished)
            return;
        
        if (!_currentTutorialStep.inputActionReference.action.name.Equals(action.name))
            return;
        
        NextTutorialStep();
    }

    private void NextTutorialStep()
    {
        if (_currentTutorialStepIndex < _tutorialSteps.Count - 1)
        {
            _currentTutorialStepIndex++;
            _currentTutorialStep = _tutorialSteps[_currentTutorialStepIndex];
        
            _tutorialText.text = _currentTutorialStep.description + GetBinding(_currentTutorialStep.inputActionReference);
        }
        else
        {
            _isFinished = true;
            _tutorialText.text = "";
            _tutorialText.enabled = false;
        }
    }

    private string GetBinding(InputAction inputAction)
    {
        var firstBinding = inputAction.bindings[0];

        return firstBinding.isComposite ? GetAllBindings(inputAction) : firstBinding.ToDisplayString(InputBinding.DisplayStringOptions.DontUseShortDisplayNames);
    }

    private string GetAllBindings(InputAction inputAction)
    {
        string display = "";
        
        var inputBindings = inputAction.bindings;

        foreach (var binding in inputBindings)
        {
            if (binding.action.Contains("Scroll"))
            {
                display += binding.action;
                break;
            }
            else 
                display += binding.ToDisplayString(InputBinding.DisplayStringOptions.DontUseShortDisplayNames) + " ";
        }
        
        return display;
    }
    
    public bool IsFinished => _isFinished;
    
    [Serializable]
    private struct TutorialStep
    {
        public string name;
        public string description;
        public InputActionReference inputActionReference;
        public GameObject target;
    }
}
