using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Health))]

public class CharacterDeath : MonoBehaviour
{
    [SerializeField] private Health _health;

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
        if (value > 0)
            return;

        StartCoroutine(PlayDeathEffect());
    }

    private IEnumerator PlayDeathEffect()
    {
        yield return null;
        Destroy(gameObject);
    }
}