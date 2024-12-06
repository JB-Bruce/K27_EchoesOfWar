using System.Collections;
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

    /// <summary>
    /// Coroutine that make the camera shake
    /// </summary>
    /// <param name="duration">duration of the shake</param>
    /// <param name="magnitude">intensity of the shake, the higher it more intense the shake is</param>
    /// <returns></returns>
    public IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 startPos = cameraTransform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1)*magnitude;
            float y = Random.Range(-1, 1)*magnitude;
            cameraTransform.localPosition = new Vector3( x, y, startPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraTransform.localPosition = startPos;
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
