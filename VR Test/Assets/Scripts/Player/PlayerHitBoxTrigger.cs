using UnityEngine;

public class PlayerHitBoxTrigger : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet")) return;
        _health.TakeDamage(_damage);
    }
}