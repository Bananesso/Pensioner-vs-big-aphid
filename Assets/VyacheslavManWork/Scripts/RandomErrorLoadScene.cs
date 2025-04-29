using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomErrorLoadScene : MonoBehaviour
{
    [SerializeField] private string _errorSceneName;
    [SerializeField] private string _normalSceneName;

    [SerializeField] private float _minRandomValue;
    [SerializeField] private float _maxRandomValue;
    [SerializeField] private float _needValue;
    private float _randomValue;
    private void Start()
    {
        _randomValue = Random.Range(-10.0f, 10.0f);
        if (_randomValue == _needValue)
        {
            LoadErrorSceneByName(_errorSceneName);
        }
        else
        {
            LoadErrorSceneByName(_normalSceneName);
        }
    }

    public void LoadErrorSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}