using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    [SerializeField] private string _agentTarget;
    private bool _attackAllowed;
    private Animator _animator;
    private Transform _player;
    private static readonly int Attack = Animator.StringToHash("attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.Find(_agentTarget).transform;
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
    }

    private void DisableBrains()
    {
        gameObject.GetComponent<EnemyMovementAi>().enabled = false;
    }

    private void AttackPlayer()
    {
        _animator.SetTrigger(Attack);
    }

    //Scripts below used by animation
    private void DecreaseHealths()
    {
        //TODO
    }

    private void LookAtPlayer()
    {
        var playerPosition = new Vector3( _player.position.x, transform.position.y, _player.position.z );
        transform.LookAt(playerPosition);
    }
}