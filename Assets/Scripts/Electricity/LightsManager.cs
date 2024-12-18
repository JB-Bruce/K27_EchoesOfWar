using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightsManager : MonoBehaviour
{
    [SerializeField] private List<Light> _lights;
    [SerializeField] private List<AlarmLight> _alarmLights;
    [SerializeField] private List<Light> _nightlights;
    [SerializeField] private List<LightStateDetailed> _lightStates;
    
    [SerializeField] private Color _alarmColor = Color.red;

    [SerializeField, Range(0,1.5f)] private float _maxTimeBetweenEachLightTurnOnOff = 1;
    
    private ElectricityMode _electricityMode;
    
    private List<string> _alarmTriggers = new();

    private void Start()
    {
        LightStateDetailed lightStateDetailed = GetLightStateDetailed(LightState.Default);
        
        foreach (var l in _lights)
        {
            SetupLight(l, lightStateDetailed);
        }

        foreach (var nl in _nightlights)
        {
            nl.enabled = false;
        }

        foreach (var al in _alarmLights)
        {
            al.SetActive(false);
        }
    }

    public void UpdateElectricityMode(ElectricityMode NewElectricityMode)
    {
        if (_electricityMode == NewElectricityMode)
            return;

        _electricityMode = NewElectricityMode;
        
        UpdateLights(_electricityMode);

        /*LightState lightState = GetLightState(_electricityMode);
        LightStateDetailed lightStateDetailed = GetLightStateDetailed(lightState);
        
        ElectricityOnOff(lightStateDetailed);*/
    }

    private LightState GetLightState(ElectricityMode electricityMode)
    {
        return electricityMode switch
        {
            ElectricityMode.On => LightState.Default,
            ElectricityMode.Off => LightState.Nightlight,
            _ => LightState.Default
        };
    }

    private LightStateDetailed GetLightStateDetailed(LightState state)
    {
        foreach (var lightStateDetailed in _lightStates)
        {
            if (lightStateDetailed.state == state)
                return lightStateDetailed;
        }

        return new LightStateDetailed();
    }

    private void ElectricityOnOff(LightStateDetailed lightStateDetailed)
    {
        StartCoroutine(ProgressivelyElectricityOnOff(lightStateDetailed));
    }
    
    private IEnumerator ProgressivelyElectricityOnOff(LightStateDetailed lightStateDetailed)
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
            SetupLight(lights[index], lightStateDetailed);
            lights.RemoveAt(index);
            
            yield return null;
        }
    }

    private void SetupLight(Light light, LightStateDetailed lightStateDetailed)
    {
        light.color = lightStateDetailed.color;
        light.intensity = lightStateDetailed.intensity;
        light.range = lightStateDetailed.range;
    }

    private void UpdateLights(ElectricityMode electricityMode)
    {
        StartCoroutine(ProgressivelyUpdateLights(/*new(_lights)*/_lights, electricityMode == ElectricityMode.On));
        StartCoroutine(ProgressivelyUpdateLights(/*new(_nightlights)*/_nightlights, electricityMode != ElectricityMode.On));
    }

    private IEnumerator ProgressivelyUpdateLights(List<Light> lights, bool isOn)
    {
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
            lights[index].enabled = isOn;
            lights.RemoveAt(index);
            yield return null;
        }
    }

    public void Alarm(string trigger, bool isOn, bool instantly)
    {
        if (isOn)
        {
            if (!_alarmTriggers.Contains(trigger))
                _alarmTriggers.Add(trigger);
            
            if (instantly)
                AlarmInstantly(true);
            else 
                AlarmWait(true);
        }
        else
        {
            if (_alarmTriggers.Contains(trigger))
                _alarmTriggers.Remove(trigger);

            if (_alarmTriggers.Count != 0) 
                return;
            
            if (instantly)
                AlarmInstantly(false);
            else 
                AlarmWait(false);
        }
    }

    private void AlarmInstantly(bool isOn)
    {
        foreach (var al in _alarmLights)
        {
            al.SetActive(isOn);
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
    
    [Serializable]
    private struct LightStateDetailed
    {
        public string name;
        public LightState state;
        public Color color;
        public float intensity;
        public float range;
    }

    private enum LightState
    {
        Default, Nightlight
    }
}
