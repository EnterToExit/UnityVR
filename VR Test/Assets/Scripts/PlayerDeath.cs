using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _deathUI;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.Changed += OnHealthChanged;
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
        _deathUI.SetActive(true);
    }
}