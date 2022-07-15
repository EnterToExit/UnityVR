using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private int _enemyMax;
    private int _enemyCount;

    private void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    private IEnumerator EnemyDrop()
    {
        while (_enemyCount < _enemyMax)
        {
            Instantiate(_enemy, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            _enemyCount++;
        }

        if (_enemyCount == _enemyMax)
        {
            Destroy(gameObject);
        }
    }
}