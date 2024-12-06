using UnityEngine;

public class PaperMap : MonoBehaviour, IinteractableCanEnd
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform Player;
    public void Interact()
    {
        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().target = target;
            Camera.main.transform.SetParent(target);
        }
    }

    public bool DoesNeedToStopPlayerMovement { get; } = false;
    public Outline outline { get; }

    public bool DoesNeedToStopAllMovement { get; } = true;

    public void Uninteract()
    {
        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().target = Player;
            Camera.main.transform.SetParent(Player.GetComponentInParent<Transform>());
        }
    }
}
