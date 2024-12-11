using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PipesBoard : MonoBehaviour
{
    [Header("Solutions Pipes")]
    [SerializeField] private List<Pipe> _pipesNeeded;
    private int _correctPipeCount;

    [Header("Valves and Themometer")]
    [SerializeField] private List<Valve> _valves;
    private int _selectedValveIndex = 0;

    [Header("Buttons")]
    [SerializeField] private SubmarineButton _addButton;
    [SerializeField] private SubmarineButton _substractButton;
    [SerializeField] private SubmarineButton _leftButton;
    [SerializeField] private SubmarineButton _rightButton;

    [Header("Indicator Positions")]
    [SerializeField] private Transform _indicator;
    [SerializeField] private List<Transform> _transformsIndicator;

    private readonly UnityEvent _onPipesResolved = new();
    private readonly UnityEvent _onValvesResolved = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        foreach (var pipe in _pipesNeeded)
        {
            pipe.CorrectIndex = Random.Range(0, 3);
            pipe.OnPipePressed.AddListener(() => CheckPipes());
        }

        _addButton.OnButtonPressed.AddListener(() => IncrementDecrementSelectedValve(true));
        _substractButton.OnButtonPressed.AddListener(() => IncrementDecrementSelectedValve(false));

        _leftButton.OnButtonPressed.AddListener(() => MoveRightLeftIndicator(false));
        _rightButton.OnButtonPressed.AddListener(() => MoveRightLeftIndicator(true));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool CheckPipes()
    {
        foreach (var pipe in _pipesNeeded)
        {
            if (!pipe.IsCorrect)
            {
                return false;
            }
        }
        Debug.Log(true);
        return true;
    }

    private bool CheckValve()
    {
        foreach (var valve in _valves)
        {
            if (!valve.IsCorrect)
            {
                return false;
            }
        }
        Debug.Log(true);
        return true;
    }

    private void IncrementDecrementSelectedValve(bool increment)
    {
        _valves[_selectedValveIndex].IncrementDecrementNumber(increment);

        if (CheckValve())
            _onValvesResolved.Invoke();
    }

    private void MoveRightLeftIndicator(bool isRight)
    {
        _selectedValveIndex += isRight ? 1 : -1;
        _selectedValveIndex = (_selectedValveIndex + _valves.Count) % _valves.Count;

        _indicator.position = _transformsIndicator[_selectedValveIndex].position;
    }


    public UnityEvent OnPipesResolved => _onPipesResolved;
    public UnityEvent OnValvesResolved => _onValvesResolved;
}

