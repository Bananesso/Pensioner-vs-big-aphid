using UnityEngine;

public class Esc : MonoBehaviour
{
    [SerializeField] private GameObject _fpCamera;
    [SerializeField] private GameObject _playerController;
    [SerializeField] private GameObject Menu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _fpCamera.SetActive(false);
            Menu.SetActive(true);
        }
    }
}