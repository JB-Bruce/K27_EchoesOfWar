using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    private Transform cameraTransform;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _zoomSpeed = 10f;

    IEnumerator CurrentShakeCoroutine;
    private void Start()
    {
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
        Vector3 startPos = cameraTransform.localPosition;
        
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            cameraTransform.localPosition = new Vector3(x, y, cameraTransform.localPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraTransform.localPosition = startPos;
        yield return null;
    }
    
    private void Update()
    {
        cameraTransform.position= Vector3.Lerp(cameraTransform.position, target.position, Time.deltaTime*_zoomSpeed);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation,target.rotation, Time.deltaTime*_rotationSpeed);
    }
}