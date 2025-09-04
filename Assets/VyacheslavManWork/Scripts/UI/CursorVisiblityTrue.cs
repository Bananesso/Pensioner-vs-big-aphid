using UnityEngine;

public class CursorVisiblityTrue : MonoBehaviour
{
    [SerializeField] private bool _playOnAwake = true;

    void Start()
    {
        if (_playOnAwake)
            ShowCursor(true);
    }

    public void ShowCursor(bool _activate)
    {
        if (_activate)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = _activate;
    }
}