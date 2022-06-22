using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float> Changed;
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
        Debug.Log("_currentHealth = _maxHealth;");
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log("Take v rot");
        Changed?.Invoke(_currentHealth);
    }
}