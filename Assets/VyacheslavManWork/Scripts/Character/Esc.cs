using UnityEngine;

[System.Serializable]
public class Esc
{
    [SerializeField] private GameObject _fpCamera;
    [SerializeField] private GameObject Menu;

    public void EscMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _fpCamera.SetActive(false);
            Menu.SetActive(true);
        }
    }
}