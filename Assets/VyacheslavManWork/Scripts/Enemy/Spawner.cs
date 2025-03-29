using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _enemySpawnSpeed;
    [SerializeField] private GameObject _enemyPrefab;

    private void Start()
    {
        StartCoroutine(EnemiesSpawning());
    }
    private IEnumerator EnemiesSpawning()
    {
        while (true)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.LookRotation(transform.forward));
            yield return new WaitForSeconds(20);
        }
    }
}