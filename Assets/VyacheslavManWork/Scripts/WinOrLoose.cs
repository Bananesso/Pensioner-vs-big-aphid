using UnityEngine;

public class WinOrLoose : MonoBehaviour
{
    [SerializeField] private GameObject _looseMenu;
    [SerializeField] private GameObject _winMenu;
    public void Loose()
    {
        _looseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Win()
    {
        _winMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
