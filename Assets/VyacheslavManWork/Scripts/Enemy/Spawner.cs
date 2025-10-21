using System.Collections;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _enemySpawnSpeed = 20;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private TextMeshPro _timerVisual;
    private int _timeLast;

    private void OnEnable() => StartCoroutine(EnemiesSpawning());

    private IEnumerator EnemiesSpawning()
    {
        while (true)
        {
            _timeLast = _enemySpawnSpeed;
            while (_timeLast > 0)
            {
                _timerVisual.text = _timeLast.ToString();
                _timeLast--;
                yield return new WaitForSeconds(1);
            }
            Instantiate(_enemyPrefab, transform.position, Quaternion.LookRotation(transform.forward));
        }
    }
}