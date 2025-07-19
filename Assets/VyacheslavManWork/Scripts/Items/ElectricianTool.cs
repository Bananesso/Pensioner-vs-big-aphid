using UnityEngine;

public class ElectricianTool : Item
{
    [Header("Режет провода (true) или делает (false)")]
    public bool Scissors;
    
    private AudioSource audioSource;
    private ParticleSystem connectionEffect;
    private InteractionController controller;

    private void Start()
    {
        connectionEffect = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
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

    private void PlayConnectionEffects()
    {
        connectionEffect?.Play();
        audioSource?.Play();
    }
}