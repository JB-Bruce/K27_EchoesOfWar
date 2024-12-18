using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tutorialText;
    [SerializeField] private List<TutorialStep> _tutorialSteps;

    [SerializeField] bool playTutorial = true;
    private bool _isFinished;
    private TutorialStep _currentTutorialStep;
    private int _currentTutorialStepIndex = -1;
    private string _splitInputActionText = " or ";
    private readonly UnityEvent _onTutorialFinished = new();

    LinkedList<string> textsToShow = new();
    bool canPlayNewText = true;

    [SerializeField] float minTimeTextIsOnScreen;
    [SerializeField] Animator animator;

    [SerializeField] string endText;
    [SerializeField] float endTextDuration;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _isFinished = !playTutorial;
        
        _tutorialText.gameObject.SetActive(playTutorial);
        _tutorialText.enabled = playTutorial;
            
        if (playTutorial)
        {
            NextTutorialStep();
            SetNewText();
            return;
        }

        OnTutorialFinished.Invoke();
    }

    public void SetNewText()
    {
        if (textsToShow.Count == 0) return;

        _tutorialText.text = textsToShow.Last.Value;
        textsToShow.RemoveLast();
        canPlayNewText = false;

        animator.Play("Show");

        Invoke("ReachedMinTextTime", IsFinished && textsToShow.Count == 0 ? endTextDuration : minTimeTextIsOnScreen);
    }

    private void MaskText()
    {
        animator.Play("Hide");
    }

    private void ReachedMinTextTime()
    {
        if (IsFinished && textsToShow.Count == 0) MaskText();
        canPlayNewText = true;
    }

    private void AddNewText(string s)
    {
        textsToShow.AddFirst(s);
    }

    private void Update()
    {
        if(textsToShow.Count > 0)
        {
            if (canPlayNewText)
                MaskText();
        }
    }

    public void CheckAction(InputAction action)
    {
        if (_isFinished)
            return;

        
        if (_currentTutorialStep.targets.Length > 0)
        {
            bool isInHotbar = false;
            
            foreach (var target in _currentTutorialStep.targets)
            {
                if (HotBar.Instance.IsInHotBar((Item)target))
                    isInHotbar = true;
            }
            
            if (!isInHotbar)
                return;
        }
        
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

            AddNewText(GetTutorialText());
        }
        else
        {
            AddNewText(endText);
            _isFinished = true;
            OnTutorialFinished.Invoke();
        }
    }

    private string GetTutorialText()
    {
        if (!_currentTutorialStep.isInputInDescription)
            return _currentTutorialStep.description + (_currentTutorialStep.showInputAction
                ? GetBinding(_currentTutorialStep.inputActionReference)
                : string.Empty);
        
        var descriptionSplit = _currentTutorialStep.description.Split("Input");

        return string.Join(_currentTutorialStep.showInputAction ? GetBinding(_currentTutorialStep.inputActionReference) : string.Empty, descriptionSplit);
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

        for (int i = 0; i < inputBindings.Count; i++)
        {
            var binding = inputBindings[i];
            
            if (binding.action.Contains("Scroll"))
            {
                display += binding.action + (_currentTutorialStep.splitInputAction ? _splitInputActionText : string.Empty);
                break;
            }

            if (binding.isComposite) 
                continue;
            
            display += binding.ToDisplayString(InputBinding.DisplayStringOptions.DontUseShortDisplayNames);
            
            if (i < inputBindings.Count - 1)
                display += _currentTutorialStep.splitInputAction ? _splitInputActionText : string.Empty;
        }
        
        return display;
    }
    
    public bool NeedTutorial { get => playTutorial; set => playTutorial = value; }

    public bool IsFinished => _isFinished;

    public string SplitInputActionText { get => _splitInputActionText; set => _splitInputActionText = value; }

    public UnityEvent OnTutorialFinished => _onTutorialFinished;

    [Serializable]
    private struct TutorialStep
    {
        public string name;
        public string description;
        public InputActionReference inputActionReference;
        public Object[] targets;
        public bool showInputAction;
        public bool splitInputAction;
        public bool isInputInDescription;
    }
}
