using System;
using UnityEngine;

public class Health : MonoBehaviour
{
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
        if (this.GetComponent<EnemyAI>() != null)
        {
            _listikiPodschet.KolichestvoListikov += _listiki;
            _listikiPodschet.KolichestvoMaterialov += _materials;
        }
    }

    public float GetHealthInParts()
    {
        return _currentHealth / _maxHealth;
    }
}