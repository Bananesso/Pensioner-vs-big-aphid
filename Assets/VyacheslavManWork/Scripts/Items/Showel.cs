using UnityEngine;

public class Showel : Item
{
    private AudioSource _useSound;
    private ParticleSystem _dirtParticle;
    private InteractionController controller;

    void Start()
    {
        _dirtParticle = GetComponentInChildren<ParticleSystem>();
        _useSound = GetComponent<AudioSource>();
        controller = FindAnyObjectByType<InteractionController>();
    }

    public override void Use(GameObject user, IInventory inventory)
    {
        IInteractable inter = controller.GetInteractable();
        if (inter is Health obj)
        {
            if (!obj.Enemy)
                Destroy(obj);
            _dirtParticle.Play();
            _useSound.Play();
        }
    }
}