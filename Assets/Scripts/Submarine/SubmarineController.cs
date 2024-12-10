using TMPro;
using UnityEngine;

public class SubmarineController : MonoBehaviour, IElectricity
{
    [SerializeField] private SubmarineBody _submarineBody;
    [SerializeField] private MapManager _mapManager;
    
    [Header("Submarine Movement")]
    [SerializeField] private ThrusterLever _thrusterLever;
    [SerializeField] private Rudder _rudder;
    
    [Header("Sonar")]
    [SerializeField] private Sonar _sonar;
    [SerializeField] private SubmarineButton _submarineZoomInButton;
    [SerializeField] private SubmarineButton _submarineZoomOutButton;

    [Header("Props")]
    [SerializeField] private Transform _submarineCompas;
    [SerializeField] private TextMeshProUGUI _submarineSpeedometer;

    [Header("Detection")]
    [SerializeField] private float _collisionArea;
    [SerializeField] private float _nearDetectionArea;
    [SerializeField] private float _farDetectionArea;
    [SerializeField] private float _goalThreshold;
    
    private Vector2 _GoalPosition;
    private bool _inGoalRange;

    private void Start()
    {
        _submarineZoomInButton.OnButtonPressed.AddListener(_sonar.ZoomIn);
        _submarineZoomOutButton.OnButtonPressed.AddListener(_sonar.ZoomOut);
        _submarineBody.SetPosition(_mapManager.GetSpawnPoint());
        _GoalPosition = _mapManager.GetGoalPoint();
        _mapManager.InitArea("Collision",      _collisionArea,     Shape.FilledCircle, _submarineBody.OnCollision);
        //_mapManager.InitArea("Near Detection", _nearDetectionArea, Shape.Circle,       () => {});
        _mapManager.InitArea("Far Detection",  _farDetectionArea,  Shape.Circle,       () => {});

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
    }

    private void Update()
    {
        if (!hasElectricity)
        {
            
        }
        _sonar.SetSubmarinePosition(_submarineBody.Position);
        _mapManager.SetSubPos(_submarineBody.Position);
        
        _mapManager.Tick();
        _submarineBody.Tick();
        
        if (Vector2.Distance(_submarineBody.Position, _GoalPosition) <= _goalThreshold && !_inGoalRange )
        {
            Debug.Log("Goal reached");
            _inGoalRange = true;
        }

        if (Vector2.Distance(_submarineBody.Position, _GoalPosition) > _goalThreshold && _inGoalRange)
        {
            Debug.Log("Goal left");
            _inGoalRange = false;
        }
        
        Rotate();
    }

    public void SetMovement(float direction)
    {
        _thrusterLever.SetMovement(direction);
        _submarineBody.SetThrust(hasElectricity ? _thrusterLever.GetRealThrust() : 0);
    }

    public void SetRotation(float angle)
    {
        _submarineBody.SetRotation(hasElectricity ? angle : 0);
    }

    private void Rotate()
    {
        _rudder.SetRotation(-_submarineBody.Angle);
        //_submarineBody.Rotate(_rudder.Angle * Time.deltaTime);
        //_submarineBody.AddRotation(direction);
        _submarineCompas.localRotation = Quaternion.Euler(0, 0, _submarineBody.Angle);
        _sonar.transform.GetChild(0).GetChild(1).localRotation = Quaternion.Euler(0, 0, _submarineBody.Angle);
        
        _submarineSpeedometer.text = Mathf.RoundToInt(_submarineBody.Velocity.magnitude * 300).ToString() + "kn";
    }

    public bool hasElectricity { get; set; }

    public void Electricity(bool hasElectricity)
    {
        if (hasElectricity)
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
}
