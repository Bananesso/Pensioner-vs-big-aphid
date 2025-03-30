using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health Health;

    public void OnRaycastHit(int damage)
    {
        Health.TakeDamage(damage);
    }
}