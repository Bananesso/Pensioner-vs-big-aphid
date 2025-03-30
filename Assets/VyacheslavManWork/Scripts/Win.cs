using UnityEngine;

public class Win : MonoBehaviour
{
    Health health;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _fpCamera;

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnDie += Winn;
    }

    public void Winn()
    {
        _fpCamera.SetActive(false);
        _winMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
