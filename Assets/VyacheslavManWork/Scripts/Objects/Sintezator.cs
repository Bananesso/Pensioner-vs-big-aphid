using UnityEngine;

public class Sintezator : MonoBehaviour, IInteractWithObj
{
    [SerializeField] private GameObject _magaz;
    [SerializeField] private GameObject _fpCamera;
    [SerializeField] private PlayerController _player;
    public void Interact()
    {
        _magaz.SetActive(true);
        _player.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _fpCamera.SetActive(false);
    }

    public void CloseMenu()
    {
        _magaz.SetActive(false);
        _player.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _fpCamera.SetActive(true);
    }
}