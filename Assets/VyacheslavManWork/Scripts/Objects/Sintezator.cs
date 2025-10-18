using UnityEngine;
using Cinemachine;

public class Sintezator : MonoBehaviour, IInteractWithObj
{
    [SerializeField] private GameObject _magaz;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private PlayerController _player;

    private float _verticalAxisVal;
    private float _horisontalAxisVal;
    private CinemachinePOV _povComponent;

    private void Start()
    {
        _povComponent = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    public void Interact()
    {
        _verticalAxisVal = _povComponent.m_VerticalAxis.Value;
        _horisontalAxisVal = _povComponent.m_HorizontalAxis.Value;

        _magaz.SetActive(true);
        _player.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _virtualCamera.gameObject.SetActive(false);
    }

    public void CloseMenu()
    {
        _povComponent.m_VerticalAxis.Value = _verticalAxisVal;
        _povComponent.m_HorizontalAxis.Value = _horisontalAxisVal;

        _magaz.SetActive(false);
        _player.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _virtualCamera.gameObject.SetActive(true);
    }
}