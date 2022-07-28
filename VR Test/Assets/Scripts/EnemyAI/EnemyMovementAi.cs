using System.Collections;
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
    private float _velocity;
    private int _idleTime;
    private bool _alreadyAttacked;
    private bool _walkPointSet;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find(_agentTarget).transform;
        StartCoroutine(IdleChecker());
    }

    private void FixedUpdate()
    {
        var agentPosition = transform.position;
        var playerPosition = _player.position;
        var distance = Vector3.Distance(agentPosition, playerPosition);
        var inSight = Vector3.SignedAngle(playerPosition - agentPosition,
            transform.forward, new Vector3(0, 0, 1)) < 90f;
        if (_idleTime == 3)
        {
            SearchWalkPoint();
            _idleTime = 0;
        }
        if (distance > _sightRange && distance > _requiredRange) Patroling();
        if (distance < _sightRange && distance > _requiredRange && inSight) ChasePlayer();
        if (distance < _sightRange && distance < _requiredRange)
        {
            _agent.SetDestination(agentPosition);
        }
    }

    private IEnumerator IdleChecker()
    {
        while (true)
        {
            var prevPos = transform.position;
            yield return new WaitForEndOfFrame();
            _velocity = (Vector3.Distance(transform.position, prevPos) / Time.deltaTime) / 10;
            if (_velocity == 0)
            {
                _idleTime += 1;
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void Patroling()
    {
        _agent.speed = gameObject.name == "CasterEnemy" ? 1f : 3.5f;
        if (!_walkPointSet) SearchWalkPoint();
        if (_walkPointSet)
            _agent.SetDestination(_walkPoint);
        var distanceToWalkPoint = transform.position - _walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }

    private void ChasePlayer()
    {
        if (_attacking) return;
        _agent.SetDestination(_player.position);
        _agent.speed = gameObject.name == "CasterEnemy" ? 2f : 3.5f;
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