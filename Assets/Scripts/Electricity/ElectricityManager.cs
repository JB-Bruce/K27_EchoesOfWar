using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ElectricityManager : MonoBehaviour, IBreakdownReceiver
{
    [Header("Working with Electricity")]
    [SerializeField] private LightsManager _lightsManager;
    [SerializeField] private List<GameObject> _electricityObjects;
    [SerializeField] private List<MonoBehaviour> _electricityComponents;
    
    [Header("Wait")]
    [SerializeField, Tooltip("Time in second")] private float _timeElectricityWorking = 5;

    [Header("Shut Down")]
    [SerializeField, Range(0, 100)] private int _shutDownProbability = 2;
    [SerializeField, Range(0, 60)] private float _timeBetweenTryShutDown = 1;
    [SerializeField, Range(0, 1.5f)] private float _maxTimeBetweenEachTurnOnOff = 0.5f;
    
    private ElectricityMode _electricityMode;
    private WaitForSeconds _waitForTryShutDown;
    private WaitForSeconds _waitElectricityWorking;
    private bool _canShutDown;
    private bool _isWaitingForShutDown;
    private readonly List<Action<bool>> _shutDownActions = new();
    
    [Header("")]
    [SerializeField] private bool _isElectricityEnabled;

    private void Awake()
    {
        _waitForTryShutDown = new WaitForSeconds(_timeBetweenTryShutDown);
        _waitElectricityWorking = new WaitForSeconds(_timeElectricityWorking);
    }

    private void Start()
    {
        foreach (var electricityComponent in _electricityComponents)
        {
            IElectricity electricity = (IElectricity)electricityComponent;
            _shutDownActions.Add(isOn => electricity.SwitchElectricity(isOn));
        }

        foreach (var electricityObject in _electricityObjects)
        {
            _shutDownActions.Add(isOn => electricityObject.SetActive(isOn));
        }
    }

    private void Update()
    {
        #region CheatCode
        if (Input.GetKeyDown(KeyCode.O))
            SetElectricitySystemEnabled(true, true, ElectricityMode.On);

        if (Input.GetKeyDown(KeyCode.Space))
            UpdateElectricityMode(ElectricityMode.On);
        #endregion
        
        if (!_isElectricityEnabled)
            return;
        
        if (_electricityMode == ElectricityMode.On && !_isWaitingForShutDown)
        {
            _isWaitingForShutDown = true;
            StartCoroutine(WaitForShutDownElectricity());
        }
        
        if (_canShutDown)
        {
            _canShutDown = false;
            StartCoroutine(TryShutDownElectricity());
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
        List<Action<bool>> shutDownActions = new List<Action<bool>>(_shutDownActions);
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
        yield return _waitElectricityWorking;
        
        _isWaitingForShutDown = false;
        _canShutDown = true;
        _electricityMode = ElectricityMode.Stopping;
    }

    private IEnumerator TryShutDownElectricity()
    {
        bool shutDown = false;
        
        while (!shutDown)
        {
            shutDown = Random.Range(0, 100) < _shutDownProbability;
            yield return _waitForTryShutDown;
        }

        _electricityMode = ElectricityMode.Off;
        UpdateElectricityMode(_electricityMode);
    }

    public void SetElectricitySystemEnabled(bool Enabled, bool ChangeMode = false, ElectricityMode Mode = ElectricityMode.On)
    {
        _isElectricityEnabled = enabled;

        if (!ChangeMode) 
            return;
        
        UpdateElectricityMode(Mode);

        if (Mode == ElectricityMode.On)
        {
            StopAllCoroutines();
        }
        else if (Mode == ElectricityMode.Off)
        {
            
        }
    }

    public void Break()
    {
        StopAllCoroutines();
        UpdateElectricityMode(ElectricityMode.Off);
    }

    public bool IsBroken { get; set; } = false;
}
