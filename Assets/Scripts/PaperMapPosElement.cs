using UnityEngine;

public class PaperMapPosElement : MonoBehaviour
{
    public Vector2 pos { get => new Vector2(
       GetComponent<RectTransform>().anchoredPosition.x, 
       GetComponent<RectTransform>().anchoredPosition.y); set {} }

    public Vector2 paperMapPos;
}
