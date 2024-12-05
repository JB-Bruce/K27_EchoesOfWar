using UnityEngine;

public class TestCameraScript : MonoBehaviour
{
    public Transform target;
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (transform.position != target.position)
        {
          cameraTransform.position= Vector3.Lerp(cameraTransform.position, target.position, Time.deltaTime);
        }

        if (transform.rotation.eulerAngles != cameraTransform.rotation.eulerAngles)
        {
            cameraTransform.rotation = Quaternion.Slerp(transform.rotation,target.rotation, Time.deltaTime);
        }
    }
}
