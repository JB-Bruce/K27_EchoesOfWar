using UnityEngine;

public class ItemScript : MonoBehaviour,IInteractable
{
    
    public Item _SOItem;
    private HotBar _hotBar;
    [SerializeField] protected Outline _outline;
    public Outline outline => _outline;
    public string interactableName => _SOItem.name;

    private void Start()
    {
        _hotBar = HotBar.Instance;
        _outline.enabled = false;
    }

    public void Interact()
    {
        if (_hotBar.AddItemToHotBar(_SOItem))
        {
            Destroy(this.gameObject);
        }
    }
    
    private void Awake()
    {
    }
}
