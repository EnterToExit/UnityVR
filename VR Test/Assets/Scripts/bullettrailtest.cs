using System.Collections;
using UnityEngine;

public class bullettrailtest : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _reloadSpeed;
    [SerializeField] private float _bulletSpeed;
 
    private void Awake()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true) {
            Instantiate(_bullet, transform.position, transform.rotation).GetComponent<Rigidbody>()
                .AddForce(transform.forward * _bulletSpeed);
            yield return new WaitForSeconds(_reloadSpeed);
        }
    }
}
