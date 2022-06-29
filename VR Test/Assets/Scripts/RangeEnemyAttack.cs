using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private string _agentTarget;
    [SerializeField] private float _shootForce;
    private bool _attackAllowed;
    private Animator _animator;
    private Transform _player;
    private NavMeshAgent _agent;
    private static readonly int Attack = Animator.StringToHash("attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.Find(_agentTarget).transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Invoke(nameof(DisableBrains), 1);
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
    private void SpawnBullet()
    {
        var spawnPosition = _bulletSpawnPoint.position;
        var directionToShoot = _player.position - spawnPosition;
        var currentBullet = Instantiate(_projectile, spawnPosition,
            Quaternion.identity).GetComponent<Rigidbody>();
        currentBullet.AddForce(directionToShoot.normalized * _shootForce, ForceMode.Impulse);
        currentBullet.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
    
    private void SpawnBulletAuto()
    {
        var spawnPosition = _bulletSpawnPoint.position;
        var directionToShoot = _player.position - spawnPosition;
        var currentBullet = Instantiate(_projectile, spawnPosition,
            Quaternion.identity).GetComponent<Rigidbody>();
        currentBullet.AddForce(directionToShoot.normalized * _shootForce, ForceMode.Impulse);
        currentBullet.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    private void LookAtPlayer()
    {
        var playerPosition = new Vector3(_player.position.x, transform.position.y, _player.position.z);
        transform.LookAt(playerPosition);
    }
}