using UnityEngine;

public class HardLevel : MonoBehaviour
{
    [SerializeField] private float MultiplierAtkDamage = 1;
    [SerializeField] private float MultiplierAtkSpeed = 1;
    [SerializeField] private float MultiplierMooveSpeed = 1;
    void ChangeHardLevel()
    {
        PlayerPrefs.SetFloat("MultiplierAtkDamage", MultiplierAtkDamage);
        PlayerPrefs.SetFloat("MultiplierAtkSpeed", MultiplierAtkSpeed);
        PlayerPrefs.SetFloat("MultiplierMooveSpeed", MultiplierMooveSpeed);
    }
}