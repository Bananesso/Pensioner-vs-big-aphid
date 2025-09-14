using System.Collections;
using UnityEngine;

public class MilitaryButton : MonoBehaviour, IInteractWithObj
{
    [Header("Настройки атаки")]
    [SerializeField] private GameObject _rocket;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private int _rocketSpeed;
    [SerializeField] private float _attackSpeed;
    
    private bool _canShoot = true;

    [Header("Обязательно должно быть назначено!")]
    [SerializeField] private AnimationLogic _pressAnimation;

    public void Interact()
    {
        if (_canShoot)
        {
            _canShoot = false;
            _pressAnimation.PlayAttackAnimation();
            GameObject BulletInstance = Instantiate(_rocket, _firePoint.position, Quaternion.identity);
            BulletInstance.GetComponent<Rigidbody>().AddForce(_firePoint.transform.forward * _rocketSpeed);
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_attackSpeed);
        _canShoot = true;
    }
}