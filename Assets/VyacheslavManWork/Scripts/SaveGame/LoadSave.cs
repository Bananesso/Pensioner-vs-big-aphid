using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSave : MonoBehaviour
{
    private float _atkDamage;
    private float _atkSpeed;
    private float _mooveSpeed;
    private int _sceneNumber;

    private void Start()
    {
        _atkDamage = PlayerPrefs.GetFloat("MultiplierAtkDamage", 1);
        _atkSpeed = PlayerPrefs.GetFloat("MultiplierAtkSpeed", 1);
        _mooveSpeed = PlayerPrefs.GetFloat("MultiplierMooveSpeed", 1);
        _sceneNumber = PlayerPrefs.GetInt("SavedSceneNumber", 0);
    }
    public void LoadGame()
    {
        PlayerPrefs.SetFloat("MultiplierAtkDamage", _atkDamage);
        PlayerPrefs.SetFloat("MultiplierAtkSpeed", _atkSpeed);
        PlayerPrefs.SetFloat("MultiplierMooveSpeed", _mooveSpeed);
        SceneManager.LoadScene(_sceneNumber);
    }
}