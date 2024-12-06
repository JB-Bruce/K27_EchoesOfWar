using UnityEngine;

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

    [SerializeField] private float _collisionArea;
    [SerializeField] private float _nearDetectionArea;
    [SerializeField] private float _farDetectionArea;

    private void Start()
    {
        _submarineZoomInButton.OnButtonPressed.AddListener(_sonar.ZoomIn);
        _submarineZoomOutButton.OnButtonPressed.AddListener(_sonar.ZoomOut);
        
        _mapManager.InitArea("Collision",      _collisionArea,     Shape.FilledCircle, _submarineBody.OnCollision);
        _mapManager.InitArea("Near Detection", _nearDetectionArea, Shape.Circle,       () => {});
        _mapManager.InitArea("Far Detection",  _farDetectionArea,  Shape.Circle,       () => {});
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
    }

    public void SetMovement(float direction)
    {
        _thrusterLever.SetMovement(direction);
        _submarineBody.SetThrust(_thrusterLever.GetThrust());
    }

    public void Rotate(float direction)
    {
        _rudder.SetRotation(direction);
        _submarineBody.Rotate(_rudder.Angle * Time.deltaTime);
    }
}
