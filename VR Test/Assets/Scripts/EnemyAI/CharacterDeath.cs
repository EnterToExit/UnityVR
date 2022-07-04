using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
public class CharacterDeath : MonoBehaviour
{
    [SerializeField] private Health _health;
    private Animator _animator;
    private bool _animationStarted;
    private static readonly int KillCharacter = Animator.StringToHash("killCharacter");
    private string _agentName;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _health.Changed += OnHealthChanged;
    }

    private void OnDestroy()
    {
        _health.Changed -= OnHealthChanged;
    }

    private void OnHealthChanged(float value)
    {
        if (value > 0) return;
        if (_animationStarted) return;
        _animationStarted = true;

        StartCoroutine(PlayDeathEffect());
    }

    private IEnumerator PlayDeathEffect()
    {
        yield return null;
        _animator.SetTrigger(KillCharacter);
    }

    private void KillCharacterFunc() //used by death animation
    {
        Destroy(gameObject);
    }

    private void DisableAllBrains()
    {
        Destroy(gameObject.GetComponentInChildren<HitBoxTrigger>());
        Destroy(gameObject.GetComponent<EnemyMovementAi>());
        Destroy(gameObject.GetComponent<EnemyAnimationController>());
        if (gameObject.name == "MeleeEnemy")
        {
            Destroy(gameObject.GetComponent<MeleeEnemyAttack>());
        }

        if (gameObject.name == "RangeEnemy")
        {
            Destroy(gameObject.GetComponent<RangeEnemyAttack>());
        }
    }
}