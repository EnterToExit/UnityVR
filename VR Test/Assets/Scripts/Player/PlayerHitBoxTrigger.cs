using UnityEngine;

public class PlayerHitBoxTrigger : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _damage;
    private PlayerSFXController _playerSFXController;

    private void Awake()
    {
        _playerSFXController = GameObject.FindWithTag("Player").GetComponent<PlayerSFXController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet")) return;
        _health.TakeDamage(_damage);
        if (_health.GiveCurrentHealths() > 0)
        {
            _playerSFXController.TakeDamageSound();
        }
    }
}