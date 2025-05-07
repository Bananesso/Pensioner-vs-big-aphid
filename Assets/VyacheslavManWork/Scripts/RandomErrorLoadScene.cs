using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomErrorLoadScene : MonoBehaviour
{
    [SerializeField] private string _errorSceneName;
    [SerializeField] private string _normalSceneName;

    [SerializeField] private int _precentToLoadErr;
    private int _randomValue;

    private void Start()
    {
        _randomValue = Random.Range(0, 100);
    }

    public void LoadErrorSceneByName(string sceneName)
    {
        if (_randomValue < _precentToLoadErr)
        {
            SceneManager.LoadScene(_errorSceneName);
        }
        else
        {
            SceneManager.LoadScene(_normalSceneName);
        }
    }
}