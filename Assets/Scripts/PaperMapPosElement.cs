using UnityEngine;

public class PaperMapPosElement : MonoBehaviour
{
    public Vector2 pos { get => new Vector2(transform.localPosition.x, transform.localPosition.y); set {} }

    public Vector2 paperMapPos;
}
