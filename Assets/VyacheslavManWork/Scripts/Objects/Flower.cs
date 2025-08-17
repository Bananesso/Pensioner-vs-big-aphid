using System.Collections;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [Header("�������� ����� (��� ��������� �������� (������� ���������� ��������m, ����� �������)")]
    [SerializeField] private float _timeUntillShot;
    [SerializeField] private float _timeAfterShot;

    [Header("��������� ��������")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _timeElectrolyzed;

    [Header("�����������")]
    [SerializeField] private bool _electrolyzed;

    private AnimationLogic _shootAnimation;
    private Coroutine _shootingCoroutine;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Appear");
        StartCoroutine(Shoot());
    }

    public void Reload() //����� ������������ �� ���� �������������
    {
        _electrolyzed = true;
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
        }
        _shootingCoroutine = StartCoroutine(LooseEnergy());
    }

    private IEnumerator LooseEnergy() //���� ������������
    {
        yield return new WaitForSeconds(_timeElectrolyzed);
        _electrolyzed = false;
    }

    private IEnumerator Shoot() //��������
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
}