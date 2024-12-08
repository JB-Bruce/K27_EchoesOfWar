using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Transform playerTarget;
    private Transform cameraTransform;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _zoomSpeed = 10f;

    private void Awake()
    {
        cameraTransform = transform;
    }

    private void Start()
    {
        ResetTarget();
    }

    private void Update()
    {
        cameraTransform.position= Vector3.Lerp(cameraTransform.position, target.position, Time.deltaTime*_zoomSpeed);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, target.rotation, Time.deltaTime*_rotationSpeed);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        //transform.parent = target;
    }

    public void ResetTarget()
    {
        SetTarget(playerTarget);
    }
}
