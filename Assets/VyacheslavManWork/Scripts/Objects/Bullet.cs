using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool CanBeFired = true;
    public bool IsFired;

    public int BulletPenetration = 1;

    [SerializeField] private ParticleSystem _fire;
    [SerializeField] private ParticleSystem _shootParticles;
    [SerializeField] private float _particlesPlayTime = 1;
    [SerializeField] private AudioSource _shootSound;

    [SerializeField] private float _bulletLifeTime = 7;
    [SerializeField] private float _damage;
    [SerializeField] private float _addDamageWhenFired;

    [SerializeField] private float _freezeTime = 2f;
    [SerializeField] private bool _freeze = false;

    private void Awake()
    {
        Destroy(gameObject, _bulletLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        _shootSound.Play();
        StartCoroutine(ParticlePlay(_shootParticles, _particlesPlayTime));
        Health health = other.GetComponent<Health>();

        if (health != null && health.Enemy)
        {
            health.TakeDamage(_damage);
            if (_freeze)
                StartCoroutine(EnemyFreeze(other.GetComponent<EnemyAI>()));
            if (IsFired)
                health.Fired();
        }

        BulletPenetration--;
        if (BulletPenetration == 0)
            Destroy(gameObject);
    }

    IEnumerator EnemyFreeze(EnemyAI enemy) //заморозка врага
    {
        enemy.enabled = false;
        yield return new WaitForSeconds(_freezeTime);
        enemy.enabled = true;
    }

    private IEnumerator ParticlePlay(ParticleSystem particles, float particlePlayTime) //партиклы
    {
        particles.Play();
        yield return new WaitForSeconds(particlePlayTime);
        particles.Stop();
    }

    public void Fired() //зажечь пулю (через другой код)
    {
        IsFired = true;
        _fire.Play();
        _damage += _addDamageWhenFired;
    }
}