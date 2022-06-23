using UnityEngine;

public class HitBoxTrigger : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _health.TakeDamage(_damage);
        }
    }
}
