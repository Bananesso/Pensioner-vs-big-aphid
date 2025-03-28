using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class MainInventory : MonoBehaviour, IInventory
{
    [SerializeField] private int _maxSlots = 20;
    [SerializeField] private Transform _inventoryPanel;
    [SerializeField] private List<Item> _items = new List<Item>();

    public List<Item> Items => _items;
    public int MaxSlots => _maxSlots;
    public event Action OnInventoryUpdated;

    private bool _isOpen = false;

    public void Initialize()
    {

        while (_items.Count < _maxSlots)
        {
            _items.Add(null);
        }
    }

    public void ToggleInventory()
    {
        _isOpen = !_isOpen;
        _inventoryPanel.gameObject.SetActive(_isOpen);
    }

    public Item SetItem(Item item, int id)
    {
        if (id < 0 || id >= _items.Count)
            return null;

        Item oldItem = _items[id];
        _items[id] = item;
        OnInventoryUpdated?.Invoke();
        return oldItem;
    }

    public bool AddItem(Item item)
    {

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = item;
                OnInventoryUpdated?.Invoke();
                return true;
            }
        }

        return false;
    }
    public void SwapItems(int indexA, int indexB)
    {
        (Items[indexA], Items[indexB]) = (Items[indexB], Items[indexA]);
        OnInventoryUpdated?.Invoke();
    }
    public bool RemoveItem(int id)
    {
        if (id >= 0 && id < _items.Count && _items[id] != null)
        {
            _items[id] = null;
            OnInventoryUpdated?.Invoke();
            return true;
        }
        return false;
    }
}