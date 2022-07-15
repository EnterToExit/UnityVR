using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class CharacterDeath : MonoBehaviour
{
    [SerializeField] private Health _health;
    private Animator _animator;
    private Vector3 _deathPosition;
    private bool _animationStarted;
    private string _agentName;
    private static readonly int KillCharacter = Animator.StringToHash("killCharacter");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _health.Changed += OnHealthChanged;
    }

    private void Update()
    {
        if (!_animationStarted) return;
        transform.position = _deathPosition;
    }

    private void OnDestroy()
    {
        _health.Changed -= OnHealthChanged;
    }

    private void OnHealthChanged(float value)
    {
        if (value > 0) return;

        StartCoroutine(PlayDeathEffect());
        Invoke(nameof(KillCharacterFunc), 5f);
    }

    private IEnumerator PlayDeathEffect()
    {
        yield return null;
        _animator.SetTrigger(KillCharacter);
        Destroy(gameObject.GetComponentInChildren<HitBoxTrigger>());
    }

    private void KillCharacterFunc() //used by death animation
    {
        Destroy(gameObject);
    }

    private void DisableAllBrains()
    {
        _animationStarted = true;
        _deathPosition = transform.position;
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