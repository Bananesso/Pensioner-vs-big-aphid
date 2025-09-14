using UnityEngine;

public class HardLevel : MonoBehaviour
{
    [SerializeField] private float _multiplierAtkDamage = 1;
    [SerializeField] private float _multiplierAtkSpeed = 1;
    [SerializeField] private float _multiplierMooveSpeed = 1;

    public void ChangeHardLevel()
    {
        PlayerPrefs.SetFloat("MultiplierAtkDamage", _multiplierAtkDamage);
        PlayerPrefs.SetFloat("MultiplierAtkSpeed", _multiplierAtkSpeed);
        PlayerPrefs.SetFloat("MultiplierMooveSpeed", _multiplierMooveSpeed);

        PlayerPrefs.Save();
    }
}