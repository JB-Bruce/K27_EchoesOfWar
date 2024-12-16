using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DrawScript : MonoBehaviour
{
    [SerializeField] int drawSizeX;
    [SerializeField] int drawSizeY;

    [SerializeField] float _penSize;
    [SerializeField] float _deleteSize;

    [SerializeField] Color _drawingColor;

    private Camera MainCamera;
    [SerializeField] private RawImage _image;
    [SerializeField] private Transform _bottomLeft;
    [SerializeField] private Transform _topRight;
    [SerializeField] private Texture2D baseTexture;
    [SerializeField] private Transform _pointer;
    private Texture2D _texture;

    [SerializeField] RenderTexture _renderTexture;

    bool _isDelete;

    
    private Vector3 _intersection;
    private Vector3 _diagonal;
    private bool _isDrawing;

    private void Start()
    {
        MainCamera = Camera.main;

        RectTransform rt = _image.GetComponent<RectTransform>();
        _topRight.localPosition = new(rt.rect.width / 2f, rt.rect.height / 2f);
        _bottomLeft.localPosition = new(-rt.rect.width / 2f, -rt.rect.height / 2f);

        _diagonal = _topRight.localPosition - _bottomLeft.localPosition;
        Texture2D newTexture = new Texture2D(drawSizeX, drawSizeY, TextureFormat.RGBA32, false);
        newTexture.filterMode = FilterMode.Point;

        List<Color> cols = new();
        for (int i = 0; i < drawSizeX * drawSizeY; i++)
        {
            cols.Add(Color.clear);
        }

        newTexture.SetPixels(cols.ToArray());
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
            _isDelete = false;
        }
        else if (Input.GetMouseButton(1))
        {
            _isDrawing = true;
            _isDelete = true;
        }
        else
        {
            _isDrawing = false;
        }

        if (_isDrawing)
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos.x = mousePos.x * _renderTexture.width / Screen.width;
            mousePos.y = mousePos.y * _renderTexture.height / Screen.height;
            var ray = MainCamera.ScreenPointToRay(mousePos);
            if (
                LinePlaneIntersection(out _intersection,
                    ray.origin,
                    ray.direction,
                    _image.transform.position,
                    _image.transform.forward))
            {
                SetTexture(_isDelete ? Color.clear : _drawingColor, _intersection, _isDelete ? _deleteSize : _penSize);
            }
        }
    }

    private void SetTexture(Color color, Vector3 position, float size)
    {
        _pointer.position = position;

        Vector3 ToPos = _pointer.localPosition - _bottomLeft.localPosition;

        float x = ToPos.x / _diagonal.x;
        float  y = ToPos.y/ _diagonal.y;

        if (x <= 1 && y <= 1)
        {
            (int x, int y) centerPixel = (Mathf.RoundToInt(x * _texture.width - .5f), Mathf.RoundToInt(y * _texture.height - .5f));

            int ceiledSize = Mathf.CeilToInt(size);

            int left = Mathf.Max(centerPixel.x - ceiledSize, 0);
            int right = Mathf.Min(centerPixel.x + ceiledSize, drawSizeX - 1);
            int bot = Mathf.Max(centerPixel.y - ceiledSize, 0);
            int top = Mathf.Min(centerPixel.y + ceiledSize, drawSizeY - 1);

            for (int i = left; i <= right; i++)
            {
                for (int j = bot; j <= top; j++)
                {
                    if(Vector2.Distance(new(i, j), new(x * _texture.width, y * _texture.height)) <= size)
                    {
                        _texture.SetPixel(i, j, color);
                    }
                }
            }


            
            _texture.Apply();
            _image.texture = _texture;
        }
    }
    
    
    public static bool LinePlaneIntersection(out Vector3 intersection, Vector3 linePoint, Vector3 lineDir, Vector3 planePoint, Vector3 planeNormal)
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
