using UnityEngine;

public class Electroshocker : Item
{
    private InteractionController controller;

    private void Start()
    {
        controller = FindAnyObjectByType<InteractionController>();
    }
    public override void Use(GameObject user, IInventory inventory)
    {
       IInteractable inter =  controller.GetInteractable();
        if (inter is ObjectElectrolyzed obj)
        {
            obj.Interact(user);
        }
    }
}
