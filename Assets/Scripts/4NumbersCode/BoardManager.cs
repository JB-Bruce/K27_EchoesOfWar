using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardManager : MonoBehaviour
{
    [SerializeField] List<NumberCase> numbers = new();
    
    private readonly UnityEvent _onCodeDiscovered = new();

    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] Light _light;
    [SerializeField] Color _onColor;
    [SerializeField] Color _offColor;

    public bool isOpen = false;
    public UnityEvent onGlassOpen = new();

    public Animator anim;

    private void ApplyColor(bool on)
    {
        Color selectedColor = on ? _onColor : _offColor;

        _meshRenderer.material.color = selectedColor;
        _meshRenderer.material.SetColor("_EmissionColor", selectedColor);
        _light.color = selectedColor;
    }


    private void Start()
    {
        ApplyColor(false);

        foreach (var item in numbers)
        {
            item.numberChangedEvent.AddListener(NumberChanged);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) Open(true);
    }

    private void NumberChanged()
    {
        if (!CheckCode()) return;
        
        _onCodeDiscovered.Invoke();
        
    }

    public void Open(bool opened)
    {
        isOpen = opened;
        ApplyColor(opened);
        if (!opened) return;
            
        onGlassOpen.Invoke();
        anim.Play("Open");
    }


    private bool CheckCode()
    {
        foreach (var numberCase in numbers)
        {
            if (!numberCase.IsCorrect)
                return false;
        }

        return true;
    }
    
    public UnityEvent OnCodeDiscovered => _onCodeDiscovered;
}
