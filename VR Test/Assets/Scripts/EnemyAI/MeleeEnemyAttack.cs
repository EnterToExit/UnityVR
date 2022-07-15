using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAttack : MonoBehaviour
{
    [SerializeField] private string _agentTarget;
    [SerializeField] private float _agentDamage;
    private Animator _animator;
    private Transform _player;
    private Health _health;
    private NavMeshAgent _agent;
    private bool _attackAllowed;
    private static readonly int Attack = Animator.StringToHash("attack");

    private void Awake()
    {
        _player = GameObject.Find(_agentTarget).transform;
        _health = _player.GetComponentInChildren<Health>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Invoke(nameof(DisableBrains), 1f);
        Invoke(nameof(AttackPlayer), 1);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        gameObject.GetComponent<EnemyMovementAi>().enabled = true;
        _agent.radius = 0.5f;
    }

    private void DisableBrains()
    {
        gameObject.GetComponent<EnemyMovementAi>().enabled = false;
        _agent.radius = 0.001f;
    }

    private void AttackPlayer()
    {
        _animator.SetTrigger(Attack);
    }

    //Scripts below used by animation
    private void DecreaseHealths()
    {
        _health.TakeDamage(_agentDamage);
    }

    private void LookAtPlayer()
    {
        var playerPosition = new Vector3(_player.position.x, transform.position.y, _player.position.z);
        transform.LookAt(playerPosition);
    }
}