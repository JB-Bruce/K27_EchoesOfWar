using UnityEngine;

public class PaperMap : MonoBehaviour, IFinishedInteractable
{
    [SerializeField] private Transform target;
    [SerializeField] private DrawScript drawScript;

    [SerializeField] Outline _outline;
    public Outline outline => _outline;

    [SerializeField] bool _doesStopMovements;
    public bool doesStopMovements => _doesStopMovements;

    [SerializeField] bool _doesLockView;
    public bool doesLockView => _doesLockView;

    [SerializeField] bool _canInteractWithOtherInteractablesWhileInteracted;
    public bool canInteractWithOtherInteractablesWhileInteracted => _canInteractWithOtherInteractablesWhileInteracted;
    public bool isInteracted { get; set; }

    public string interactableName => "PaperMap";

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void Interact()
    {
        if (isInteracted)
        {
            Uninteract();
            return;
        }

        PlayerController.Instance.SetPlayerBlockingInteractable("SubControls", true);
        PlayerController.Instance.SetCameraBlockingInteractables("SubControls", true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        drawScript.enabled = true;

        isInteracted = true;

        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().SetTarget(target);
        }
    }

    public void Uninteract()
    {
        PlayerController.Instance.SetPlayerBlockingInteractable("SubControls", false);
        PlayerController.Instance.SetCameraBlockingInteractables("SubControls", false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        drawScript.enabled = false;
        
        isInteracted = false;

        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().ResetTarget();
        }
    }
}
