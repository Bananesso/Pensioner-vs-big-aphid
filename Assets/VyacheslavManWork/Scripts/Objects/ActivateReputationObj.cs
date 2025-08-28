using UnityEngine;
using System.Collections.Generic;

public class ActivateReputationObj : MonoBehaviour
{
    [SerializeField] private List<GameObject> _reputationObject;
    //[SerializeField] private List<bool> _enabled; конкретно в нашей игры необходимости в этой функции нет, но может пригодиться в будущих проектах
    [SerializeField] private List<int> _reputationValue;
    [SerializeField] private List<string> _reputationKey;

    private void Start()
    {
        //опять же, эти переменные не несут практической ценности, но без них читать код будет невыносимо 
        int reputation;
        string key;

        for (int num = 0; num < _reputationObject.Count; num++)
        {
            key = _reputationKey[num];
            reputation = PlayerPrefs.GetInt(key);

            if (PlayerPrefs.GetInt(_reputationKey[num], 0) == _reputationValue[num])
            {
                _reputationObject[num].SetActive(true);
            }
        }
    }
}