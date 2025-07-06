using System.Collections;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private bool _electrolyzed;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _timeElectrolyzed;
    private Coroutine shootingCoroutine;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    public void Reload() //вызов электризации из кода электрошокера
    {
        _electrolyzed = true;
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
        }
        shootingCoroutine = StartCoroutine(LooseEnergy());
    }

    private IEnumerator LooseEnergy() //спад электризации
    {
        yield return new WaitForSeconds(_timeElectrolyzed);
        _electrolyzed = false;
    }

    private IEnumerator Shoot() //стрельба
    {
        while (true)
        {
            if (_electrolyzed)
            {
                GameObject BulletInstance = Instantiate(_bullet, _firePoint.position, Quaternion.identity);
                BulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * _bulletSpeed);
            }
            yield return new WaitForSeconds(_attackSpeed);
        }
    }
}