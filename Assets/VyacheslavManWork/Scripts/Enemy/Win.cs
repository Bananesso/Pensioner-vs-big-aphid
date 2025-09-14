using UnityEngine;

public class Win : MonoBehaviour
{
    Health health;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _fpCamera;
    [SerializeField] private GameObject _player;

    private void Start()
    {
        //_fpCamera = GameObject.FindGameObjectWithTag("Camera");
        //_winMenu = GameObject.FindGameObjectWithTag("Win");
        //_player = GameObject.FindGameObjectWithTag("Player");
        //_winMenu.SetActive(false);
        health = GetComponent<Health>();
        health.OnDie += Winn;
    }


    public void Winn()
    {
        Time.timeScale = 0;
        _fpCamera.SetActive(false);
        _winMenu.SetActive(true);
        _player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}