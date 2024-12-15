using System.Collections;
using UnityEngine;

public class PaperMap : MonoBehaviour, IFinishedInteractable
{
    [SerializeField] private Transform target;

    [SerializeField] Outline _outline;
    public Outline outline => _outline;

    [SerializeField] bool _doesStopMovements;
    public bool doesStopMovements => _doesStopMovements;

    [SerializeField] bool _doesLockView;
    public bool doesLockView => _doesLockView;

    [SerializeField] bool _canInteractWithOtherInteractablesWhileInteracted;
    public bool canInteractWithOtherInteractablesWhileInteracted => _canInteractWithOtherInteractablesWhileInteracted;
    public bool isInteracted { get; set; }

    [SerializeField] Transform _mapItemParent;
    HotBar _hotBar;
    MapItem _mapInPlace;

    public string interactableName => "PaperMap";

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = true;
    }

    private void Start()
    {
        _hotBar = HotBar.Instance;
        StartCoroutine(((IInteractable)this).DeactivateOutline());
    }
    public void Interact()
    {
        if(_hotBar.GetSelectedItemName() == "Map")
        {
            GameObject map = _hotBar.DropItem();

            if(_mapInPlace != null)
                _mapInPlace.Interact();

            _mapInPlace = map.GetComponent<MapItem>();
            SetMap(_mapInPlace);

            return;
        }

        if (isInteracted)
        {
            Uninteract();
            return;
        }

        PlayerController.Instance.SetPlayerBlockingInteractable("SubControls", true);
        PlayerController.Instance.SetCameraBlockingInteractables("SubControls", true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isInteracted = true;

        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().SetTarget(target);
        }
    }

    public void SetMap(MapItem map)
    {
        map.SetPosition(_mapItemParent);
    }

    public void Uninteract()
    {
        

        PlayerController.Instance.SetPlayerBlockingInteractable("SubControls", false);
        PlayerController.Instance.SetCameraBlockingInteractables("SubControls", false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        isInteracted = false;

        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().ResetTarget();
        }
    }
}
