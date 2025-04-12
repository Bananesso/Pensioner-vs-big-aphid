using System.Collections;
using UnityEngine;

public class FrozenBullet : MonoBehaviour
{
    public float Damage;
    private void Awake()
    {
        Destroy(gameObject, 7);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(Damage);
            StartCoroutine(EnemyFreeze(other.GetComponent<EnemyAI>()));
        }
    }
    IEnumerator EnemyFreeze(EnemyAI enemy)
    {
        enemy.enabled = false;
        yield return new WaitForSeconds(2);
        enemy.enabled = true;
    }
}