using UnityEngine;

public class HardLevelManager : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetFloat("MultiplierAtkDamage", 1);
        PlayerPrefs.SetFloat("MultiplierAtkSpeed", 1);
        PlayerPrefs.SetFloat("MultiplierMooveSpeed", 1);
    }
}