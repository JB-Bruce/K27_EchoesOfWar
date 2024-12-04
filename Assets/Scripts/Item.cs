using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
     public GameObject _prefab;
    public string _name; 
    public Sprite _sprite;
}
