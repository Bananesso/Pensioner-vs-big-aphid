using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("Атака")]
    [SerializeField] private int _damage = 10;
    [SerializeField] private int _fireRate;

    [Header("Зрение/область атаки")]
    [SerializeField] private Transform _vision;
    [SerializeField] private float _sphereRadius;

    [Header("Передвижение")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _tempSpeed;

    [Header("Заморозка")]
    [SerializeField] private float _freezeSpeedDebuff = 1.5f;
    [SerializeField] private float _freezeTime = 5;

    [Header("Стан от электризации")]
    [SerializeField] private float _stunTime = 3;

    private float _stunSpeedDebuff;
    private float _stunTimeLast;
    private float _freezeTimeLast;
    private Rigidbody _rigidbody;
    private Health flower;
    private Coroutine coroutine;
    private Coroutine freezeCoroutine;
    private Coroutine stunCoroutine;
    private Animator _animator;

    private void Start()
    {
        _stunSpeedDebuff = _moveSpeed;
        _tempSpeed = _moveSpeed;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        StartCoroutine(Check());
    }

    private void Update()
    {
        _rigidbody.velocity = transform.forward * _tempSpeed * PlayerPrefs.GetFloat("MultiplierMooveSpeed", 1);
    }

    private IEnumerator Check()
    {
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(_vision.position, _sphereRadius);
            if (colliders.Length > 0)
            {
                flower = colliders[0].GetComponent<Health>();
                if (flower != null && !flower.Enemy)
                {
                    if (coroutine == null)
                    {
                        _tempSpeed = 0;
                        _animator.SetBool("IsAttacking", true);
                        coroutine = StartCoroutine(Atack());
                    }
                }
            }
            else
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                    _animator.SetBool("IsAttacking", false);
                }
                coroutine = null;
                flower = null;
                _tempSpeed = _moveSpeed;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator Atack()
    {
        while (flower != null)
        {
            flower.GetComponent<Health>().TakeDamage(_damage + PlayerPrefs.GetFloat("MultiplierAtkDamage", 1));
            yield return new WaitForSeconds(_fireRate + PlayerPrefs.GetFloat("MultiplierAtkSpeed", 1));
        }
    }

    public void Stun()
    {
        _stunTimeLast = _stunTime;
        if (stunCoroutine == null)
            stunCoroutine = StartCoroutine(StunCoroutine());
    }

    private IEnumerator StunCoroutine()
    {
        _moveSpeed -= _freezeSpeedDebuff;
        while (_stunTimeLast > 0)
        {
            yield return new WaitForSeconds(1);
            _stunTimeLast--;
        }
        _moveSpeed += _freezeSpeedDebuff;
        stunCoroutine = null;
    }

    public void Freeze()
    {
        _freezeTimeLast = _freezeTime;
        if (freezeCoroutine == null)
            freezeCoroutine = StartCoroutine(FreezeCoroutine());
    }

    private IEnumerator FreezeCoroutine()
    {
        _moveSpeed -= _stunSpeedDebuff;
        while (_freezeTimeLast > 0)
        {
            yield return new WaitForSeconds(1);
            _freezeTimeLast--;
        }
        _moveSpeed += _stunSpeedDebuff;
        freezeCoroutine = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_vision.position, _sphereRadius);
    }
}