using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    [SerializeField] private List<Light> _lights;
    [SerializeField] private List<Light> _alarmLights;
    
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _alarmColor = Color.red;
    [SerializeField] private Color _noElectricityColor = Color.black;

    private ElectricityMode _electricityMode;
    
    private void Start()
    {
        foreach (var l in _lights)
        {
            l.color = _defaultColor;
        }

        foreach (var al in _alarmLights)
        {
            al.color = _alarmColor;
        }
    }

    public void UpdateElectricityMode(ElectricityMode NewElectricityMode)
    {
        if (_electricityMode == NewElectricityMode)
            return;

        _electricityMode = NewElectricityMode;
        ElectricityOnOff(_electricityMode == ElectricityMode.On);
    }

    private void ElectricityOnOff(bool onOff)
    {
        foreach (var l in _lights)
        {
            l.color = onOff ? _defaultColor : _noElectricityColor;
        }
        
        Alarm(true);
    }

    public void Alarm(bool isOn)
    {
        foreach (var al in _alarmLights)
        {
            al.gameObject.SetActive(isOn);
        }
    }
}
