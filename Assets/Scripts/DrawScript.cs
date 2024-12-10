using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrawScript : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private InputAction _action;
    [SerializeField] private RawImage _image;
    
    private Vector3 _intersection;

    private void OnEnable()
    {
        _action.Enable();
        _action.performed += Draw;
    }

    private void OnDisable()
    {
        _action.Disable();
        _action.performed -= Draw;
    }

    private void Draw(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0)
        {
            Debug.Log(MainCamera.ScreenToWorldPoint(Input.mousePosition));
            LinePlaneIntersection(out _intersection, MainCamera.ScreenToWorldPoint(Input.mousePosition),
                    MainCamera.transform.forward,
                    _image.transform.position, _image.transform.forward);
            
            Debug.Log(_intersection);
            
        }
    }
    
    private static bool LinePlaneIntersection(out Vector3 intersection, Vector3 linePoint, Vector3 lineDir, Vector3 planePoint, Vector3 planeNormal)
    {
        intersection = Vector3.zero;

        // Calcul du dénominateur
        float denom = Vector3.Dot(planeNormal, lineDir);

        // Vérifier si la droite est parallèle au plan
        if (Mathf.Abs(denom) < 1e-6f)
        {
            return false; // Pas d'intersection (parallèle)
        }

        // Calcul du paramètre t
        float t = Vector3.Dot(planeNormal, planePoint - linePoint) / denom;

        // Calculer le point d'intersection
        intersection = linePoint + t * lineDir;
        return true;
    }
}
