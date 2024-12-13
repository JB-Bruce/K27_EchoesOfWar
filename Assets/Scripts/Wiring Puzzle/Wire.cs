using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Wire : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    public Transform End;

    private Camera _camera;
    private float CameraZDistance;
    private readonly UnityEvent _linked = new();

    private Vector3 _initPos;
    private Vector3 _initScale;

    private bool isDragging = false;

    void Awake()
    {
        _camera = Camera.main;
        CameraZDistance = _camera.WorldToScreenPoint(transform.position).z;
        _transform = GetComponent<Transform>();

        _initPos = transform.localPosition;
        _initScale = transform.localScale;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        } 
        else
        {
            isDragging = false;
        }

        if(isDragging)
        {

        } 
        else
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
    }

    private void ResetWire()
    {
        _transform.position = _initPos;
        _transform.right = Vector3.zero;
        _transform.localScale = _initScale;
    }

    private void OnMouseDrag()
    {
        Vector3 mouseScreenPosition = new Vector3(_transform.position.x, _transform.position.y, CameraZDistance);
        Vector3 NewPosition = _camera.ScreenToWorldPoint(mouseScreenPosition);
        _transform.position = NewPosition;
        Debug.Log("hfo");
    }


    public UnityEvent Linked => _linked;
}
