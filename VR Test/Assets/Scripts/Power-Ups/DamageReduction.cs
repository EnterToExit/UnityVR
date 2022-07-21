using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DamageReduction : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float _damageMultiplier;
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
        _grabInteractable.selectEntered.AddListener(ReduceDamage);
    }

    private void ReduceDamage(SelectEnterEventArgs selectEnterEventArgs)
    {
        _health.damageMultiplier *= _damageMultiplier;

        // Makes the object invisible so that it appears consumed
        gameObject.transform.localScale = new Vector3(0, 0, 0);

        Invoke("RemoveReduction", duration);
    }

    private void RemoveReduction()
    {
        _health.damageMultiplier /= _damageMultiplier;
        Destroy(gameObject);
    }
}