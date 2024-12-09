using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    [SerializeField] private List<Light> _lights;
    [SerializeField] private List<Light> _alarmLights;
    
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _alarmColor = Color.red;
    [SerializeField] private Color _noElectricityColor = Color.black;

    [SerializeField, Range(0,1.5f)] private float _maxTimeBetweenEachLightTurnOnOff = 1;
    
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

    private void ElectricityOnOff(bool isOn)
    {
        StartCoroutine(ProgressivelyElectricityOnOff(isOn));
        
        Alarm(!isOn, true);
    }

    public void Alarm(bool isOn, bool instantly)
    {
        if (instantly)
            AlarmInstantly(isOn);
        else 
            AlarmWait(isOn);
    }

    private void AlarmInstantly(bool isOn)
    {
        foreach (var al in _alarmLights)
        {
            al.gameObject.SetActive(isOn);
        }
    }

    private void AlarmWait(bool isOn)
    {
        StartCoroutine(WaitAndAlarm(isOn));
    }

    private IEnumerator WaitAndAlarm(bool isOn)
    {
        float elapsedTime = 0;

        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        AlarmInstantly(isOn);
    }
    
    private IEnumerator ProgressivelyElectricityOnOff(bool isOn)
    {
        List<Light> lights = new List<Light>(_lights);
        lights = lights.OrderBy(x => Random.value).ToList();
        
        while (lights.Count > 0)
        {
            float timeToWait = Random.Range(0, _maxTimeBetweenEachLightTurnOnOff);
            float elapsedTime = 0;

            while (elapsedTime < timeToWait)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            int index = Random.Range(0, lights.Count);
            lights[index].color = isOn ? _defaultColor : _noElectricityColor;
            lights.RemoveAt(index);
            
            yield return null;
        }
    }
}
