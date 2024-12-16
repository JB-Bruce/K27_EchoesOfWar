using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Wire : MonoBehaviour
{
    public Transform End;
    public Transform Transform;
    public bool IsLinked = false;
    [HideInInspector] public Vector3 InitPos;
    [HideInInspector] public Vector3 InitScale;
    [HideInInspector] public float InitZ;
    

    void Awake()
    {
        InitPos = transform.localPosition;
        InitScale = transform.localScale;
        InitZ = Camera.main.WorldToScreenPoint(transform.position).z;
    }
}
