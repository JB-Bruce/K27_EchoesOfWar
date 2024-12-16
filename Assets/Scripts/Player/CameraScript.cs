using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Transform playerTarget;
    private Transform cameraTransform;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _zoomSpeed = 10f;

    IEnumerator CurrentShakeCoroutine;

    private void Start()
    {
        ResetTarget();
        cameraTransform = transform;
    }

    /// <summary>
    /// start the coroutine that shake the screen
    /// </summary>
    /// <param name="duration">time duration of screen shake</param>
    /// <param name="magnitude">intensity of the screen shake, the higher the farther the screen shake</param>
    public void startShake(float duration, float magnitude)
    {
        if (CurrentShakeCoroutine != null)
            StopCoroutine(CurrentShakeCoroutine);
        CurrentShakeCoroutine = CameraShake(duration, magnitude);
        StartCoroutine(CurrentShakeCoroutine);
    }
    
    private IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 startPos = cameraTransform.position;
        
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            cameraTransform.position = new Vector3(cameraTransform.position.x +x, cameraTransform.position.y +y, cameraTransform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraTransform.position = startPos;
        yield return null;
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
