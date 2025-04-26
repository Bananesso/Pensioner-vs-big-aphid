using UnityEngine;
using UnityEngine.UI;


public class OpenWebsite : MonoBehaviour
{
    public string url = "https://www.donationalerts.com/r/boomblasta"; 

    void Start()
    {
        
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OpenURL);
        }
    }

    public void OpenURL()
    {
        Application.OpenURL(url);
        Debug.Log("Opening URL: " + url);
    }
    
}