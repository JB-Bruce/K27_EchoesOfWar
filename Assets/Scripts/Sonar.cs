using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sonar : MonoBehaviour
{
    public RawImage rawImage; // RawImage UI Component
    public Texture2D texture; // La texture que vous voulez afficher

    private Rect currentUVRect; // La zone UV visible de la texture

    [SerializeField] bool doesLoop;
    [SerializeField] List<ZoomInfo> sizes = new();
    int curIndex;

    void Start()
    {
        rawImage.texture = texture;

        currentUVRect = new Rect(0, 0, 1, 1);
        rawImage.uvRect = currentUVRect;

        HandleZoom();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            if(scroll < 0)
                ZoomIn();
            else 
                ZoomOut();
        }
        HandlePan();
    }

    void HandleZoom()
    {

        float nextZoom = sizes[curIndex].zoom;

        currentUVRect.width = Mathf.Clamp(nextZoom, 0, 1);
        currentUVRect.height = Mathf.Clamp(nextZoom, 0, 1);

        currentUVRect.x = Mathf.Clamp(currentUVRect.x, 0, 1 - currentUVRect.width);
        currentUVRect.y = Mathf.Clamp(currentUVRect.y, 0, 1 - currentUVRect.height);

        rawImage.uvRect = currentUVRect;

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

        curIndex = curIndex + increase;

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

    void HandlePan()
    {
        currentUVRect.x = (MapManager.testVector.position.x / 3000f) - 0.5f * (currentUVRect.width);
        currentUVRect.y = (MapManager.testVector.position.y / 3000f) - 0.5f * (currentUVRect.height);

        rawImage.uvRect = currentUVRect;

    }

    [System.Serializable]
    public struct ZoomInfo
    {
        public string length;
        public float zoom;
    }
}
