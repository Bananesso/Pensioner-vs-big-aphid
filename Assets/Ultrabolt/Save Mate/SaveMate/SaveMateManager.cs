using System.IO;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace SaveMate
{
	[DisallowMultipleComponent]
	public class SaveMateManager : MonoBehaviour
	{
		public static SaveMateManager instance;

		private enum SaveType { JSON, BINARY };
		[SerializeField]
		private SaveType saveType = SaveType.JSON;

		public string worldName = "My Save";
		public bool dontDestroyOnLoad;

		//Save on very special place :*
		private string SavePath => $"{Application.persistentDataPath}/{UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}/{worldName}.sm";

		void Awake()
		{
			if (string.IsNullOrEmpty(worldName))
				worldName = "My Save";

			if (instance != null)
			{
				Destroy(gameObject);
				Debug.LogWarning("SaveMateManager here!: i've found another SaveMateManager so i'll destroy mysel °^°");
			}
			else
				instance = this;

			if (dontDestroyOnLoad)
				DontDestroyOnLoad(gameObject);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.S))
				SaveGame();

			if (Input.GetKeyDown(KeyCode.L))
				LoadGame();
		}

		[ContextMenu("Save Game")]
		public void SaveGame()
		{
			Directory.CreateDirectory(Path.GetDirectoryName(SavePath));

			var saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
			var wrapper = new SaveWrapper();

			var usedIDs = new HashSet<string>();
			foreach (var s in saveables)
			{
				if (string.IsNullOrEmpty(s.UniqueID) || !usedIDs.Add(s.UniqueID))
				{
					string newID = System.Guid.NewGuid().ToString();
					s.UniqueID = newID;
					usedIDs.Add(newID);
					Debug.LogWarning($"Generated new UniqueID for {((MonoBehaviour)s).name}: {newID}");
				}
			}

			foreach (var s in saveables)
			{
				var go = ((MonoBehaviour)s).gameObject;
				var obj = new SaveObject
				{
					id = s.UniqueID,
					prefabPath = s.PrefabName,
					data = new ScriptData(s.SaveData()),
					position = new SerializableVector3(go.transform.position),
					rotation = new SerializableQuaternion(go.transform.rotation)
				};

				wrapper.objects.Add(obj);
			}

			if (saveType == SaveType.JSON)
				File.WriteAllText(SavePath, JsonUtility.ToJson(wrapper, true));
			else if (saveType == SaveType.BINARY)
			{
				using FileStream fs = new(SavePath, FileMode.Create);
				var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				bf.Serialize(fs, wrapper);
			}

			Debug.Log($"Game Saved to {SavePath} using {saveType} format :P");
		}

		[ContextMenu("Load Game")]
		public void LoadGame()
		{
			if (!File.Exists(SavePath))
			{
				Debug.LogWarning("Save file not found :v save before load");
				return;
			}

			SaveWrapper wrapper = null;

			if (saveType == SaveType.JSON)
			{
				string json = File.ReadAllText(SavePath);
				wrapper = JsonUtility.FromJson<SaveWrapper>(json);
			}
			else if (saveType == SaveType.BINARY)
			{
				using FileStream fs = new(SavePath, FileMode.Open);
				var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				wrapper = (SaveWrapper)bf.Deserialize(fs);
			}

			var existing = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToDictionary(s => s.UniqueID, s => s);

			foreach (var entry in wrapper.objects)
			{
				if (existing.TryGetValue(entry.id, out var existingObj))
					existingObj.LoadData(entry.data.GetObject());
				else if (entry.prefabPath != null)
				{
					GameObject prefab = Resources.Load<GameObject>(entry.prefabPath);
					if (prefab != null)
					{
						Vector3 position = new(entry.position.x, entry.position.y, entry.position.z);
						Quaternion rotation = new(entry.rotation.x, entry.rotation.y, entry.rotation.z, entry.rotation.w);
						GameObject spawned = Instantiate(prefab, position, rotation);

						var saveable = spawned.GetComponent<ISaveable>();
						if (saveable != null)
						{
							saveable.UniqueID = entry.id;
							saveable.LoadData(entry.data.GetObject());
						}
					}
					else
						Debug.LogWarning($"Can't find the prefab to spawn :( are you sure {entry.prefabPath} is inside Resources folder?");
				}
			}

			Debug.Log($"Game loaded successfully from: {SavePath} :)");
		}

		[System.Serializable]
		private class SaveWrapper { public List<SaveObject> objects = new(); }

		private string GetPrefabPath(GameObject prefab) => (prefab == null) ? null : prefab.name; // Assumes prefab is in Resources folder

		[System.Serializable]
		public struct SerializableVector3
		{
			public float x, y, z;

			public SerializableVector3(Vector3 v) { x = v.x; y = v.y; z = v.z; }
			public Vector3 ToVector3() => new(x, y, z);
		}

		[System.Serializable]
		public struct SerializableQuaternion
		{
			public float x, y, z, w;

			public SerializableQuaternion(Quaternion q) { x = q.x; y = q.y; z = q.z; w = q.w; }
			public Quaternion ToQuaternion() => new(x, y, z, w);
		}

		[System.Serializable]
		private class SaveObject
		{
			public string id;
			public string prefabPath;
			public ScriptData data;

			public SerializableVector3 position;
			public SerializableQuaternion rotation;
		}

		[System.Serializable]
		private class ScriptData
		{
			public string json;
			public string type;

			public ScriptData(object obj)
			{
				if (obj == null) return;
				json = JsonUtility.ToJson(obj);
				type = obj.GetType().AssemblyQualifiedName;
			}

			public object GetObject() => JsonUtility.FromJson(json, System.Type.GetType(type));
		}
	}
}
