using Cinemachine;
using UnityEngine;

public class Sintezator : MonoBehaviour, IInteractWithObj
{
    [SerializeField] private GameObject _magaz;
    [SerializeField] private CinemachinePOV _fpCamera;
    [SerializeField] private PlayerController _player;

    private float _verticalAxisVal;
    private float _horisontalAxisVal;

    public void Interact()
    {
		_verticalAxisVal = _fpCamera.m_VerticalAxis.Value;
        _horisontalAxisVal = _fpCamera.m_HorizontalAxis.Value;

		_magaz.SetActive(true);
        _player.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _fpCamera.gameObject.SetActive(false);
    }

    public void CloseMenu()
    {
        _fpCamera.m_VerticalAxis.Value = _verticalAxisVal;
        _fpCamera.m_HorizontalAxis.Value = _horisontalAxisVal;

		_magaz.SetActive(false);
        _player.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _fpCamera.gameObject.SetActive(true);
    }
}