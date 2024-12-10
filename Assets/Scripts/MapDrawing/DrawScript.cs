using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DrawScript : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [FormerlySerializedAs("_action")] [SerializeField] private InputAction leftClick;
    [SerializeField] private RawImage _image;
    [SerializeField] private Transform _bottomLeft;
    [SerializeField] private Transform _topRight;
    private Texture2D _texture;
    
    private Vector3 _intersection;
    private Vector3 diag;
    private bool _isDrawing;

    private void Start()
    {
        diag = _topRight.position - _bottomLeft.position;
        _texture = new Texture2D(_image.texture.width,_image.texture.height, TextureFormat.RGBA32, false);
    }

    private void OnEnable()
    {
        leftClick.Enable();
        leftClick.performed += Draw;
        _image.texture = _texture;
    }

    private void OnDisable()
    { 
        leftClick.Disable();
        leftClick.performed -= Draw;
    }

    private void Draw(InputAction.CallbackContext context)
    {
        if (context.performed )
        {
                _isDrawing = true;
        }
    }

    private void Update()
    {
        if (_isDrawing)
        {
            var ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            if (
                LinePlaneIntersection(out _intersection,
                    ray.origin,
                    ray.direction,
                    _image.transform.position,
                    _image.transform.forward))
            {
                SetTexture(Color.white, _intersection);
            }
        }
    }

    private void SetTexture(Color color, Vector3 position)
    {
        Vector3 ToPos = position - _bottomLeft.position;
        float x = ToPos.x/diag.x;
        float  y = ToPos.y/diag.y;

        if (x <= 1 && y <= 1)
        {
            Debug.Log("drawing texture");
            _texture.SetPixel(Mathf.RoundToInt(x*_texture.width),Mathf.RoundToInt(y*_texture.height),color);
            _texture.Apply();
            _image.texture = _texture;
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
