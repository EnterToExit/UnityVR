using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RangeEnemyAi : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private string _agentTarget;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _walkPointRange;
    [SerializeField] private float _timeBetweenAttacks;
    [SerializeField] private float _sightRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _shootForce;
    [SerializeField] private float _upwardForce;
    private Transform _player;
    private Vector3 _walkPoint;
    private bool _alreadyAttacked;
    private bool _walkPointSet;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find(_agentTarget).transform;
    }

    private void FixedUpdate()
    {
        //Check for sight and attack range
        var agentPosition = transform.position;
        var playerPosition = _player.position;
        var distance = Vector3.Distance(agentPosition, playerPosition);

        if (distance > _sightRange && distance > _attackRange) Patroling();
        if (distance < _sightRange && distance > _attackRange) ChasePlayer();
        if (distance < _sightRange && distance < _attackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!_walkPointSet) SearchWalkPoint();
        if (_walkPointSet)
            _agent.SetDestination(_walkPoint);
        var distanceToWalkPoint = transform.position - _walkPoint;
        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        _agent.SetDestination(transform.position);
        transform.LookAt(_player);
        if (_alreadyAttacked) return;
        var currentBullet = Instantiate(_projectile, _bulletSpawnPoint.position,
            Quaternion.identity).GetComponent<Rigidbody>();
        currentBullet.AddForce(_bulletSpawnPoint.forward * _shootForce, ForceMode.Impulse);
        currentBullet.AddForce(_bulletSpawnPoint.up * _upwardForce, ForceMode.Impulse);
        _alreadyAttacked = true;
        Invoke(nameof(ResetAttack), _timeBetweenAttacks);
    }

    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    private void SearchWalkPoint()
    {
        var randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        var randomX = Random.Range(-_walkPointRange, _walkPointRange);
        var position = transform.position;
        _walkPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);
        if (Physics.Raycast(_walkPoint, -transform.up, 2f, _whatIsGround))
            _walkPointSet = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var position = transform.position;
        Gizmos.DrawWireSphere(position, _attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position, _sightRange);
    }
}