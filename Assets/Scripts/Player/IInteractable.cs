using System.Collections;
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

    public IEnumerator DeactivateOutline()
    {
        yield return new WaitForSeconds(0.1f);
        outline.enabled = false;
    }

    void Interact() {}

    Outline outline { get; }
}