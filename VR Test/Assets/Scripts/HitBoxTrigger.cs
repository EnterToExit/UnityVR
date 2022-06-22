using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitBoxTrigger : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("_health.TakeDamage(_damage);");
            _health.TakeDamage(_damage);
        }
    }
}
