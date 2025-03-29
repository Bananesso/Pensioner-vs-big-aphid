using UnityEngine;

public class Seeds : Item
{
    private InteractionController controller;
    private QuickSlotInventory inventorySlot;
    public GameObject FlowerPrefab;
    private void Start()
    {
        controller = FindAnyObjectByType<InteractionController>();
        inventorySlot = FindAnyObjectByType<QuickSlotInventory>();
    }
    public override void Use(GameObject user, IInventory inventory)
    {
        IInteractable nazvanye = controller.GetInteractable();

        if (nazvanye is Gryadka gryadka)
        {
            gryadka.Interact(FlowerPrefab);
            inventorySlot.RemoveItem();
        }
    }
}