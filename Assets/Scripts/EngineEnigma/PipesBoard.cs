using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;

public class PipesBoard : MonoBehaviour
{
    [Header("Solutions Pipes")]
    [SerializeField] private List<Pipe> _pipesNeeded;
    private int _correctPipeCount;

    [Header("Valves and Themometer")]
    [SerializeField] private List<Valve> _valves;

    [Header("Wheels")]
    [SerializeField] private List<Wheel> _wheels;
    private int _selectedValveIndex = 0;

    private readonly UnityEvent _onPipesResolved = new();
    private readonly UnityEvent _onValvesResolved = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var pipe in _pipesNeeded)
        {
            pipe.OnPipePressed.AddListener(() => CheckPipes());
        }

        foreach (var valve in _valves)
        {
            valve.OnValvePressed.AddListener(() => CheckValve());
        }
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

    public UnityEvent OnPipesResolved => _onPipesResolved;
    public UnityEvent OnValvesResolved => _onValvesResolved;
}

