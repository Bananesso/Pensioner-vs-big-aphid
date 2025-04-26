using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject uiObject; // Перетащите сюда ваш UI-объект из Inspector

    void Start()
    {
        // Проверяем, что объект не null (на всякий случай)
        if (uiObject != null)
        {
            uiObject.SetActive(true); // Включаем UI-объект
        }
        else
        {
            Debug.LogError("UI Object не назначен в инспекторе!");
        }
    }
}