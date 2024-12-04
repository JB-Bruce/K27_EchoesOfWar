using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HotBar : MonoBehaviour
{
    //remove once done
    [SerializeField] private Item DebugItem;
    [SerializeField] private Item DebugItem1;
    [SerializeField] private Item DebugItem2;
    [SerializeField] private Item DebugItem3;

    [SerializeField] private Transform _player;
    [SerializeField] private InputAction _scrollAction;
    [SerializeField] private InputAction _dropItem;
    [SerializeField] private InputAction _debug;
    [SerializeField] private Image _selector;
    [SerializeField] private List<Image> _itemDisplays = new List<Image>();
    private List<Item> _items = new List<Item>();
    private int _selectedItem;

    private void Start()
    {
        _scrollAction.Enable();
        _scrollAction.performed += ScrollSelect;
        _dropItem.Enable();
        _dropItem.performed += DropItem;
        //to remove once done
        _debug.Enable();
        _debug.performed += DebugAddItem;
        AddItemToHotBar(DebugItem);
        AddItemToHotBar(DebugItem1);
        AddItemToHotBar(DebugItem2);
        AddItemToHotBar(DebugItem3);
    }

    //remove once donce
    private void DebugAddItem(InputAction.CallbackContext context)
    {
        if (context.performed) 
            AddItemToHotBar(DebugItem);
    }

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

    private void  SelectorPosition()
    {
        if (_items.Count == 0) _selector.color = Color.clear;
        else
        {
            _selector.transform.position = _itemDisplays[_selectedItem].rectTransform.position;
            _selector.color = Color.green;
        }
    }
    
    public void AddItemToHotBar(Item item)
    {
        if (_items.Count < _itemDisplays.Count)
        {
            if (_items.Count == 0)
            {
                _selectedItem = 0;
            }
            _items.Add(item);
            SelectorPosition();
           RefreshHotBar();
        }
    }
    
    public void RemoveItemFromHotBar(Item item)
    {
        _items.Remove(item);
        if (_selectedItem == _items.Count && _items.Count != 0) _selectedItem--;
        SelectorPosition();
        RefreshHotBar();
    }
    
    private void RefreshHotBar()
    {
        for(int i = 0; i < _items.Count; i++)
        {
            _itemDisplays[i].sprite = _items[i]._sprite;
            _itemDisplays[i].color = Color.white;
        }
        
        
        
        if (_itemDisplays.Count <= _items.Count) return; 
        for (int i = _items.Count; i < _itemDisplays.Count; i++) 
        { 
            _itemDisplays[i].sprite = null; 
            _itemDisplays[i].color = Color.clear;
        }
    }
}
