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
		float newX;
        float newZ;

		foreach (GameObject spawner in _spawners)
        {
			newX = spawner.transform.position.x;
			newZ = spawner.transform.position.z;
			transform.position = new Vector3(newX, transform.position.y, newZ);

            _timer = _enableSpawnerTime;
            while (_timer > 0)
            {
                _timerVisual.text = _timer.ToString();
                _timer--;
                yield return new WaitForSeconds(1);
            }
			spawner.gameObject.SetActive(true);
		}
        Destroy(_timerVisual);
    }
}