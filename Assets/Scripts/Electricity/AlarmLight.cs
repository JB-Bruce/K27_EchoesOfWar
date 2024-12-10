using System;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    private Transform _transform;
    private Light _light;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _light = GetComponent<Light>();
        _light.enabled = false;
        _light.type = LightType.Spot;
    }

    private void Update()
    {
        _transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void SetColor(Color newColor)
    {
        _light.color = newColor;
    }

    public void SetActive(bool active)
    {
        _light.enabled = active;
        enabled = active;
    }
}
