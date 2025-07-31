using System.Collections;
using UnityEngine;

public class SplashBullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shootParticles;
    [SerializeField] private ParticleSystem _explosionParticlesPrefab;
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioSource _explosionSound;

    [SerializeField] private float _bulletLifeTime = 6;

    [SerializeField] private float _explosionParticlePlayTime = 3;
    [SerializeField] private float _shootParticlePlayTime = 1;
    [SerializeField] private float _radius;
    [SerializeField] private float _timeUntillExplosion;
    [SerializeField] private float _damageCurrentEnemy;
    [SerializeField] private float _damageNearEnemy;

    [SerializeField] private float _freezeTime = 0.5f;
    [SerializeField] private bool _freeze = false;

    private void Awake()
    {
        StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter(Collider other) //нанесение урона врагу, в которого попала пуля
    {
        Health health = other.GetComponent<Health>();

        if (health != null && health.Enemy)
        {
            StartCoroutine(ParticlePlay(_shootParticles, _shootParticlePlayTime));
            _shootSound.Play();
            health.TakeDamage(_damageCurrentEnemy);
            StartCoroutine(Explode());
            Destroy(gameObject);
        }
    }

    public IEnumerator Explode() //взрыв и нанесение урона ближестоящим врагам (включая того, в которого попала пуля)
    {
        yield return new WaitForSeconds(_timeUntillExplosion);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        _explosionSound.Play();
        StartCoroutine(ParticlePlay(_explosionParticlesPrefab, _explosionParticlePlayTime));

        foreach (Collider collider in colliders)
        {
            Health hp = collider.GetComponent<Health>();

            if (hp.Enemy)
            {
                if (_freeze)
                    StartCoroutine(EnemyFreeze(collider.GetComponent<EnemyAI>()));
                hp.TakeDamage(_damageNearEnemy);
            }
        }
    }

    private IEnumerator ParticlePlay(ParticleSystem particlePrefab, float particlePlayTime) //партиклы
    {
        ParticleSystem particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.Play();
        yield return new WaitForSeconds(particleInstance.main.duration);
        //yield return new WaitForSeconds(particlePlayTime);
        Destroy(particleInstance.gameObject);
    }

    IEnumerator EnemyFreeze(EnemyAI enemy)
    {
        enemy.enabled = false;
        yield return new WaitForSeconds(_freezeTime);
        enemy.enabled = true;
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_bulletLifeTime);
        StartCoroutine(Explode());
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Color color = Color.red;
        color.a = 0.4f;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}