using UnityEngine;

public class AddBulletPenetrationStation : MonoBehaviour
{
    [SerializeField] private int _bulletPenetrationAddition = 1;

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet != null && bullet.CanBeFired)
        {
            bullet.BulletPenetration += _bulletPenetrationAddition;
        }
    }
}