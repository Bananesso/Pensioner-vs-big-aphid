using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    private int _sceneNumber;
    private float _atkDamage;
    private float _atkSpeed;
    private float _mooveSpeed;

    private void Start()
    {
        _sceneNumber = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedSceneNumber", _sceneNumber);

        _atkDamage = PlayerPrefs.GetFloat("MultiplierAtkDamage", 1);
        _atkSpeed = PlayerPrefs.GetFloat("MultiplierAtkSpeed", 1);
        _mooveSpeed = PlayerPrefs.GetFloat("MultiplierMooveSpeed", 1);

        PlayerPrefs.Save();
    }

    public void LoadSave()
    {
        SceneManager.LoadScene(_sceneNumber);
        PlayerPrefs.SetFloat("MultiplierAtkDamage", _atkDamage);
        PlayerPrefs.SetFloat("MultiplierAtkSpeed", _atkSpeed);
        PlayerPrefs.SetFloat("MultiplierMooveSpeed", _mooveSpeed);
    }
}