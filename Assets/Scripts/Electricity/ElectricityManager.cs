using UnityEngine;

public class ElectricityManager : MonoBehaviour
{
    [SerializeField] private LightsManager _lightsManager;
    [SerializeField] private Sonar _sonar;

    [SerializeField] private ElectricityMode _newElectricityMode;
    private ElectricityMode _electricityMode;

    private void Update()
    {
        if (_electricityMode != _newElectricityMode)
        {
            UpdateElectricityMode();
        }
    }

    private void UpdateElectricityMode()
    {
        _electricityMode = _newElectricityMode;
        
        if (_electricityMode == ElectricityMode.On)
            ElectricityOnOff(true);
        else if (_electricityMode == ElectricityMode.Off)
            ElectricityOnOff(false);
    }

    private void ElectricityOnOff(bool isOn)
    {
        if (isOn)
        {
            _lightsManager.UpdateElectricityMode(ElectricityMode.On);
            _sonar.Activate();
        }
        else
        {
            _lightsManager.UpdateElectricityMode(ElectricityMode.Off);
            _sonar.Deactivate();
        }
    }
}
