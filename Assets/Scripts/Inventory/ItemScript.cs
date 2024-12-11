using UnityEngine;

public class ItemScript : MonoBehaviour,IInteractable
{
    
    public Item _SOItem;
    public HotBar _hotBar;

    void interact()
    {
        if (_hotBar.AddItemToHotBar(_SOItem))
        {
            Destroy(this.gameObject);
        }
    }
    
    public string interactableName { get; }
    public Outline outline { get; }
}
