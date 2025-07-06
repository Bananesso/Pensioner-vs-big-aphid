using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool Enemy;

    private Coroutine _fired;
    [SerializeField] private ParticleSystem _fireParticles;
    [SerializeField] private float _fireDamage;
    [SerializeField] private int _fireTimes;
    [SerializeField] private int _fireTimesLast;
    [SerializeField] private float _fireSpeed;

    [SerializeField] private int _listiki;
    [SerializeField] private int _materials;

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth;

    private ListikiPodschet _listikiPodschet;

    public float CurrentHealth => _currentHealth;
    public bool IsAlive => _currentHealth > 0;

    public event Action OnHit;
    public event Action OnDie;
    private void Start()
    {
        _listikiPodschet = FindObjectOfType<ListikiPodschet>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;

        _currentHealth -= amount;
        OnHit?.Invoke();
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (!IsAlive) return;

        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
    }

    private void Die()
    {
        OnDie?.Invoke();
        Destroy(gameObject);
        _listikiPodschet.KolichestvoListikov += _listiki;
        _listikiPodschet.KolichestvoMaterialov += _materials;
    }

    public float GetHealthInParts()
    {
        return _currentHealth / _maxHealth;
    }

    public void Fired()
    {
        if (_fired == null)
        {
            _fired = StartCoroutine(Fire());
        }
        else
        {
            _fireTimesLast = _fireTimes;
        }
    }
    private IEnumerator Fire()
    {
        _fireParticles.Play();
        while (_fireTimes != 0)
        {
            _currentHealth -= _fireDamage;
            _fireTimesLast--;
            yield return new WaitForSeconds(_fireSpeed);
        }
        _fireParticles.Stop();
    }
}