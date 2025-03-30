using UnityEngine;

public class Loose : MonoBehaviour
{
    Health health;
    [SerializeField] private GameObject _looseMenu;
    [SerializeField] private GameObject _fpCamera;
    [SerializeField] private GameObject _playerController;

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnDie += Loosee;
    }

    public void Loosee()
    {
        _playerController.SetActive(false);
        _fpCamera.SetActive(false);
        _looseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
