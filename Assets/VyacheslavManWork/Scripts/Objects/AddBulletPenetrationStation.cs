using UnityEngine;

public class AddBulletPenetrationStation : MonoBehaviour
{
    [SerializeField] private int _bulletPenetrationAddition = 1;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private ParticleSystem _particles;

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet != null && bullet.CanBeFired)
        {
            bullet.BulletPenetration += _bulletPenetrationAddition;
            _audio.Play();
            _particles.Play();
        }
    }
}