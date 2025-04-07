using UnityEngine;

public class Bullet : MonoBehaviour
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
        }
        Destroy(gameObject);
    }
}
