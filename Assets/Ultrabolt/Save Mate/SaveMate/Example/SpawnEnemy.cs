using UnityEngine;

namespace SaveMate
{
	public class SpawnEnemy : MonoBehaviour
	{
		public GameObject enemy;
		public float randomValue => Random.Range(-5f, 5f);

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				Vector3 pos = new Vector3(randomValue, 1, randomValue);
				Instantiate(enemy, pos, Quaternion.identity);
			}
		}
	}
}
