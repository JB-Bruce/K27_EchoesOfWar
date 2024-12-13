using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class Wire : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    public Transform End;

    private Camera _camera;
    private readonly UnityEvent _linked = new();

    private Vector3 _initPos;
    private Vector3 _initScale;

    void Awake()
    {
        _camera = Camera.main;
        _transform = GetComponent<Transform>();

        _initPos = transform.localPosition;
        _initScale = transform.localScale;
    }
    private void Update()
    {
       
    }
    private void ResetWire()
    {
        _transform.position = _initPos;
        _transform.right = Vector3.zero;
    }

    private void OnMouseDrag()
    {
        Vector3 NewPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _transform.position = NewPosition;
        Debug.Log("hfo");
    }

    private void OnMouseDown()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (ray.Equals(End))
        {
            _linked.Invoke();
        }
        else
        {
            ResetWire();
        }
    }

    public UnityEvent Linked => _linked;
}
