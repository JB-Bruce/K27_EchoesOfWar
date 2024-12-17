using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class PipesBoard : MonoBehaviour, IFinishedInteractable
{
    [Header("Solutions Pipes")]
    [SerializeField] private List<Pipe> _pipesNeeded;
    private int _correctPipeCount;

    [Header("Valves and Themometer")]
    [SerializeField] private List<Valve> _valves;

    [Header("Wheels")]
    [SerializeField] private List<Wheel> _wheels;
    private int _selectedValveIndex = 0;

    [Header("Zoom")]
    [SerializeField] private Transform _target;

    private Outline _outline;
    private bool _doesStopMovements;
    private bool _doesLockView;
    private bool _canInteractWithOtherInteractablesWhileInteracted;

    private readonly UnityEvent _onPipesResolved = new();
    private readonly UnityEvent _onValvesResolved = new();


    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var valve in _valves)
        {
            valve.OnValvePressed.AddListener(() => CheckValve());
        }

        StartCoroutine(((IInteractable)this).DeactivateOutline());
    }

    public void Interact()
    {
        if (isInteracted)
        {
            Uninteract();
            return;
        }

        PlayerController.Instance.SetPlayerBlockingInteractable("PipesBoard", true);
        PlayerController.Instance.SetCameraBlockingInteractables("PipesBoard", true);

        isInteracted = true;

        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().SetTarget(_target);
        }
    }

    public void Uninteract()
    {
        PlayerController.Instance.SetPlayerBlockingInteractable("PipesBoard", false);
        PlayerController.Instance.SetCameraBlockingInteractables("PipesBoard", false);

        isInteracted = false;

        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().ResetTarget();
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
        return true;
    }

    public UnityEvent OnPipesResolved => _onPipesResolved;
    public UnityEvent OnValvesResolved => _onValvesResolved;

    public bool doesStopMovements => _doesStopMovements;

    public bool doesLockView => _doesLockView;

    public bool canInteractWithOtherInteractablesWhileInteracted => _canInteractWithOtherInteractablesWhileInteracted;

    public bool isInteracted { get; set ; }

    public string interactableName => "EngineBoard";

    public Outline outline => _outline;
}

