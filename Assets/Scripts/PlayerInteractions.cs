using UnityEngine;

public class PlayerInteractions : MonoBehaviour 
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _interactionDistance = 10f;

    public void Interact()
    {
        if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _interactionDistance)) 
            return;
        
        if (hitInfo.transform.TryGetComponent(out IInteractable i))
            i.Interact();
    }

    public void UnInteract()
    {
        if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _interactionDistance)) 
            return;
        
        if (hitInfo.transform.TryGetComponent(out IinteractableCanEnd i))
        {
            i.Uninteract();
        }
    }

    public bool NeedToStopAllMovement()
    {
        if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _interactionDistance)) 
            return false;
        
        if (hitInfo.transform.TryGetComponent(out IinteractableCanEnd i))
        {
            return i.DoesNeedToStopAllMovement;
        }
        return false;
    }
    
    public bool NeedToStopPlayerMovement()
    {
        if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _interactionDistance)) 
            return false;

        if (hitInfo.transform.TryGetComponent(out IInteractable i))
        {
            return i.DoesNeedToStopPlayerMovement;
        }
        
        return false;
    }
}
