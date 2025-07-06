using UnityEngine;

public class FireTree : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet != null && bullet.CanBeFired)
        {
            bullet.Fired();
        }
    }
}