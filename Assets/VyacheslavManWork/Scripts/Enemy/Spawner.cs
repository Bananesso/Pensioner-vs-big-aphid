using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _enemySpawnSpeed = 20;
    [SerializeField] private GameObject _enemyPrefab;

    private void Start()
    {
        StartCoroutine(EnemiesSpawning());
    }

    private IEnumerator EnemiesSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(_enemySpawnSpeed);
            Instantiate(_enemyPrefab, transform.position, Quaternion.LookRotation(transform.forward));
        }
    }
}