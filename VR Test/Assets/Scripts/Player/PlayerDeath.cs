using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _deathUI;
    private PlayerSFXController _playerSFXController;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.Changed += OnHealthChanged;
        _playerSFXController = GameObject.FindWithTag("Player").GetComponent<PlayerSFXController>();
    }

    private void OnDestroy()
    {
        _health.Changed -= OnHealthChanged;
    }

    private void OnHealthChanged(float value)
    {
        if (value > 0) return;
        PlayDeathEffect();
    }

    private void PlayDeathEffect()
    {
        Time.timeScale = 0;
        _playerSFXController.DeathSound();
        _deathUI.SetActive(true);
    }
}