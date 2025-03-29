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
    private IEnumerator Shoot()
    {
        while (_electrolyzed)
        {
            GameObject BulletInstance = Instantiate(_bullet, _firePoint.position, Quaternion.identity);
            BulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * _bulletSpeed);
            yield return new WaitForSeconds(_attackSpeed);
        }
    }
}