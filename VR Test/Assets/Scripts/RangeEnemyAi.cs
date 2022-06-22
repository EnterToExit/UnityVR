using System;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RangeEnemyAi : MonoBehaviour
{
    [SerializeField] private string _agentTarget;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private LayerMask _whatIsGround;
    private Transform _player;

    //Patroling
    public Vector3 walkPoint;
    public float walkPointRange;
    private bool _walkPointSet;

    //Attacking
    public GameObject projectile;
    public float timeBetweenAttacks;
    private bool _alreadyAttacked;

    //States
    public float sightRange;
    public float attackRange;

    //bullet
    public Transform bulletSpawnPoint;
    public float shootForce;
    public float upwardForce;

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
        var sightRangeDist = Vector3.Distance(agentPosition, playerPosition);
        var attackRangeDist = Vector3.Distance(agentPosition, playerPosition);

        if (!(sightRangeDist < sightRange) && !(attackRangeDist < attackRange)) Patroling(); //TODO agentPosition fix
        if (sightRangeDist < sightRange && !(attackRangeDist < attackRange)) ChasePlayer();
        if (sightRangeDist < sightRange && attackRangeDist < attackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!_walkPointSet) SearchWalkPoint();
        if (_walkPointSet)
            _agent.SetDestination(walkPoint);
        var distanceToWalkPoint = transform.position - walkPoint;
        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        var randomZ = Random.Range(-walkPointRange, walkPointRange);
        var randomX = Random.Range(-walkPointRange, walkPointRange);
        var position = transform.position;
        walkPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, _whatIsGround))
            _walkPointSet = true;
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
        var currentBullet = Instantiate(projectile, bulletSpawnPoint.position,
            Quaternion.identity).GetComponent<Rigidbody>();
        currentBullet.AddForce(bulletSpawnPoint.forward * shootForce, ForceMode.Impulse);
        currentBullet.AddForce(bulletSpawnPoint.up * upwardForce, ForceMode.Impulse);
        _alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var position = transform.position;
        Gizmos.DrawWireSphere(position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position, sightRange);
    }
}