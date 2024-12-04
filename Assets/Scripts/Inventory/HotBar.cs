using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HotBar : MonoBehaviour
{
    //remove once done
    [SerializeField] private Item DebugItem;

    [SerializeField] private Transform _player;
    [SerializeField] private InputAction _scrollAction;
    [SerializeField] private InputAction _dropItem;
    [SerializeField] private InputAction _debug;
    [SerializeField] private Image _selector;
    [SerializeField] private GameObject _ItemSlot;
    [SerializeField] private int _inventorySize;
    [SerializeField] private Color _selectorColor;
    private List<GameObject> _itemDisplays = new List<GameObject>();
    private List<Item> _items = new List<Item>();
    private int _selectedItem;

    private void Start()
    {
        //will need to be moved to an input action probably in the player controller
        _scrollAction.Enable();
        _scrollAction.performed += ScrollSelect;
        _dropItem.Enable();
        _dropItem.performed += DropItem;
        //to remove once done
        _debug.Enable();
        _debug.performed += DebugAddItem;
    }

    //remove once donce
    private void DebugAddItem(InputAction.CallbackContext context)
    {
        if (context.performed) 
            AddItemToHotBar(DebugItem);
    }

    /// <summary>
    ///  Function that allow the player to drop the selected item from the inventory
    /// </summary>
    /// <param name="context">the action that need to be performed in order to drop the item</param>
    private void DropItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_items.Count > 0)
            {
                Instantiate(_items[_selectedItem]._prefab,_player.position+_player.forward,Quaternion.identity);
                RemoveItemFromHotBar(_items[_selectedItem]);
            }
        }
    }
    
    /// <summary>
    /// allow the player to select an item in it's inventory by scrolling through it
    /// </summary>
    /// <param name="context"> the  positive/negative action to be performed to scroll through the inventory</param>
    private void ScrollSelect(InputAction.CallbackContext context)
    {
        if (_items.Count <= 0) return;
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
        }
        else
        {
            _selector.transform.SetParent(_itemDisplays[_selectedItem].transform);
            _selector.transform.localPosition = Vector2.zero;
            _selector.color = _selectorColor ;
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
            _itemDisplays[i].GetComponent<Image>().sprite = _items[i]._sprite;
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
