using UnityEngine;

public class TestCameraScript : MonoBehaviour
{
    public Transform target;
    private Transform cameraTransform;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _zoomSpeed = 10f;

    private void Start()
    {
        cameraTransform = transform;
    }

    private void Update()
    {
        cameraTransform.position= Vector3.Lerp(cameraTransform.position, target.position, Time.deltaTime*_zoomSpeed);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation,target.rotation, Time.deltaTime*_rotationSpeed);
    }
}
