using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Heal : MonoBehaviour
{
    [SerializeField] private float healAmount;
    private Health _health;
    private XRGrabInteractable _grabInteractable;
    private PlayerSFXController _playerSFXController;

    private void Awake()
    {
        _health = GameObject.FindWithTag("PlayerDeathController").GetComponent<Health>();
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _playerSFXController = GameObject.FindWithTag("Player").GetComponent<PlayerSFXController>();
    }

    private void OnEnable()
    {
        if (_grabInteractable == null) return;
        _grabInteractable.selectEntered.AddListener(Consume);
    }

    private void Consume(SelectEnterEventArgs selectEnterEventArgs)
    {
        // Prevents effect duplication by grabbing with 2 hands simultaneously
        _grabInteractable.selectEntered.RemoveListener(Consume);

        _health.Heal(healAmount);
        _playerSFXController.HealSound();
        Destroy(gameObject);
    }
}
