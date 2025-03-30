using UnityEngine;

public class Win : MonoBehaviour
{
    Health health;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _fpCamera;
    [SerializeField] private GameObject _playerController;

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnDie += Winn;
    }

    public void Winn()
    {
        _playerController.SetActive(false);
        _fpCamera.SetActive(false);
        _winMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
