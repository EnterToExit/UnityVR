using UnityEngine;

public class HitBoxTrigger : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _damage;
    [SerializeField] private GameObject _gameObject;
    private Animator _animator;
    private static readonly int TakeDamage = Animator.StringToHash("takeDamage");

    public void Start()
    {
        _animator = _gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet")) return;
        _animator.SetTrigger(TakeDamage);
        _health.TakeDamage(_damage);
    }
}