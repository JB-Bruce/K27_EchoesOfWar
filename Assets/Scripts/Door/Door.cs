using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _transformDoor;
    [SerializeField] private bool isOpen = false;

    private Vector3 _initRotation;
    private Vector3 _initPosition;

    private Vector3 _newRotation;
    private Vector3 _newPosition;

    private Outline _outline;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _outline = GetComponent<Outline>();

        _initPosition = _transformDoor.position;
        _initRotation = new Vector3(_transformDoor.rotation.x, _transformDoor.rotation.y, _transformDoor.rotation.z);

        _newPosition = new Vector3(_transformDoor.position.x + 0.75f, _transformDoor.position.y, _transformDoor.position.z + 0.5f);
        _newRotation = new Vector3(_transformDoor.rotation.x, _transformDoor.rotation.y + 90, _transformDoor.rotation.z);

        StartCoroutine(((IInteractable)this).DeactivateOutline());
    }

   public void Interact()
    {
        if (isOpen)
        {
            _transformDoor.position = _initPosition;
            _transformDoor.eulerAngles = _initRotation;
            isOpen = false;
        }
        else
        {
            _transformDoor.position = _newPosition;
            _transformDoor.eulerAngles = _newRotation;
            isOpen = true;
        }
    }

    public string interactableName => "Door";

    public Outline outline => _outline;
}
