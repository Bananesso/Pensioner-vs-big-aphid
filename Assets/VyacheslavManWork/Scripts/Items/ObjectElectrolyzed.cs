using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectElectrolyzed : MonoBehaviour, IInteractable
{
    private ParticleSystem _particleSystem;
    [SerializeField] private float _radius;
    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    public void Interact(GameObject obj)
    {
        StartCoroutine(ParticlePlay());

        EnemyAI enemy = GetComponent<EnemyAI>();
        if (enemy != null)
        {
            StartCoroutine(EnemyFreeze(enemy));
        }

        Flower flower = GetComponent<Flower>();
        if (flower != null)
        {
            flower.Reload();
        }

        if (obj.GetComponent<PlayerController>() != null)
        {
            StartCoroutine(Electrolyze(obj));
        }
    }
    private void OnDrawGizmosSelected()
    {
        Color color = Color.blue;
        color.a = 0.4f;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, _radius);
    }

    IEnumerator Electrolyze(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in colliders)
        {
            IInteractable inter = collider.GetComponent<IInteractable>();
            if(inter is ObjectElectrolyzed objectt)
            {
                objectt.Interact(gameObject);
            }
        }
    }

    IEnumerator ParticlePlay()
    {
        _particleSystem.Play();
        yield return new WaitForSeconds(3);
        _particleSystem.Stop();
    }

    IEnumerator EnemyFreeze(EnemyAI enemy)
    {
        enemy.enabled = false;
        yield return new WaitForSeconds(3);
        enemy.enabled = true;
    }
}
