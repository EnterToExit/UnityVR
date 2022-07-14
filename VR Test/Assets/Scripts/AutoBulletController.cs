using UnityEngine;

public class AutoBulletController : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _target;
    [SerializeField] private float _translateSpeed;
    [SerializeField] private string _bulletTarget;

    private void Awake()
    {
        _target = GameObject.Find(_bulletTarget).transform;
    }

    private void FixedUpdate()
    {
        HandleTranslation();
    }

    private void HandleTranslation()
    {
        var target = _target.TransformPoint(_offset);
        transform.position = Vector3.Lerp(transform.position, target, _translateSpeed * Time.deltaTime);
    }
}