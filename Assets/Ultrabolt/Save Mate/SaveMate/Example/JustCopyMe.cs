//Ready to use babe B) just copy this to your code.
using UnityEngine;
using SaveMate;

namespace SaveMate
{
	public class JustCopyMe : MonoBehaviour, ISaveable
	{
		#region This required to use on any saveable object.
		[Header("Resources")]

		[SerializeField, Tooltip("Prefab Name from Resources folder, Keep it null if you won't this object to respawn again when you load the game.")]
		private string prefabName;
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
				//Your data to save.
			};
		}

		public void LoadData(object data)
		{
			if (data == null) return;

			if (data is DataToSave d)
			{
				//Your data to load.
			}
		}

		[System.Serializable]
		public class DataToSave
		{
			//Your specific saveable data.
		}
		#endregion
	}
}
