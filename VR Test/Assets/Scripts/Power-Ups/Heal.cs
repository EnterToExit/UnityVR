using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Heal : MonoBehaviour
{
    [SerializeField] private float healAmount;
    private Health _health;
    private XRGrabInteractable _grabInteractable;

    private void Awake()
    {
        _health = GameObject.Find("PlayerDeathController").GetComponent<Health>();
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (_grabInteractable == null) return;
        _grabInteractable.selectEntered.AddListener(Consume);
    }

    private void Consume(SelectEnterEventArgs selectEnterEventArgs)
    {
        _health.Heal(healAmount);
        Destroy(gameObject);
    }
}
