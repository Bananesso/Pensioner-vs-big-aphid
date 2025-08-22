using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Пуля")]
    [SerializeField] private float _bulletLifeTime = 7;
    [SerializeField] private float _damage;

    [Header("Пробивная способность")]
    public int BulletPenetration = 1;

    [Header("Эффекты")]
    [SerializeField] private ParticleSystem _fire;
    [SerializeField] private ParticleSystem _shootParticles;
    [SerializeField] private float _shootParticlesPlayTime = 1;
    [SerializeField] private AudioSource _shootSound;

    [Header("Огонь")]
    public bool CanBeFired = true;
    public bool IsFired;
    [SerializeField] private float _addDamageWhenFired;

    [Header("Заморозка/оглушение")]
    [SerializeField] private bool _freeze = false;

    private void Awake()
    {
        Destroy(gameObject, _bulletLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health != null && health.Enemy)
        {
            if (_shootSound != null)
            {
                _shootSound.transform.SetParent(null);
                SoundsPlay();
            }

            ShootParticlePlay();

            health.TakeDamage(_damage);
            if (_freeze)
            {
                EnemyAI enemy = other.GetComponent<EnemyAI>();
                if (enemy != null)
                    enemy.Freeze();
            }
            if (IsFired)
                health.Fired();

            BulletPenetration--;
            if (BulletPenetration <= 0)
                Destroy(gameObject);
        }
    }

    private void ShootParticlePlay() //партиклы
    {
        ParticleSystem particleInstance = Instantiate(_shootParticles, transform.position, Quaternion.identity);
        particleInstance.Play();
        Destroy(particleInstance.gameObject, 0.25f);
    }

    private void SoundsPlay()
    {
        _shootSound.Play();
        Destroy(_shootSound.gameObject, 5);
    }

    public void Fired() //зажечь пулю (через другой код)
    {
        IsFired = true;
        _fire.Play();
        _damage += _addDamageWhenFired;
    }
}