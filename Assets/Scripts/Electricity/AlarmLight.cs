using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private bool _isActive;
    private MeshRenderer _meshRenderer;
    private Transform _alarm;
    Transform _alm;
    private Light[] _lights;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _alarm = GetComponentsInChildren<Transform>()[1];
        _lights = GetComponentsInChildren<Light>();

        _alm = transform.GetChild(0);
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

        //_alarm.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        _alm.localRotation = Quaternion.Euler(0, 0, _alm.localEulerAngles.z + rotationSpeed * Time.deltaTime);
    }

    public void SetActive(bool active)
    {
        _isActive = active;
        _alarm.gameObject.SetActive(active);
        
        if (active)
            _meshRenderer.materials[1].EnableKeyword("_EMISSION");
        else 
            _meshRenderer.materials[1].DisableKeyword("_EMISSION");

        foreach (var l in _lights)
        {
            l.enabled = active;
        }
        
        enabled = active;
    }
}
