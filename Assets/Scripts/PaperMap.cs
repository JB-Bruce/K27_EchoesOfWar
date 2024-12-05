using UnityEngine;

public class PaperMap : MonoBehaviour, Interactable
{
    [SerializeField] private Transform target;
    public void Interact()
    {
        Camera.main.transform.GetComponent<TestCameraScript>().target = target;
    }
}
