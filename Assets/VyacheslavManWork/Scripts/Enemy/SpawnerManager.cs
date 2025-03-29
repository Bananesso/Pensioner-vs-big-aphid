using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private Spawner[] _spawners;
    [SerializeField] private int _enableSpawnerTime;

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
