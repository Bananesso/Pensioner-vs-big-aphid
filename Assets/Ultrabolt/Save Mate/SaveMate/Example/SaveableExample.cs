using UnityEngine;
using SaveMate;

namespace SaveMate
{
	public class SaveableExample : MonoBehaviour, ISaveable
	{
		[Header("Saveable")]
		[SerializeField] private float health = 100;
		[SerializeField] private string characterName = "Ultrabolt";
		[SerializeField] private Color characterColor;

		[Header("Non Saveable")]
		[SerializeField] private int score;

		[Header("Resources"), Tooltip("Prefab Name from Resources folder, Keep it null if you won't this object to respawn again when you load the game.")]
		[SerializeField] private string prefabName;
		public string PrefabName => prefabName;

		[SerializeField, Tooltip("Leave it empty and this script will automatically set random id, or you can set yours!.")]
		private string uniqueID;
		public string UniqueID
		{
			get => uniqueID;
			set => uniqueID = value;
		}

		private void Awake()
		{
			if (string.IsNullOrEmpty(uniqueID))
				uniqueID = System.Guid.NewGuid().ToString();
		}

		public object SaveData()
		{
			return new DataToSave
			{
				health = health,
				characterName = characterName,
				characterColor = characterColor,
				position = transform.position,
				rotation = transform.rotation.eulerAngles
			};
		}

		public void LoadData(object data)
		{
			if (data == null) return;

			if (data is DataToSave d)
			{
				health = d.health;
				characterName = d.characterName;
				characterColor = d.characterColor;
				transform.position = d.position;
				transform.rotation = Quaternion.Euler(d.rotation);
			}
		}

		[System.Serializable]
		public class DataToSave
		{
			public float health;
			public string characterName;
			public Vector3 position, rotation;
			public Color characterColor;
		}
	}
}
