using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _range = 1f;
    [SerializeField] private int _fireRate;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _tempSpeed;
    [SerializeField] private Transform _vision;
    [SerializeField] private float _sphereRadius;
    private Health _health;
    public bool IsMoving;
    public event Action OnAtack;
    private Rigidbody _rigidbody;
    Health flower;
    Coroutine coroutine;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(Check());
    }
    private void Update()
    {
        _rigidbody.velocity = transform.forward * _tempSpeed * PlayerPrefs.GetFloat("MultiplierMooveSpeed");
    }

    private IEnumerator Check()
    {
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(_vision.position, _sphereRadius);
            if (colliders.Length > 0)
            {
                flower = colliders[0].GetComponent<Health>();
                if (flower != null)
                {
                    if (coroutine == null)
                    {
                        _tempSpeed = 0;
                        coroutine = StartCoroutine(Atack());
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
            }
            yield return new WaitForSeconds(0.5f);

        }
    }

    private IEnumerator Atack()
    {
        while (flower != null)
        {
            flower.GetComponent<Health>().TakeDamage(_damage*PlayerPrefs.GetFloat("MultiplierAtkDamage"));
            yield return new WaitForSeconds(_fireRate*PlayerPrefs.GetFloat("MultiplierAtkSpeed"));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_vision.position, _sphereRadius);
    }
}