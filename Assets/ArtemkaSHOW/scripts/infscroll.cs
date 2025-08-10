using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollingUIBackground : MonoBehaviour
{
    public float scrollSpeed = 50f;
    public RawImage[] backgroundImages; // Два изображения для бесшовного скролла

    private float imageHeight;

    void Start()
    {
        imageHeight = backgroundImages[0].rectTransform.rect.height;

        // Второе изображение размещаем выше первого
        backgroundImages[1].rectTransform.anchoredPosition = new Vector2(
            backgroundImages[0].rectTransform.anchoredPosition.x,
            backgroundImages[0].rectTransform.anchoredPosition.y + imageHeight
        );
    }

    void Update()
    {
        foreach (var img in backgroundImages)
        {
            // Двигаем вниз
            img.rectTransform.anchoredPosition -= Vector2.up * scrollSpeed * Time.deltaTime;

            // Если изображение ушло за экран, перемещаем его вверх
            if (img.rectTransform.anchoredPosition.y <= -imageHeight)
            {
                img.rectTransform.anchoredPosition = new Vector2(
                    img.rectTransform.anchoredPosition.x,
                    img.rectTransform.anchoredPosition.y + 2 * imageHeight
                );
            }
        }
    }
}