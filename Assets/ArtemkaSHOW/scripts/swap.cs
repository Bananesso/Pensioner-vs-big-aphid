using UnityEngine;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour
{
    public Image buttonImage; // Компонент Image кнопки
    public Sprite normalTexture; // Обычная текстура
    public Sprite hoverTexture; // Текстура при наведении

    void Start()
    {
        buttonImage.sprite = normalTexture;
    }

    public void OnPointerEnter()
    {
        buttonImage.sprite = hoverTexture; // Меняем текстуру при наведении
    }

    public void OnPointerExit()
    {
        buttonImage.sprite = normalTexture; // Возвращаем обратно
    }
}