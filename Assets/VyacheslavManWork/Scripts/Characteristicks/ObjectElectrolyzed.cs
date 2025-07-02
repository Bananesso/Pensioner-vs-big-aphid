using System.Collections;
using UnityEngine;

public class ObjectElectrolyzed : MonoBehaviour, IInteractable
{
    private ParticleSystem _particleSystem;
    [SerializeField] private float _radius;

    [SerializeField] private float _timeUntillElectrolyzeNear = 2;
    [SerializeField] private float _freezeTime = 3;
    [SerializeField] private float _particlePlayTime = 3;

    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    public void Interact(GameObject obj) //событие электризации для всех
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

    IEnumerator Electrolyze(GameObject obj) //электризация
    {
        yield return new WaitForSeconds(_timeUntillElectrolyzeNear);
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

    IEnumerator ParticlePlay() //партиклы
    {
        _particleSystem.Play();
        yield return new WaitForSeconds(_particlePlayTime);
        _particleSystem.Stop();
    }

    IEnumerator EnemyFreeze(EnemyAI enemy) //электризация врага
    {
        enemy.enabled = false;
        yield return new WaitForSeconds(_freezeTime);
        enemy.enabled = true;
    }
}
