using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float> Changed;
    [SerializeField] private float _maxHealth;
    private float _currentHealth;
    [NonSerialized] public float damageMultiplier = 1;
    private PlayerSFXController _playerSFXController;

    private void Awake()
    {
        if (gameObject.tag == "PlayerDeathController")
        {
            _playerSFXController = GameObject.FindWithTag("Player").GetComponent<PlayerSFXController>();
        }
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage * damageMultiplier;
        if (gameObject.tag == "PlayerDeathController" && _currentHealth > 0)
        {
            _playerSFXController.TakeDamageSound();
        }
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