using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HotBar : Singleton<HotBar>
{
    [SerializeField] private UseItem _UseItem;
    [SerializeField] private Transform _player;
    [SerializeField] private MeshFilter _SelectedItemRenderer;
    [SerializeField] private Image _selector;
    [SerializeField] private GameObject _ItemSlot;
    [SerializeField] private int _inventorySize;
    [SerializeField] private Color _selectorColor;
    [SerializeField] private TextMeshProUGUI _SelectedItemNameDisplay;
    [SerializeField] private float _ThrowStrength;
    private List<GameObject> _itemDisplays = new List<GameObject>();
    private List<Item> _items = new List<Item>();
    private int _selectedItem;
    public Item GetSelectedItem()
    {
        return _items.Count == 0 ? null : _items[_selectedItem];
    }
    
    /// <summary>
    ///  Function that allow the player to drop the selected item from the inventory
    /// </summary>
    public GameObject DropItem()
    {
        if (_items.Count > 0 && _items[_selectedItem]._prefab != null && !_UseItem.activated)
        { 
            GameObject item  = Instantiate(_items[_selectedItem]._prefab,_player.position+Camera.main.transform.forward, _items[_selectedItem]._prefab.transform.rotation);
            item.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward*_ThrowStrength);
            RemoveItemFromHotBar(_items[_selectedItem]);

            return item;
        }

        return null;
    }
    public string GetSelectedItemName()
    {
        if (_items.Count == 0) return "";
        return _items[_selectedItem]._name;
    }
    
    /// <summary>
    /// allow the player to select an item in it's inventory by scrolling through it
    /// </summary>
    /// <param name="context"> the  positive/negative action to be performed to scroll through the inventory</param>
    public void ScrollSelect(InputAction.CallbackContext context)
    {
        if (_items.Count <= 0 || _UseItem.activated) return;
        if (context.ReadValue<float>() >0)
        {
            if (_selectedItem <= _items.Count)
            {
                _selectedItem++;
            }
            if (_selectedItem == _items.Count)
            {
                _selectedItem = 0;
            }
        }
        if (context.ReadValue<float>() < 0)
        {
            if (_selectedItem >= 0)
            { 
                _selectedItem--;
            }

            if (_selectedItem < 0)
            {
                _selectedItem = _items.Count - 1;
            }
        }
        SelectorPosition();
    }

    /// <summary>
    /// change the SelectorPosition to the currently seletec item
    /// </summary>
    private void  SelectorPosition()
    {
        if (_items.Count == 0)
        {
            _selector.color = Color.clear;
            _SelectedItemNameDisplay.text = "";
        }
        else
        {
            _selector.transform.SetParent(_itemDisplays[_selectedItem].transform);
            _selector.transform.localPosition = Vector2.zero;
            _selector.color = _selectorColor ;
            _SelectedItemRenderer.mesh = _items[_selectedItem]._sprite3D;
            _SelectedItemNameDisplay.text = _items[_selectedItem]._name;
        }
    }
    
    /// <summary>
    /// add an item to the hotbar to be displayed
    /// </summary>
    /// <param name="item">the item you want to add to the hotbar</param>
    /// <returns></returns>
    public bool AddItemToHotBar(Item item)
    {
        if (_items.Count < _inventorySize)
        {
            if (_items.Count == 0)
            {
                _selectedItem = 0;
            }
            _itemDisplays.Add(Instantiate(_ItemSlot, transform));
            _items.Add(item);
            RefreshHotBar(); 
            SelectorPosition(); 
            
            return true;
        }
        
        return false;
    }
    
    /// <summary>
    /// remove an item from the hotbar
    /// </summary>
    /// <param name="item">the item you want to remove from the  inventory</param>
    /// <returns></returns>
    public bool RemoveItemFromHotBar(Item item)
    {
        if (!_items.Contains(item)) return false;
        _items.Remove(item);
        if (_selectedItem == _items.Count && _items.Count != 0) _selectedItem--;
        RefreshHotBar();
        SelectorPosition(); 
        return true;
    }
    
    /// <summary>
    /// refresh the display of the hotbar
    /// </summary>
    private void RefreshHotBar()
    {
        for(int i = 0; i < _items.Count; i++)
        {
            _itemDisplays[i].GetComponent<Image>().sprite = _items[i]._sprite2D;
            _itemDisplays[i].GetComponent<Image>().color = Color.white;
        }
        
        if (_itemDisplays.Count <= _items.Count) return; 
        for (int i = _items.Count; i <= _itemDisplays.Count; i++) 
        { 
            _selector.transform.SetParent(transform);
            _selector.transform.localPosition = Vector2.zero;
            Destroy(_itemDisplays[i]);
            _itemDisplays.RemoveAt(i);
        }
    }
}
