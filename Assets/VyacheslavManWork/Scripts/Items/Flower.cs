using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private bool _electrolyzed;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    public void Reload()
    {
        _electrolyzed = true;
        StartCoroutine(Shoot());

    }

    private IEnumerator LooseEnergy()
    {
        yield return new WaitForSeconds(5);
        _electrolyzed = false;
    }
    private IEnumerator Shoot()
    {
        StartCoroutine(LooseEnergy());
        while (_electrolyzed)
        {
            GameObject BulletInstance = Instantiate(_bullet, _firePoint.position, Quaternion.identity);
            Bullet bullet = BulletInstance.GetComponent<Bullet>();
            if(bullet!=null) bullet.Damage = _damage;

            FrozenBullet frozentbullet = BulletInstance.GetComponent<FrozenBullet>();
            if (frozentbullet != null)
                frozentbullet.Damage = _damage;

            BulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * _bulletSpeed);
            yield return new WaitForSeconds(_attackSpeed);
        }
    }
}