using UnityEngine;

public class Loose : MonoBehaviour
{
    [SerializeField] private GameObject _looseMenu;
    [SerializeField] private GameObject _fpCamera;
    [SerializeField] private GameObject _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Time.timeScale = 0;
            _fpCamera.SetActive(false);

            _player = GameObject.Find("Playerw");
            _player.SetActive(false);
            _looseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
