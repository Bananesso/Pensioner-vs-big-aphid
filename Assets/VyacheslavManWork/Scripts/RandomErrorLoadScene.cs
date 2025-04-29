using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomErrorLoadScene : MonoBehaviour
{
    [SerializeField] private string _errorSceneName;
    [SerializeField] private string _normalSceneName;

    [SerializeField] private int _minRandomValue;
    [SerializeField] private int _maxRandomValue;
    [SerializeField] private int _needValue;
    private float _randomValue;
    private void Start()
    {
        _randomValue = Random.Range(_minRandomValue, _maxRandomValue);
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