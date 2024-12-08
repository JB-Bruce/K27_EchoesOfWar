using UnityEngine;
public interface IInteractable
{
    public string interactableName { get; }

    void SetOutline(bool Bool) 
    { 
        if (outline != null)
        {
            outline.enabled = Bool;
        } 
    }

    void Interact() {}

    Outline outline { get; }
}