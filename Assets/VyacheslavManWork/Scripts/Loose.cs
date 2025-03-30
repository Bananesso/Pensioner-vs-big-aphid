using UnityEngine;

public class Loose : MonoBehaviour
{
    Health health;
    [SerializeField] private GameObject _looseMenu;
    [SerializeField] private GameObject _fpCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _fpCamera.SetActive(false);
            _looseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
