using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas _interactionNotification;

    private GameObject _item; // Variable that is use to stock the gameObject we are hovering by the camera
    // Update is called once per frame

    void Start()
    {
        _camera = Camera.main; // get the camera
    }
    void Update()
    {
        bool touched = false;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, 10f))
        { 
            //trying to get component Interactable interface
            if(hitInfo.transform.TryGetComponent<IInteractable>(out IInteractable i))
            {
                i.SetOutline(true);
                _interactionNotification.gameObject.SetActive(true);
                _item = hitInfo.transform.gameObject; // stock our item
                touched = true;
            } 
        }

        if (!touched && !_item)
        {
            _interactionNotification.gameObject.SetActive(false);
            _item.GetComponent<IInteractable>().SetOutline(false);
            _item = null; // empty our variable
        }
    }
}
