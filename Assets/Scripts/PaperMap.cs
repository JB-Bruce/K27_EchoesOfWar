using UnityEngine;

public class PaperMap : MonoBehaviour, IUninteractable
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
    
    public void Uninteract()
    {
        if (Camera.main != null)
        {
            Camera.main.transform.GetComponent<CameraScript>().target = Player;
            Camera.main.transform.SetParent(Player);
        }
    }
}
