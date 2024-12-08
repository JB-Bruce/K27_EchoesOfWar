using System;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class TestItem : MonoBehaviour, IInteractable
{
    public ScriptableObject scriptableObject; // may need change of access

    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void Interact(){

    }

    public Outline outline => _outline;

    public string interactableName => throw new NotImplementedException();
}
