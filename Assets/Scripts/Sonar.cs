using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sonar : MonoBehaviour
{
    public RawImage rawImage; // RawImage UI Component
    public RawImage rawImage2; // RawImage UI Component
    public Texture2D texture; // La texture que vous voulez afficher

    private Rect currentUVRect; // La zone UV visible de la texture

    [SerializeField] bool doesLoop;
    [SerializeField] List<ZoomInfo> sizes = new();
    int curIndex;

    private Vector2 _submarinePosition;

    private void Start()
    {
        rawImage.texture = texture;

        currentUVRect = new Rect(0, 0, 1, 1);
        rawImage.uvRect = currentUVRect;

        HandleZoom();
    }

    private void Update()
    {
        HandlePan();
    }

    private void HandleZoom()
    {
        float nextZoom = sizes[curIndex].zoom;

        currentUVRect.width = Mathf.Clamp(nextZoom, 0, 1);
        currentUVRect.height = Mathf.Clamp(nextZoom, 0, 1);

        currentUVRect.x = Mathf.Clamp(currentUVRect.x, 0, 1 - currentUVRect.width);
        currentUVRect.y = Mathf.Clamp(currentUVRect.y, 0, 1 - currentUVRect.height);

        rawImage.uvRect = currentUVRect;
        rawImage2.uvRect = currentUVRect;
    }

    public void ZoomIn()
    {
        SetNextZoom(true);
        HandleZoom();
    }

    public void ZoomOut()
    {
        SetNextZoom(false);
        HandleZoom();
    }

    private void SetNextZoom(bool next)
    {
        int increase = next ? 1 : -1;

        curIndex += increase;

        if (curIndex >= sizes.Count)
        {
            if (doesLoop) curIndex %= sizes.Count;
            else curIndex = sizes.Count - 1;
        }
        else if (curIndex < 0)
        {
            if (doesLoop) curIndex = sizes.Count + curIndex;
            else curIndex = 0;
        }
    }

    private void HandlePan()
    {
        currentUVRect.x = _submarinePosition.x / texture.width - 0.5f * currentUVRect.width;
        currentUVRect.y = _submarinePosition.y / texture.height - 0.5f * currentUVRect.height;

        rawImage.uvRect = currentUVRect;
        rawImage2.uvRect = currentUVRect;
    }

    public void SetSubmarinePosition(Vector2 pos)
    {
        _submarinePosition = pos;
    }

    [System.Serializable]
    public struct ZoomInfo
    {
        public string length;
        public float zoom;
    }
}
