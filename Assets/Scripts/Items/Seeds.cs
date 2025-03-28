using UnityEngine;

public class Seeds : Item
{
    private InteractionController controller ;
    public GameObject FlowerPrefab;
    private void Start()
    {
        controller = FindAnyObjectByType<InteractionController>();
    }
    public override void Use(GameObject user, IInventory inventory)
    {
        IInteractable nazvanye = controller.GetInteractable();

        if (nazvanye is Gryadka gryadka)
        {
            gryadka.Interact(FlowerPrefab);
        }
    }
}
