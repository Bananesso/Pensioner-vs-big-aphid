using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth;

    public float CurrentHealth => _currentHealth;
    public bool IsAlive => _currentHealth > 0;

    public GameObject Bed;
    private int _lifes;

    public event Action OnHit;
    public event Action OnDie;
    private void Start()
    {
        _currentHealth = _maxHealth;
        _lifes = 3;
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
        _lifes--;
        this.transform.position = Bed.transform.position;
        if (_lifes == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public float GetHealthInParts()
    {
        return _currentHealth / _maxHealth;
    }
}