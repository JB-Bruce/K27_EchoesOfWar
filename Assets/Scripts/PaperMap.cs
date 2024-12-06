using UnityEngine;

public class PaperMap : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform target;
    public void Interact()
    {
        Camera.main.transform.GetComponent<TestCameraScript>().target = target;
        Camera.main.transform.SetParent(target);
    }

    public bool DoesNeedToStopPlayerMovement { get; } = true;
}
