using System;
using System.Collections;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [Header("Скорость атаки (для настройки анимации (сначала вызывается анимацияm, потом выстрел)")]
    [SerializeField] private float _timeUntillShot;
    [SerializeField] private float _timeAfterShot;

    [Header("Настройки стрельбы")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private int _timeElectrolyzed;
    private int _timeElectrLast;

    [Header("Техническое")]
    [SerializeField] private bool _electrolyzed;

    private Coroutine _shootingCoroutine;
    private Animator _animator;
    public event Action OnTimeElectrChange;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(Shoot());
    }

    public void Reload() //вызов электризации из кода электрошокера
    {
        _electrolyzed = true;
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
        }
        _shootingCoroutine = StartCoroutine(LooseEnergy());
    }

    private IEnumerator LooseEnergy() //спад электризации
    {
        _timeElectrLast = _timeElectrolyzed;
        while (_timeElectrLast > 0)
        {
            _timeElectrLast--;
			OnTimeElectrChange?.Invoke();
            yield return new WaitForSeconds(1);
        }
        _electrolyzed = false;
    }

    private IEnumerator Shoot() //стрельба
    {
        while (true)
        {
            if (_electrolyzed)
            {
                if (_animator != null)
                    _animator.SetTrigger("Attack");
                yield return new WaitForSeconds(_timeUntillShot);
                GameObject BulletInstance = Instantiate(_bullet, _firePoint.position, Quaternion.identity);
                BulletInstance.GetComponent<Rigidbody>().AddForce(_firePoint.transform.forward * _bulletSpeed);
            }
            yield return new WaitForSeconds(_timeAfterShot);
        }
    }

    public float GetTimeElInParts()
    {
		return (float) _timeElectrLast / _timeElectrolyzed;
    }
}