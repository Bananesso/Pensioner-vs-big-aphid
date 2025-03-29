using UnityEngine;

public class Esc : MonoBehaviour
{
    public GameObject Menu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.SetActive(true);
        }
    }
}
