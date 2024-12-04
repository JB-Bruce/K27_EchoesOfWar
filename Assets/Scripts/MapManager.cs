using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Texture2D texture;

    int width;
    int height;

    private void Start()
    {
        width = texture.width;
        height = texture.height;
    }


}
