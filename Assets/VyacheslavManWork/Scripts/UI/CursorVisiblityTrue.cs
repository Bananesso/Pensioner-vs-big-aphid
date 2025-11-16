using UnityEngine;

public class CursorVisiblityTrue : MonoBehaviour
{
    [SerializeField] private bool _playOnAwake = true;
    [SerializeField] private bool _activate = true;

    void Start()
    {
        if (_playOnAwake)
            ShowCursor(_activate);
    }

    public void ShowCursor(bool activate)
    {
        if (activate)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = activate;
    }
}