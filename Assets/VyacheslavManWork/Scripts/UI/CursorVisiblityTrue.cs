using UnityEngine;

public class CursorVisiblityTrue : MonoBehaviour
{
    [SerializeField] private bool _playOnAwake = true;

    void Start()
    {
        if (_playOnAwake)
            ShowCursor();
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}