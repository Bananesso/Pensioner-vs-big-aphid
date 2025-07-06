using System.Collections;
using UnityEngine;

public class SplashBullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shootParticles;
    [SerializeField] private ParticleSystem _explosionParticles;
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

    private void OnTriggerEnter(Collider other) //��������� ����� �����, � �������� ������ ����
    {
        Health health = other.GetComponent<Health>();
        StartCoroutine(ParticlePlay(_shootParticles, _shootParticlePlayTime));
        _shootSound.Play();
        if (health != null && health.Enemy)
        {
            health.TakeDamage(_damageCurrentEnemy);
        }
        StartCoroutine(Explode());
        Destroy(gameObject);
    }

    public IEnumerator Explode() //����� � ��������� ����� ������������ ������ (������� ����, � �������� ������ ����)
    {
        yield return new WaitForSeconds(_timeUntillExplosion);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        _explosionSound.Play();
        StartCoroutine(ParticlePlay(_explosionParticles, _explosionParticlePlayTime));

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

    private IEnumerator ParticlePlay(ParticleSystem particles, float particlePlayTime) //��������
    {
        particles.Play();
        yield return new WaitForSeconds(particlePlayTime);
        particles.Stop();
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
}