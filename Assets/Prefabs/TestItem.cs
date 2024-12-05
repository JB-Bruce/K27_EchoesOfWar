using UnityEngine;

public class TestItem : MonoBehaviour, IInteractable
{
    public ScriptableObject scriptableObject; // may need change of access


    public void Interact(){

    }

    public bool DoesNeedToStopPlayerMovement { get; }
}
