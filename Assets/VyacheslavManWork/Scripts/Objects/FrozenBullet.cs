using System.Collections;
using UnityEngine;

public class FrozenBullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletLifeTime = 10;
    [SerializeField] private float _freezeTime = 2;
    private void Awake()
    {
        Destroy(gameObject, _bulletLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(_damage);
            StartCoroutine(EnemyFreeze(other.GetComponent<EnemyAI>()));
        }
    }
    IEnumerator EnemyFreeze(EnemyAI enemy)
    {
        enemy.enabled = false;
        yield return new WaitForSeconds(_freezeTime);
        enemy.enabled = true;
    }
}