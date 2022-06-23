using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private string _agentTarget;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _shootForce;
    [SerializeField] private float _upwardForce;
    private bool _alreadyAttacked;
    private bool _enableAttack;
    private Animator _animator;
    private Transform _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find(_agentTarget).transform;
    }

    // private void FixedUpdate()
    // {
    //     var distance = Vector3.Distance(transform.position, _player.position);
    //     if (distance < _attackRange)
    //     {
    //         _agent.SetDestination(transform.position);
    //         transform.LookAt(_player);
    //         if (_alreadyAttacked) return;
    //         _animator.SetTrigger("attack");
    //         _alreadyAttacked = true;
    //         Invoke(nameof(ResetAttack), 8);
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        _animator.SetTrigger("attack");
    }

    private void RepeatAnimation()
    {
        _animator.SetTrigger("attack");
    }

    private void SpawnBullet()
    {
        var currentBullet = Instantiate(_projectile, _bulletSpawnPoint.position,
            Quaternion.identity).GetComponent<Rigidbody>();
        currentBullet.AddForce(_bulletSpawnPoint.forward * _shootForce, ForceMode.Impulse);
        currentBullet.AddForce(_bulletSpawnPoint.up * _upwardForce, ForceMode.Impulse);
    }
}