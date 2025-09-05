using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawners;
    [SerializeField] private int _enableSpawnerTime = 15;

    private void Start()
    {
        StartCoroutine(EnableSpawner());
    }
    private IEnumerator EnableSpawner()
    {
        foreach (GameObject spawner in _spawners)
        {
            spawner.gameObject.SetActive(true);
            yield return new WaitForSeconds(_enableSpawnerTime);
        }
    }
}