using UnityEngine;

public class WaterStream : MonoBehaviour
{
    Vector2 stream;

    [SerializeField] float streamChangeRate;
    Vector2 targetStream;

    [SerializeField] float minStreamChangeTime;
    [SerializeField] float maxStreamChangeTime;

    float force = 0f;
    float targetForce;
    [SerializeField] float maxForce;
    [SerializeField] float forceChangeRate;

    [SerializeField] float minForceChangeTime;
    [SerializeField] float maxForceChangeTime;

    [SerializeField] float timeWithoutStreamAfterCollision;
    float timer = 0f;

    bool inCooldown = false;

    public float angle;
    bool _initialized = false;

    

    [SerializeField] TutorialManager _tuto;

    private void Awake()
    {
        _tuto.OnTutorialFinished.AddListener(Init);
    }

    public void Init()
    {
        _initialized = true;
        SetTargetForce();
        SetTargetStream();
    }

    private void Update()
    {
        if (!_initialized) return;

        if (inCooldown)
        {
            timer += Time.deltaTime;
            if(timer >= timeWithoutStreamAfterCollision)
            {
                timer = 0f;
                inCooldown = false;
            }
        }

        force = Mathf.MoveTowards(force, targetForce, forceChangeRate * Time.deltaTime);
        stream = Vector2.MoveTowards(stream, targetStream, streamChangeRate * Time.deltaTime);

        angle = Mathf.Atan2(stream.x, stream.y) * Mathf.Rad2Deg;
    }

    private void SetTargetStream()
    {
        targetStream = Random.insideUnitCircle.normalized;
        Invoke("SetTargetStream", Random.Range(minStreamChangeTime, maxStreamChangeTime));
    }

    private void SetTargetForce()
    {
        targetForce = Random.Range(0f, maxForce);
        Invoke("SetTargetForce", Random.Range(minForceChangeTime, maxForceChangeTime));
    }

    public (Vector2 dir, float force) GetStream()
    {
        return (stream.normalized, inCooldown ? 0f : force);
    }

    public void OnCollision()
    {
        timer = 0f;
        inCooldown = true;
    }
}
