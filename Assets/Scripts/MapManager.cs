using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] Texture2D texture;
    [SerializeField] Texture2D texture2;
    [SerializeField] Image image;
    Texture2D textureToModif;

    [SerializeField] Color sonarColor;


    public Transform testVector;
    public bool test;
    public Color testColor;
    public float testRange;

    int width;
    int height;

    [SerializeField] Color accessibleColor = Color.black;

    List<Vector2Int> lastPaintedSquares = new();

    Dictionary<string, (List<Vector2Int> list, Color color, UnityAction action)> circles = new();

    private void Start()
    {
        Init(1f, 6f);
    }

    private void Init(float shipSize, float detectionRange)
    {
        width = texture.width;
        height = texture.height;

        textureToModif = CreateReadableTexture(texture2);

        image.sprite = CreateSpriteFromTexture(textureToModif);

        circles.Add("Collision", (GetFilledCircle(shipSize), sonarColor, null));
        circles.Add("Detection", (GetCircle(detectionRange), sonarColor, null));

        //InitCollision(testRange, () => { });
    }



    private void Update()
    {
        UpdateCircles();
    }

    private void UpdateCircles()
    {
        foreach (var i in lastPaintedSquares)
        {
            textureToModif.SetPixel(i.x, i.y, Color.clear);
        }

        lastPaintedSquares.Clear();

        Vector2Int pos = GetPixelPos(testVector.position);

        foreach (var i in circles)
        {
            foreach(var j in i.Value.list)
            {
                lastPaintedSquares.Add(j + pos);
                textureToModif.SetPixel(j.x + pos.x, j.y + pos.y, i.Value.color);
            }
        }

        textureToModif.Apply();
    }

    public Vector2Int GetPixelPos(Vector2 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(pos.x - 0.5f), Mathf.RoundToInt(pos.y - 0.5f));
    }

    public Color GetPixel(Vector2 pos)
    {
        Vector2Int newPos = GetPixelPos(pos);
        return texture.GetPixel(newPos.x, newPos.y);
    }

    Texture2D CreateReadableTexture(Texture2D originalTexture)
    {
        Texture2D newTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false);
        newTexture.filterMode = FilterMode.Point;

        newTexture.SetPixels(originalTexture.GetPixels());
        newTexture.Apply();

        return newTexture;
    }

    Sprite CreateSpriteFromTexture(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public void InitCollision(float size, UnityAction collisionDetection)
    {
        

        List<Vector2Int> l = GetCircle(size);
        Vector2Int pos = GetPixelPos(testVector.position);

        lastPaintedSquares.Add(pos);

        textureToModif.SetPixel(pos.x, pos.y, Color.red);

        foreach (Vector2Int v in l)
        {
            lastPaintedSquares.Add(v + pos);
            textureToModif.SetPixel(v.x + pos.x, v.y + pos.y, Color.red);
        }

        textureToModif.Apply();
    }

    private List<Vector2Int> GetCircle(float size)
    {
        HashSet<Vector2Int> results = new HashSet<Vector2Int>();

        int numberToGet = Mathf.CeilToInt(Mathf.Pow(2f, size + 1f));

        for (int i = 0; i < numberToGet; i++)
        {
            float pointNum = (i * 1.0f) / numberToGet;
            float angle = pointNum * Mathf.PI * 2;
            float r = (size - 0.1f) / 2 * (Mathf.PI);
            float x = Mathf.Sin(angle) * r;
            float y = Mathf.Cos(angle) * r;
            Vector2Int pointPos = GetPixelPos(new Vector2(x + 0.5f, y + 0.5f));

            results.Add(pointPos);
        }

        return results.ToList();
    }

    private List<Vector2Int> GetFilledCircle(float size)
    {
        List<Vector2Int> results = new List<Vector2Int>();

        int ceiled = Mathf.CeilToInt(size);

        for (int i = -ceiled; i <= ceiled; i++)
        {
            for (int j = -ceiled; j <= ceiled; j++)
            {
                Vector2Int newPos = new Vector2Int(i, j);
                if (newPos.magnitude <= size)
                {
                    results.Add(newPos);
                }
            }
        }

        return results;
    }
}
