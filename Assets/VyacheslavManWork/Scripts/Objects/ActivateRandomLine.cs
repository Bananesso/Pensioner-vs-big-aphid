using System.Collections;
using UnityEngine;

public class ActivateRandomLine : MonoBehaviour
{
    [SerializeField] private GameObject[] _lineSpawners = new GameObject[3];
    [SerializeField] private int _timeAfterDisactivate;
    [SerializeField] private int _activateTime;

    private int _previousLine = 0;


    void Start()
    {
        StartCoroutine(ActivateLine());
    }

    private IEnumerator ActivateLine()
    {
        while (true)
        {
            _lineSpawners[_previousLine].SetActive(false);
            yield return new WaitForSeconds(_timeAfterDisactivate);
            _previousLine = Random.Range(0, _lineSpawners.Length);
            _lineSpawners[_previousLine].SetActive(true);
            yield return new WaitForSeconds(_activateTime);
        }
    }
}