using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DamageReduction : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float damageMultiplier;
    private Health _health;
    private XRGrabInteractable _grabInteractable;
    private Rigidbody _rigidbody;
    private PlayerSFXController _playerSFXController;

    private void Awake()
    {
        _health = GameObject.FindWithTag("PlayerDeathController").GetComponent<Health>();
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerSFXController = GameObject.FindWithTag("Player").GetComponent<PlayerSFXController>();
    }

    private void OnEnable()
    {
        if (_grabInteractable == null) return;
        _grabInteractable.selectEntered.AddListener(ReduceDamage);
    }

    private void ReduceDamage(SelectEnterEventArgs selectEnterEventArgs)
    {
        // Prevents effect duplication by grabbing with 2 hands simultaneously
        _grabInteractable.selectEntered.RemoveListener(ReduceDamage);

        _health.damageMultiplier *= damageMultiplier;
        _playerSFXController.DamageReductionSound();

        // Disables physics and makes the object invisible so that it appears consumed
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        Invoke("RemoveReduction", duration);
    }

    private void RemoveReduction()
    {
        _health.damageMultiplier /= damageMultiplier;
        _playerSFXController.DamageReductionRemovedSound();
        Destroy(gameObject);
    }
}