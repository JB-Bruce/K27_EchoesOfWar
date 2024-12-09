using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SubmarineController : MonoBehaviour
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

    [SerializeField] private Transform _submarineCompas;
    [SerializeField] private TextMeshProUGUI _submarineSpeedometer;

    [SerializeField] private float _collisionArea;
    [SerializeField] private float _nearDetectionArea;
    [SerializeField] private float _farDetectionArea;
    [FormerlySerializedAs("_GoalThreshold")] [SerializeField] private float _goalThreshold;
    
    private Vector2 _GoalPosition;
    private bool _inGoalRange;

    private void Start()
    {
        _submarineZoomInButton.OnButtonPressed.AddListener(_sonar.ZoomIn);
        _submarineZoomOutButton.OnButtonPressed.AddListener(_sonar.ZoomOut);
        _submarineBody.SetPosition(_mapManager.GetSpawnPoint());
        _GoalPosition = _mapManager.GetSpawnPoint();
        _mapManager.InitArea("Collision",      _collisionArea,     Shape.FilledCircle, _submarineBody.OnCollision);
        //_mapManager.InitArea("Near Detection", _nearDetectionArea, Shape.Circle,       () => {});
        _mapManager.InitArea("Far Detection",  _farDetectionArea,  Shape.Circle,       () => {});
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
        _sonar.SetSubmarinePosition(_submarineBody.Position);
        _mapManager.SetSubPos(_submarineBody.Position);
        
        _mapManager.Tick();
        _submarineBody.Tick();
        if (Vector2.Distance(_submarineBody.Position, _GoalPosition) < _goalThreshold && !_inGoalRange )
        {
            _inGoalRange = true;
        }

        if (Vector2.Distance(_submarineBody.Position, _GoalPosition) > _goalThreshold && _inGoalRange)
        {
            _inGoalRange = false;
        }
        Rotate();
    }

    public void SetMovement(float direction)
    {
        _thrusterLever.SetMovement(direction);
        _submarineBody.SetThrust(_thrusterLever.GetRealThrust());
    }

    public void SetRotation(float angle)
    {
        _submarineBody.SetRotation(angle);
    }

    private void Rotate()
    {
        _rudder.SetRotation(-_submarineBody.Angle);
        //_submarineBody.Rotate(_rudder.Angle * Time.deltaTime);
        //_submarineBody.AddRotation(direction);
        _submarineCompas.localRotation = Quaternion.Euler(0, 0, _submarineBody.Angle);
        _sonar.transform.GetChild(0).GetChild(0).localRotation = Quaternion.Euler(0, 0, _submarineBody.Angle);
        
        _submarineSpeedometer.text = Mathf.RoundToInt(_submarineBody.Velocity.magnitude * 300).ToString() + "kn";
    }
}
