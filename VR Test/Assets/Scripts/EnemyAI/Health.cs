using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float> Changed;
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Changed?.Invoke(_currentHealth);
    }

    public float GiveCurrentHealths()
    {
        return _currentHealth;
    }
}