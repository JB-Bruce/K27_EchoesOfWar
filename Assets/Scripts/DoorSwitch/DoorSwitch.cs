using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer;

    [SerializeField] Color _onColor;
    [SerializeField] Color _offColor;

    bool _isOn = true;

    private void Start()
    {
        _meshRenderer.material = new Material(_meshRenderer.material);
        ApplyColor();
    }

    private void ApplyColor()
    {
        _meshRenderer.material.color = _isOn ? _onColor : _offColor;
        _meshRenderer.material.SetColor("_EmissionColor", _isOn ? _onColor : _offColor);
    }

    public void SetActivation(bool isActive)
    {
        _isOn = isActive;
        ApplyColor();
    }
}
