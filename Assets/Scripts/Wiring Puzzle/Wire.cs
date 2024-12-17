using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Wire : MonoBehaviour
{
    public Transform end;
    public GameObject mesh;
    public GameObject symbole;
    public bool isLinked = false;
    [HideInInspector] public Vector3 InitPos;

    void Awake()
    {
        InitPos = transform.position;
    }
}
