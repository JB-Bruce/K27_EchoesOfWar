using UnityEngine;
public interface IInteractable
{
    void SetOutline(bool Bool) 
    { 
        if (outline != null)
        {
            outline.enabled = Bool;
        } 
    }

    void Interact();

    bool DoesNeedToStopPlayerMovement { get; }

    Outline outline { get; }
}