using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private Spawner[] _spawners;
    [SerializeField] private int _enableSpawnerTime = 15;

    private void Start()
    {
        StartCoroutine(EnableSpawner());
    }
    private IEnumerator EnableSpawner()
    {
        foreach (Spawner spawner in _spawners)
        {
            spawner.gameObject.SetActive(true);
            yield return new WaitForSeconds(_enableSpawnerTime);
        }
    }
}