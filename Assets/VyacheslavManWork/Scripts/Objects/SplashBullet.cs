using System.Collections;
using UnityEngine;

public class SplashBullet : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private ParticleSystem _explosionParticlesPrefab;
    [SerializeField] private float _explosionParticlePlayTime = 3;

    [Header("����")]
    [SerializeField] private AudioSource _explosionSound;

    [Header("����")]
    [SerializeField] private float _bulletLifeTime = 6;
    [SerializeField] private float _radius;
    [SerializeField] private float _damageCurrentEnemy;
    [SerializeField] private float _damageNearEnemy;

    private void Awake()
    {
        StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter(Collider other) //��������� ����� �����, � �������� ������ ����
    {
        Health health = other.GetComponent<Health>();

        if (health != null && health.Enemy)
        {
            _explosionSound.transform.SetParent(null);
            health.TakeDamage(_damageCurrentEnemy);
            Explode();
        }
    }

    private void Explode() //����� � ��������� ����� ������������ ������ (������� ����, � �������� ������ ����)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        SoundsPlay(_explosionSound);
        ParticlePlay(_explosionParticlesPrefab, _explosionParticlePlayTime);

        foreach (Collider collider in colliders)
        {
            Health hp = collider.GetComponent<Health>();

            if (hp != null && hp.Enemy)
            {
                hp.TakeDamage(_damageNearEnemy);
            }
        }
        Destroy(gameObject);
    }

    private void ParticlePlay(ParticleSystem particlePrefab, float particlePlayTime) //��������
    {
        ParticleSystem particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particleInstance.Play();
        Destroy(particleInstance.gameObject, particlePlayTime);
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_bulletLifeTime);
        Explode();
    }

    private void SoundsPlay(AudioSource sound)
    {
        sound.Play();
        Destroy(sound.gameObject, 5);
    }

    private void OnDrawGizmosSelected()
    {
        Color color = Color.red;
        color.a = 0.4f;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}