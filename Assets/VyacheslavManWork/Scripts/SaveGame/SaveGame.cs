using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    private int _sceneNumber;

    private void Start()
    {
        _sceneNumber = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedSceneNumber", _sceneNumber);
        PlayerPrefs.Save();
    }
}