using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ElectricityManager : MonoBehaviour
{
    [SerializeField] private LightsManager _lightsManager;
    [SerializeField] private List<GameObject> _electricityObjects;
    [SerializeField] private List<MonoBehaviour> _electricityComponents;

    [SerializeField, Range(0, 100)] private int _shutDownProbability = 2;
    [SerializeField, Range(0, 5)] private float _timeBetweenTryShutDown = 1;
    [SerializeField] private float _timeBetweenEachShutDown = 5;
    
    private ElectricityMode _electricityMode;
    private WaitForSeconds _waitForSeconds;
    private bool _canShutDown;
    private bool _isWaitingForShutDown;
    private List<Action<bool>> _shutDownActions = new List<Action<bool>>();
    [SerializeField, Range(0, 1.5f)] private float _maxTimeBetweenEachTurnOnOff = 0.5f;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_timeBetweenTryShutDown);
    }

    private void Start()
    {
        foreach (var electricityComponent in _electricityComponents)
        {
            _shutDownActions.Add(isOn => electricityComponent.enabled = isOn );
        }

        foreach (var electricityObject in _electricityObjects)
        {
            _shutDownActions.Add(isOn => electricityObject.SetActive(isOn));
        }
    }

    private void Update()
    {
        if (_electricityMode == ElectricityMode.On && !_isWaitingForShutDown)
        {
            _isWaitingForShutDown = true;
            StartCoroutine(WaitForShutDownElectricity());
        }
        
        if (_canShutDown)
        {
            _canShutDown = false;
            _electricityMode = ElectricityMode.Stopping;
            StartCoroutine(TryShutDownElectricity());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateElectricityMode(ElectricityMode.On);
        }
    }

    public void UpdateElectricityMode(ElectricityMode newElectricityMode)
    {
        _electricityMode = newElectricityMode;

        switch (_electricityMode)
        {
            case ElectricityMode.On:
                ElectricityOnOff(true);
                break;
            case ElectricityMode.Off:
                ElectricityOnOff(false);
                break;
        }
    }

    private void ElectricityOnOff(bool isOn)
    {
        StartCoroutine(ProgressivelyElectricityOnOff(isOn));
        
        _lightsManager.UpdateElectricityMode(isOn ? ElectricityMode.On : ElectricityMode.Off);
    }

    private IEnumerator ProgressivelyElectricityOnOff(bool isOn)
    {
        List<Action<bool>> shutDownActions = new List<Action<bool>>();
        shutDownActions = shutDownActions.OrderBy(_ => Random.value).ToList();

        while (shutDownActions.Count > 0)
        {
            float timeToWait = Random.Range(0, _maxTimeBetweenEachTurnOnOff);
            float elapsedTime = 0;

            while (elapsedTime < timeToWait)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            int index = Random.Range(0, shutDownActions.Count);
            shutDownActions[index].Invoke(isOn);
            shutDownActions.RemoveAt(index);
        }
    }

    private IEnumerator WaitForShutDownElectricity()
    {
        float elapsedTime = 0;
        while (elapsedTime < _timeBetweenEachShutDown)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _isWaitingForShutDown = false;
        _canShutDown = true;
    }

    private IEnumerator TryShutDownElectricity()
    {
        bool shutDown = false;
        
        while (!shutDown)
        {
            shutDown = Random.Range(0, 100) < _shutDownProbability;
            yield return _waitForSeconds;
        }

        _electricityMode = ElectricityMode.Off;
        UpdateElectricityMode(ElectricityMode.Off);
    }
}
