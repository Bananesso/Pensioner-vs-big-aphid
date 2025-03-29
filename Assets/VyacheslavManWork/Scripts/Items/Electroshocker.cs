using System.Collections;
using UnityEngine;

public class Electroshocker : Item
{
    private AudioSource _shootSound;
    private ParticleSystem _electro;
    private bool _isReloading;
    private InteractionController controller;

    private void Start()
    {
        _electro = GetComponentInChildren<ParticleSystem>();
        _shootSound = GetComponent<AudioSource>();
        controller = FindAnyObjectByType<InteractionController>();
    }
    public override void Use(GameObject user, IInventory inventory)
    {
        if (_isReloading) return;
        IInteractable inter =  controller.GetInteractable();
        if (inter is ObjectElectrolyzed obj)
        {
            obj.Interact(user);
            _electro.Play();
            _shootSound.Play();
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(10);
        _isReloading = false;
    }
}
