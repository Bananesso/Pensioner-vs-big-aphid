using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private Transform _itemHolder;
    [SerializeField] private Transform _mainInventoryUI;

    private QuickSlotInventory _quickSlotInventory;
    private MainInventory _mainInventory;

    public QuickSlotInventory QuickSlotInventory => _quickSlotInventory;
    public MainInventory MainInventory => _mainInventory;

    private void Awake()
    {
        _quickSlotInventory = GetComponentInChildren<QuickSlotInventory>();
        _mainInventory = GetComponentInChildren<MainInventory>();
        _quickSlotInventory.Initialize();
        _mainInventory.Initialize();
    }
    public void PickupItem()
    {
        Vector3 cameraPoint = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(cameraPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            Item item = hit.collider.GetComponent<Item>();
            if (item != null)
            {
                if (_quickSlotInventory.AddItem(item))
                    item.PickupItem(_itemHolder);
            }
        }

    }

    public void DropItem()
    {
        Item item = _quickSlotInventory.Items[_quickSlotInventory.CurrentSlotIndex];
        if (_quickSlotInventory.RemoveItem())
            item.DropItem();
    }

    public void ToggleInventory()
    {
        _mainInventoryUI.gameObject.SetActive(!_mainInventoryUI.gameObject.activeSelf);
    }
}