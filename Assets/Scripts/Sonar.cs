using UnityEngine;
using UnityEngine.UI;

public class Sonar : MonoBehaviour
{
    public RawImage rawImage; // RawImage UI Component
    public Texture2D texture; // La texture que vous voulez afficher

    private Rect currentUVRect; // La zone UV visible de la texture
    private float zoomSpeed = 0.1f; // Vitesse de zoom
    private float panSpeed = 0.01f; // Vitesse de déplacement

    void Start()
    {
        if (rawImage == null || texture == null)
        {
            Debug.LogError("Assurez-vous que RawImage et Texture sont assignées !");
            return;
        }

        rawImage.texture = texture;

        currentUVRect = new Rect(0, 0, 1, 1);
        rawImage.uvRect = currentUVRect;
    }

    void Update()
    {
        HandleZoom();
        HandlePan();
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float zoomFactor = 1 - scroll * zoomSpeed;

            float minZoom = 0.1f;
            float maxZoom = 1f;

            currentUVRect.width = Mathf.Clamp(currentUVRect.width * zoomFactor, minZoom, maxZoom);
            currentUVRect.height = Mathf.Clamp(currentUVRect.height * zoomFactor, minZoom, maxZoom);

            currentUVRect.x = Mathf.Clamp(currentUVRect.x, 0, 1 - currentUVRect.width);
            currentUVRect.y = Mathf.Clamp(currentUVRect.y, 0, 1 - currentUVRect.height);

            rawImage.uvRect = currentUVRect;
        }
    }

    void HandlePan()
    {

        //currentUVRect.x = Mathf.Clamp(currentUVRect.x + panX, 0, 1 - currentUVRect.width);
        //currentUVRect.y = Mathf.Clamp(currentUVRect.y + panY, 0, 1 - currentUVRect.height);

        currentUVRect.x = (MapManager.testVector.position.x / 3000f) - 0.5f * (currentUVRect.width);
        currentUVRect.y = (MapManager.testVector.position.y / 3000f) - 0.5f * (currentUVRect.height);

        rawImage.uvRect = currentUVRect;

    }
}
