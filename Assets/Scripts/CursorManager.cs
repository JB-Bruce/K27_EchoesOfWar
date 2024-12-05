using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Canvas interactionNotification;
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hitInfo, 10f))
        { 
            //trying to get component Interactable interface
            //if the gameObject has it, we stock the component in variable i
            if(hitInfo.transform.TryGetComponent<Interactable>(out Interactable i))
            {
                interactionNotification.gameObject.SetActive(true);
                Debug.Log("There is an object");
            }
        }
        else 
        {
            interactionNotification.gameObject.SetActive(false);
        }
    }
}
