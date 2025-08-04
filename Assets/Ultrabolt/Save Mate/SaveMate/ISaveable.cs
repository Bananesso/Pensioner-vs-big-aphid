namespace SaveMate
{
	public interface ISaveable
	{
		object SaveData();             // Return serializable data.
		void LoadData(object data);    // Apply saved data.

		string PrefabName { get; } // Prefab to use if spawning.
		string UniqueID { get; set; } // Unique ID used for saving/loading :P
	}
}