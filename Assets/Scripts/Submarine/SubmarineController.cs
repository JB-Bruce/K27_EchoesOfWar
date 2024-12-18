using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SubmarineController : MonoBehaviour, IElectricity, IBreakdownCaster
{
    [SerializeField] private SubmarineBody _submarineBody;
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private LightsManager _lightsManager;
    [SerializeField] private EmergenceSystem _emergenceSystem;
    
    [Header("Submarine Movement")]
    [SerializeField] private ThrusterLever _thrusterLever;
    [SerializeField] private Rudder _rudder;
    
    [Header("Sonar")]
    [SerializeField] private Sonar _sonar;
    [SerializeField] private GameObject _sonarScreenOn;
    [SerializeField] private SubmarineButton _submarineZoomInButton;
    [SerializeField] private SubmarineButton _submarineZoomOutButton;

    [Header("Props")]
    [SerializeField] private Transform _submarineCompas;
    [SerializeField] private TextMeshProUGUI _submarineSpeedometer;

    [Header("Detection")]
    [SerializeField] private SubmarineButton _submarineDetectionButton;
    [SerializeField] private float _collisionArea;
    [SerializeField] private float _nearDetectionArea;
    [SerializeField] private float _farDetectionArea;
    [SerializeField] private float _goalThreshold;
    
    [Header("Shake")]
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeIntensity;
    [SerializeField] private float _shakethreshold;
    
    
    private bool _isAlarmActivated = true;
    private bool _isWarningActivated = true;
    private string _OnCollisionTriggerName = "OnCollision";
    private string _OnFarDetectionTriggerName = "OnFarDetection";
    private bool _isCollided = false;
    
    private Vector2 _GoalPosition;
    private bool _inGoalRange;
    [SerializeField] AudioClip _audioAlarme;
    [SerializeField] AudioClip _audioColition;

    private void Start()
    {
        _submarineZoomInButton.OnButtonPressed.AddListener(_sonar.ZoomIn);
        _submarineZoomOutButton.OnButtonPressed.AddListener(_sonar.ZoomOut);
        _submarineDetectionButton.OnButtonPressed.AddListener(SwitchWarningOnOff);
        
        _submarineBody.SetPosition(_mapManager.GetSpawnPoint());
        _GoalPosition = _mapManager.GetGoalPoint();
        
        _sonar.SetMapTexture(_mapManager.GetMapTexture());
        
        _mapManager.InitArea("Collision",     _collisionArea,    Shape.FilledCircle, OnSubmarineCollisionEnter,    OnSubmarineCollisionExit,   OnSubmarineCollisionEnterConstant);
        _mapManager.InitArea("Far Detection", _farDetectionArea, Shape.Circle,       OnSubmarineFarDetectionEnter, OnSubmarineFarDetectionExit);

        hasElectricity = true;
    }

    public void SetControls(bool isActive)
    {
        _thrusterLever.isInteracted = isActive;
        _rudder.isInteracted = isActive;
    }

    private void OnDestroy()
    {
        _submarineZoomInButton.OnButtonPressed.RemoveListener(_sonar.ZoomIn);
        _submarineZoomOutButton.OnButtonPressed.RemoveListener(_sonar.ZoomOut);
        _submarineDetectionButton.OnButtonPressed.RemoveListener(SwitchWarningOnOff);
    }

    private void Update()
    {
        _sonar.SetSubmarinePosition(_submarineBody.Position);
        _mapManager.SetSubPos(_submarineBody.Position);
        _submarineBody.SetThrust(hasElectricity ? _thrusterLever.GetRealThrust() : 0);

        _submarineBody.TickMove();

        if (!_isCollided)
            _submarineBody.Tick();

        Vector2 newLastPos = _submarineBody.Position;
        if (_mapManager.Tick(ref newLastPos)) _submarineBody.SetLastOk(newLastPos);


        if (Mathf.Abs(_thrusterLever.GetRealThrust()) > _shakethreshold)
        {
            Camera.main.GetComponent<CameraScript>().constantShake();
           
            AudioManageur.Instance.PlayClipAt(_audioAlarme,transform.position);
        }
        else if (!_isCollided)
        {
            Camera.main.GetComponent<CameraScript>().StopShake();

            Camera.main.GetComponent<CameraScript>().StopShake();
        }

        if (Vector2.Distance(_submarineBody.Position, _GoalPosition) <= _goalThreshold && !_inGoalRange )
        {
            _inGoalRange = true;
            _emergenceSystem.IsAtGoodPosition(true);
        }

        if (Vector2.Distance(_submarineBody.Position, _GoalPosition) > _goalThreshold && _inGoalRange)
        {
            _inGoalRange = false;
            _emergenceSystem.IsAtGoodPosition(false);
        }
        
        Rotate();
    }

    public void SetMovement(float direction)
    {
        _thrusterLever.SetMovement(direction);
    }

    public void SetRotation(float angle)
    {
        _submarineBody.SetRotation(angle);
    }

    private void Rotate()
    {
        _rudder.SetRotation(-_submarineBody.Angle);
        _submarineCompas.localRotation = Quaternion.Euler(0, 0, _submarineBody.Angle);
        _sonarScreenOn.transform.localRotation = Quaternion.Euler(0, 0, _submarineBody.Angle);
        
        _submarineSpeedometer.text = Mathf.RoundToInt(_submarineBody.Velocity.magnitude * 300).ToString() + "<size=6>kn";
    }

    private void SwitchWarningOnOff()
    {
        _isWarningActivated = !_isWarningActivated;
        
        bool isAlarmOn = _isAlarmActivated && _isWarningActivated;
        
        _lightsManager.Alarm(_OnCollisionTriggerName, isAlarmOn, true);
        _lightsManager.Alarm(_OnFarDetectionTriggerName, isAlarmOn, true);
    }

    private void OnSubmarineCollisionEnter()
    {
        _thrusterLever.SetMovement(0);
        _thrusterLever.ResetThrust();

        AudioManageur.Instance.PlayClipAt(_audioColition,transform.position);
        _isCollided = true;
        Camera.main.GetComponent<CameraScript>().startShake(_shakeDuration,_shakeIntensity);
        _submarineBody.OnCollision();
        OnBreakDown.Invoke();
        
        _isAlarmActivated = true;
        
        if (_isWarningActivated)
            _lightsManager.Alarm(_OnCollisionTriggerName, true, true);
    }

    private void OnSubmarineCollisionEnterConstant()
    {
        _submarineBody.OnCollisionConstant();
    }

    private void OnSubmarineCollisionExit()
    {
        _isCollided = false;
        _isAlarmActivated = false;
        
        if (_isWarningActivated)
            _lightsManager.Alarm(_OnCollisionTriggerName, false, true);
    }

    private void OnSubmarineFarDetectionEnter()
    {
        _isAlarmActivated = true;
        
        if (_isWarningActivated)
            _lightsManager.Alarm(_OnFarDetectionTriggerName, true, true);
    }
    
    private void OnSubmarineFarDetectionExit()
    {
        _isAlarmActivated = false;
        
        if (_isWarningActivated)
            _lightsManager.Alarm(_OnFarDetectionTriggerName, false, true);
    }

    public bool hasElectricity { get; set; }

    public void SwitchElectricity(bool HasElectricity)
    {
        if (HasElectricity)
            EnableElectricity();
        else 
            DisableElectricity();
    }

    public void EnableElectricity()
    {
        hasElectricity = true;
    }

    public void DisableElectricity()
    {
        hasElectricity = false;
    }

    public UnityEvent OnBreakDown { get; set; } = new();
}
