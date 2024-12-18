using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private bool _isActive;
    private MeshRenderer _meshRenderer;
    private Transform _alarm;
    private Light[] _lights;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _alarm = GetComponentInChildren<Transform>();
        _lights = GetComponentsInChildren<Light>();
    }

    private void Start()
    {
        foreach (var l in _lights)
            l.type = LightType.Spot;
    }

    private void Update()
    {
        if (!_isActive)
            return;
        
        _alarm.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void SetActive(bool active)
    {
        _isActive = active;
        _alarm.gameObject.SetActive(active);
        
        if (active)
            _meshRenderer.materials[1].EnableKeyword("_EMISSION");
        else 
            _meshRenderer.materials[1].DisableKeyword("_EMISSION");
        
        enabled = active;
    }
}
