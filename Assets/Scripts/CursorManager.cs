using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas _interactionNotification;

    private GameObject _item; // Variable that is use to stock the gameObject we are hovering by the camera
    // Update is called once per frame
    
    private bool _isLookingAtItem = false;

    void Start()
    {
        _camera = Camera.main; // get the camera
    }
    void Update()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, 10f))
        {
            if (hitInfo.transform.TryGetComponent(out IInteractable i))
            {
                i.SetOutline(true);
                _item = hitInfo.collider.gameObject;
                _interactionNotification.gameObject.SetActive(true);
                _isLookingAtItem = true;
            }
            else
            {
                _isLookingAtItem = false;
            }
        }
        else
        {
            _isLookingAtItem = false;
        }
        
        if (_item && !_isLookingAtItem)
        {
            _interactionNotification.gameObject.SetActive(false);
            _item.GetComponent<IInteractable>().SetOutline(false);
            _item = null;
        }
    }
}
