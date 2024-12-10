using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DrawScript : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private RawImage _image;
    [SerializeField] private Transform _bottomLeft;
    [SerializeField] private Transform _topRight;
    [SerializeField] private Texture2D baseTexture;
    private Texture2D _texture;
    
    private Vector3 _intersection;
    private Vector3 _diagonal;
    private bool _isDrawing;

    private void Start()
    {
        _diagonal = _topRight.position - _bottomLeft.position;
        print("STARGTEDDDDDDDD");
        print("JFDGJGFS   " + _diagonal);
        print("STARGTEDDDDDDDD");
        Texture2D newTexture = new Texture2D(baseTexture.width, baseTexture.height, TextureFormat.RGBA32, false);
        newTexture.filterMode = FilterMode.Point;

        newTexture.SetPixels(baseTexture.GetPixels());
        newTexture.Apply();

        _texture = newTexture;

        _image.texture = newTexture;
    }

    private void OnEnable()
    {
        _image.texture = _texture;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _isDrawing = true;
        }
        else
        {
            _isDrawing = false;
        }

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
        float x = _bottomLeft.up.x;
        float  y = ToPos.y/ _diagonal.y;
        print(_diagonal);

        if (x <= 1 && y <= 1)
        {

            Debug.Log("x : " + x + " y : " + y);
            Debug.Log("diagx : " + _diagonal.x + " diagy : " + _diagonal.y);
            Debug.Log("posx : " + ToPos.x + " posy : " + ToPos.y);
            Debug.Log("diag" + _diagonal.x.ToString());
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
