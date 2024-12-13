using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public GameObject _prefab;
    public string _name; 
    public Sprite _sprite2D;
    public Mesh  _sprite3D;
    public bool _hideInventory;
}
