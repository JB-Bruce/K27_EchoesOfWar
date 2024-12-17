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

    [Header("Zoom")]
    [SerializeField] private Transform _target;
    [SerializeField] private RenderTexture renderTxt;

    private Camera _camera;
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

        _camera = Camera.main;
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

    private void Update()
    {
        if (isInteracted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var pos = Input.mousePosition;
                pos = new(pos.x * renderTxt.width / Screen.width, pos.y * renderTxt.height / Screen.height);

                Ray ray = _camera.ScreenPointToRay(pos);
                RaycastHit hitInfos;

                if (Physics.Raycast(ray, out hitInfos))
                {
                    print(hitInfos.collider.gameObject.name);

                    if (hitInfos.transform.gameObject.TryGetComponent<Pipe>(out Pipe pipe))
                    {
                        pipe.RotatePipe();
                        CheckPipes();
                    }
                }
            }
        }
    }

    public void Interact()
    {
        if (isInteracted)
        {
            Uninteract();
            return;
        }

        PlayerController.Instance.SetPlayerBlockingInteractable("Wires", true);
        PlayerController.Instance.SetCameraBlockingInteractables("Wires", true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isInteracted = true;

        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().SetTarget(_target);
        }
    }

    public void Uninteract()
    {
        PlayerController.Instance.SetPlayerBlockingInteractable("Wires", false);
        PlayerController.Instance.SetCameraBlockingInteractables("Wires", false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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

