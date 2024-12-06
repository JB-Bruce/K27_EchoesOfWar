using UnityEngine;

public class PaperMap : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform target;
    public void Interact()
    {
        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().target = target;
            Camera.main.transform.SetParent(target);
        }
    }

    public bool DoesNeedToStopPlayerMovement { get; } = false;
}
