using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _range = 1f;
    [SerializeField] private int _fireRate;
    private Health _health;
    public bool IsMoving;
    public event Action OnAtack;

    private void Awake()
    {
        
    }
    public void ShootAtEnemy()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(_fireRate);
    }

    private void PerformRaycast(Vector3 position, Vector3 direction)
    {
        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        Vector3 endPoint = position + direction * _range;

        if (Physics.Raycast(ray, out hit, _range))
        {
            HitBox targetHitBox = hit.collider.GetComponent<HitBox>();
            if (targetHitBox != null)
            {
                targetHitBox.OnRaycastHit(_damage);
            }

            endPoint = hit.point;
        }
    }
}