using UnityEngine;

public class ItemScript : MonoBehaviour,IInteractable
{
    
    public Item _SOItem;
    public HotBar _hotBar;
    [SerializeField] Outline _outline;
    public Outline outline => _outline;
    public string interactableName => _SOItem.name;
    
    public void Interact()
    {
        if (_hotBar.AddItemToHotBar(_SOItem))
        {
            Destroy(this.gameObject);
        }
    }
    
    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }
}
