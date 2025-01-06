using System.Collections.Generic;
using UnityEngine;

public class ActivationLight : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] List<Light> _lights;
    [SerializeField] Color _onColor;
    [SerializeField] Color _offColor;

    public void SetActivation(bool activated)
    {
        Color selectedColor = activated ? _onColor : _offColor;

        _meshRenderer.material.color = selectedColor;
        _meshRenderer.material.SetColor("_EmissionColor", selectedColor);

        foreach (var light in _lights)
        {
            light.color = selectedColor;
        }
    }
}
