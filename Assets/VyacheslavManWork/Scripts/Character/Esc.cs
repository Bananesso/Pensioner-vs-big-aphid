using UnityEngine;

[System.Serializable]
public class Esc
{
    [SerializeField] private GameObject _fpCamera;
    [SerializeField] private GameObject Menu;

    public void EscMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0;
            _fpCamera.SetActive(false);
            Menu.SetActive(true);
        }
    }
}