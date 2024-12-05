using UnityEngine;

public class PlayerInteractions : MonoBehaviour 
{
    [Header("Camera Reference")]
    [SerializeField] private Camera camera; // reference to the camera

    public Canvas interactionNotification;

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // if left mouse button is clicked 
        {
            // if the ray cast has collided with a gameObject within 10 meters in front of the camera
            // get in hitInfo the informations about the gameObject
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hitInfo, 10f))
            { 
                //trying to get component Interactable interface
                //if the gameObject has it, we stock the component in variable i
                if(hitInfo.transform.TryGetComponent<Interactable>(out Interactable i))
                {
                    Debug.Log("HITTEN");
                }
            } 
        }
    }
}
