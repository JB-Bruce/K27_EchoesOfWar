using UnityEngine;

public class PlayerInteractions : MonoBehaviour 
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _interactionRange = 10f;
    [SerializeField] private UseItem _useItem;

    IFinishedInteractable selectedInteractable = null;

    IInteractable overedInteractable = null;

    [SerializeField] GameObject _interactionCanvas;


    void Update()
    {

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _interactionRange))
        {
            if (hitInfo.transform.TryGetComponent(out IInteractable i))
            {
                if (overedInteractable != null)
                    overedInteractable.SetOutline(false);

                overedInteractable = i;
                i.SetOutline(true);
                return;
            }
        }

        if(overedInteractable != null)
        {
            overedInteractable.SetOutline(false);
            overedInteractable = null;
        }

        
    }

    public bool TryInteract()
    {
        if (overedInteractable == null || _useItem.activated) 
        {
            if (selectedInteractable != null && selectedInteractable.isInteracted)
            {
                Cancel();
                return true;

            }
			return false;
        }

        


        if (overedInteractable is IFinishedInteractable finishedInteractable)
        {
            if (selectedInteractable != null)
            {
                if(selectedInteractable.interactableName != finishedInteractable.interactableName)
                    return false;
                selectedInteractable = null;
            }
            else
            {
                overedInteractable.Interact();
                if(finishedInteractable.isInteracted)
                    selectedInteractable = finishedInteractable;
                return true;
            }
        }

        overedInteractable.Interact();
        return true;
    }

    public void Cancel()
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Uninteract();
            selectedInteractable = null;
        }
    }
}
