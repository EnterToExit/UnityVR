using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float> Changed;
    [SerializeField] private float _maxHealth;
    private float _currentHealth;
    public float damageMultiplier = 1;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage * damageMultiplier;
        Changed?.Invoke(_currentHealth);
    }

    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
        Changed?.Invoke(_currentHealth);
    }

    public float GiveCurrentHealths()
    {
        return _currentHealth;
    }
}