using UnityEngine;
[RequireComponent (typeof(Outline))]
public class Wheel: MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Valve _valve;
    [SerializeField] private Transform _target;

    private Outline _outline;

    private bool _doesStopMovements;
    private bool _doesLockView;
    private bool _canInteractWithOtherInteractablesWhileInteracted;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = true;

        StartCoroutine(((IInteractable)this).DeactivateOutline());
    }

    void Update()
    {
        if (isInteracted)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _valve.IncrementDecrementNumber(false);
                _transform.Rotate(-2, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _valve.IncrementDecrementNumber(true);
                _transform.Rotate(2, 0, 0);
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

    public string interactableName => "Wheel";
    public Outline outline => _outline;
    public bool doesStopMovements => _doesStopMovements;
    public bool doesLockView => _doesLockView;
    public bool canInteractWithOtherInteractablesWhileInteracted => _canInteractWithOtherInteractablesWhileInteracted;
    public bool isInteracted { get ; set ; }
}
