using UnityEngine;

public class Electroshocker : Item
{
    private InteractionController controller;
    private QuickSlotInventory inventorySlot;
    void Start()
    {
        
    }

    public override void Use(GameObject user, IInventory inventory)
    {
        if (gameObject.GetComponent<EnemyAI>())
        {
            
        }
        else if (gameObject.GetComponent<Flower>())
        {

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

        }
    }
}
