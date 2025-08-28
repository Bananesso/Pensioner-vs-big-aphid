using UnityEngine;

public class ElectricianTool : Item
{
    [Header("Режет провода (true) или создаёт (false)")]
    public bool Scissors;

    private InteractionController controller;

    private void Start()
    {
        controller = FindObjectOfType<InteractionController>();
    }

    public override void Use(GameObject user, IInventory inventory)
    {
        IInteractable inter = controller.GetInteractable();
        if (inter is ObjectElectrolyzed obj)
        {
            ElectricTarget target = obj.GetComponent<ElectricTarget>();
            if (target != null)
                target.Interact(this.gameObject);
        }
    }
}