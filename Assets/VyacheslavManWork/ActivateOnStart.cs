using UnityEngine;

public class ActivateOnStart : MonoBehaviour
{
    [SerializeField] private GameObject _activateThis;

    void Start()
    {
        _activateThis.SetActive(true);
    }
}