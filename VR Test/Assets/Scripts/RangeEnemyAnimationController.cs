using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Windows.Speech;

public class RangeEnemyAnimationController : MonoBehaviour
{
    [SerializeField] private float _velocity;
    private Animator _animator;
    private int _velocityHash;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _velocityHash = Animator.StringToHash("charVelocity");
        StartCoroutine(CalcSpeed());
    }
    //asdasdasdasd

    public void Update()
    {
        _animator.SetFloat(_velocityHash, _velocity);
    }

    private IEnumerator CalcSpeed()
    {
        while (true)
        {
            var prevPos = transform.position;
            yield return new WaitForEndOfFrame();
            _velocity = (Vector3.Distance(transform.position, prevPos) / Time.deltaTime) / 10;
        }
    }
}