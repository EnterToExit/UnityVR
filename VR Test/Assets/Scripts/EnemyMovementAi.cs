using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMovementAi : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private string _agentTarget;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _walkPointRange;
    [SerializeField] private float _sightRange;
    [SerializeField] private float _requiredRange;
    [SerializeField] private bool _attacking;
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

        if (distance > _sightRange && distance > _requiredRange) Patroling();
        if (distance < _sightRange && distance > _requiredRange) ChasePlayer();
        if (distance < _sightRange && distance < _requiredRange)
        {
            _agent.SetDestination(agentPosition);
        }
    }

    private void Patroling()
    {
        _agent.speed = 2f;
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
        if (_attacking) return;
        _agent.SetDestination(_player.position);
        _agent.speed = 3.5f;
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
        Gizmos.DrawWireSphere(position, _requiredRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position, _sightRange);
    }
}