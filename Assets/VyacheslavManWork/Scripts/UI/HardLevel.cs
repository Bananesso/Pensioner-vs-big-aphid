using UnityEngine;

public class HardLevel : MonoBehaviour
{
    [SerializeField] private float MultiplierAtkDamage;
    [SerializeField] private float MultiplierAtkSpeed;
    [SerializeField] private float MultiplierMooveSpeed;
    void ChangeHardLevel()
    {
        PlayerPrefs.SetFloat("MultiplierAtkDamage", MultiplierAtkDamage);
        PlayerPrefs.SetFloat("MultiplierAtkSpeed", MultiplierAtkSpeed);
        PlayerPrefs.SetFloat("MultiplierMooveSpeed", MultiplierMooveSpeed);
    }
}