using System.Collections;
using UnityEngine;
using TMPro;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawners;
    [SerializeField] private int _enableSpawnerTime = 15;
    [SerializeField] private TextMeshPro _timerVisual;
    private int _timer;

    private void Start()
    {
        StartCoroutine(EnableSpawner());
    }
    private IEnumerator EnableSpawner()
    {
        foreach (GameObject spawner in _spawners)
        {
            this.transform.position = spawner.transform.position;
            spawner.gameObject.SetActive(true);
            _timer = _enableSpawnerTime;
            while (_timer > 0)
            {
                _timerVisual.text = _timer.ToString();
                _timer--;
                yield return new WaitForSeconds(1);
            }
        }
        Destroy(_timerVisual);
    }
}