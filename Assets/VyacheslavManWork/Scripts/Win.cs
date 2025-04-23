using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    Health health;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _fpCamera;
    [SerializeField] private GameObject Player;


    private void Start()
    {
        _fpCamera = GameObject.Find("Virtual Camera");
        _winMenu = GameObject.FindGameObjectWithTag("Win");
        _winMenu.SetActive(false);
        Player = GameObject.Find("Playerw");
        health = GetComponent<Health>();
        health.OnDie += Winn;
    }


    public void Winn()
    {
        Time.timeScale = 0;
        _fpCamera.SetActive(false);
        _winMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}