using UnityEngine;

public class Sintezator : MonoBehaviour
{
    [SerializeField] private GameObject _magaz;
    [SerializeField] private GameObject _fpCamera;
    public void OpenMenu()
    {
        _magaz.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _fpCamera.SetActive(false);
    }

    public void CloseMenu()
    {
        _magaz.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _fpCamera.SetActive(true);
    }
}