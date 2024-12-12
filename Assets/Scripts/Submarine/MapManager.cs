using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    [SerializeField] Texture2D texture;
    [SerializeField] Texture2D texture2;
    [SerializeField] RawImage rawImage;
    Texture2D textureToModif;
    
    [SerializeField] Color sonarColor;

    [SerializeField] List<Vector2> SpawnPoints;
    [SerializeField] List<Vector2> GoalPoints;
    private Vector3 subPos;

    int width;
    int height;


    [SerializeField] Color accessibleColor = Color.black;

    List<Vector2Int> lastPaintedSquares = new();

    //Dictionary<string, (List<Vector2Int> list, Color color, UnityAction action)> circles = new();
    Dictionary<string, (List<Vector2Int> list, Color color, UnityAction enter, UnityAction exit)> circles = new();

    private readonly Dictionary<string, float> _eventsTriggered = new();
    [SerializeField] private float _minTimeBeforeLeave = 0.3f;


    public Vector2 GetSpawnPoint()
    {
        return SpawnPoints[Random.Range(0, SpawnPoints.Count)];
    }

    public Vector2 GetGoalPoint()
    {
        return GoalPoints[Random.Range(0, GoalPoints.Count)];
    }
    private void Start()
    {
        Init();
    }

    public void SetSubPos(Vector3 pos)
    {
        subPos = pos;
    }

    /// <summary>
    /// Initialize the Map
    /// </summary>
    private void Init()
    {
        width = texture.width;
        height = texture.height;
        
        textureToModif = CreateReadableTexture(texture2);

        rawImage.texture = textureToModif;
    }



    public void Tick()
    {
        UpdateCircles();
    }

    /// <summary>
    /// Draw and check collision with all of the created circles
    /// </summary>
    private void UpdateCircles()
    {
        foreach (var i in lastPaintedSquares)
        {
            textureToModif.SetPixel(i.x, i.y, Color.clear);
        }

        lastPaintedSquares.Clear();

        Vector2Int pos = GetPixelPos(subPos);

        foreach (var i in circles)
        {
            bool triggered = false;

            foreach(var j in i.Value.list)
            {
                lastPaintedSquares.Add(j + pos);
                textureToModif.SetPixel(j.x + pos.x, j.y + pos.y, i.Value.color);

                if(texture.GetPixel(j.x + pos.x, j.y + pos.y) != accessibleColor)
                {
                    triggered = true;
                }
            }

            if (triggered)
            {
                i.Value.enter?.Invoke();
                _eventsTriggered.TryAdd(i.Key, Time.time);
            }
            else
            {
                if (!_eventsTriggered.TryGetValue(i.Key, out var timeTriggered)) 
                    continue;

                if (Time.time - timeTriggered < _minTimeBeforeLeave)
                    continue;
                
                _eventsTriggered.Remove(i.Key);
                i.Value.exit?.Invoke();
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

    public void InitArea(string areaName, float size, Shape shape, UnityAction OnAreaEnter, UnityAction OnAreaExit)
    {
        Func<float, List<Vector2Int>> shapeCreation = GetCircle;

        switch (shape)
        {
            case Shape.Circle:
                shapeCreation = GetCircle;
                break;
            case Shape.FilledCircle:
                shapeCreation = GetFilledCircle;
                break;
        }

        circles.Add(areaName, (shapeCreation(size), sonarColor, OnAreaEnter, OnAreaExit));
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

public enum Shape
{
    Circle,
    FilledCircle
}
