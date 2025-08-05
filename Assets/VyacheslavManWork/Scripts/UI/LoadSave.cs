using UnityEngine;

public class LoadSave : MonoBehaviour
{
    SaveGame saveGame;

    public void LoadGame()
    {
        saveGame.LoadSave();
    }
}