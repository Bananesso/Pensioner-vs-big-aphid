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
    [SerializeField] private int _freezeTime = 5;

    private float _freezeTimeLast;
    public bool IsMoving;
    private Rigidbody _rigidbody;
    Health flower;
    Coroutine coroutine;
    Coroutine freezeCoroutine;

    private AnimationLogic _shootAnimation;

    private void Start()
    {
        _shootAnimation = GetComponent<AnimationLogic>();
        _rigidbody = GetComponent<Rigidbody>();
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
                        coroutine = StartCoroutine(Atack());
                    }
                }
            }
            else
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
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
            if (_shootAnimation != null)
                _shootAnimation.PlayAttackAnimation();
            flower.GetComponent<Health>().TakeDamage(_damage + PlayerPrefs.GetFloat("MultiplierAtkDamage", 1));
            yield return new WaitForSeconds(_fireRate + PlayerPrefs.GetFloat("MultiplierAtkSpeed", 1));
        }
    }

    public void Freeze()
    {
        _freezeTimeLast = _freezeTime;
        if (freezeCoroutine == null)
            freezeCoroutine = StartCoroutine(FreezeCoroutine());
    }

    private IEnumerator FreezeCoroutine()
    {
        _moveSpeed -= _freezeSpeedDebuff;
        while (_freezeTimeLast > 0)
        {
            yield return new WaitForSeconds(1);
            _freezeTimeLast--;
        }
        _moveSpeed += _freezeSpeedDebuff;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_vision.position, _sphereRadius);
    }
}