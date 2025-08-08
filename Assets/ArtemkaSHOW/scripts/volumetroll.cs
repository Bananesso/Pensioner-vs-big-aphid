using UnityEngine;
using UnityEngine.UI;

public class IntensityController : MonoBehaviour
{
    [Header("References")]
    public Slider intensitySlider; // Ссылка на UI Slider
    public Camera mainCamera;     // Основная камера
    public Image redOverlay;     // Полупрозрачный красный overlay (UI Image)
    public RectTransform uiPanelToShake; // UI элемент для тряски (опционально)

    [Header("Settings")]
    public float maxShakeIntensity = 1f; // Максимальная сила тряски
    public float maxRedIntensity = 0.7f; // Максимальная прозрачность красного overlay (0-1)
    public float shakeSpeed = 20f;       // Скорость тряски

    private Vector3 originalCamPosition;
    private Vector3 originalUIPosition;
    private float currentShakeIntensity = 0f;

    private void Start()
    {
        // Сохраняем исходные позиции
        if (mainCamera != null)
            originalCamPosition = mainCamera.transform.localPosition;

        if (uiPanelToShake != null)
            originalUIPosition = uiPanelToShake.localPosition;

        // Настраиваем начальное состояние
        if (redOverlay != null)
            redOverlay.color = new Color(1, 0, 0, 0);
    }

    private void Update()
    {
        if (intensitySlider == null) return;

        // Получаем текущее значение слайдера (0-1)
        float sliderValue = intensitySlider.value;

        // Устанавливаем прозрачность красного overlay
        if (redOverlay != null)
        {
            float redAlpha = sliderValue * maxRedIntensity;
            redOverlay.color = new Color(1, 0, 0, redAlpha);
        }

        // Устанавливаем интенсивность тряски
        currentShakeIntensity = sliderValue * maxShakeIntensity;

        // Применяем тряску к камере
        if (mainCamera != null)
        {
            Vector3 shakeOffset = new Vector3(
                Mathf.PerlinNoise(0, Time.time * shakeSpeed) * 2 - 1,
                Mathf.PerlinNoise(1, Time.time * shakeSpeed) * 2 - 1,
                0) * currentShakeIntensity;

            mainCamera.transform.localPosition = originalCamPosition + shakeOffset;
        }

        // Применяем тряску к UI (если задан)
        if (uiPanelToShake != null)
        {
            Vector3 uiShakeOffset = new Vector3(
                Mathf.PerlinNoise(2, Time.time * shakeSpeed) * 2 - 1,
                Mathf.PerlinNoise(3, Time.time * shakeSpeed) * 2 - 1,
                0) * currentShakeIntensity * 10f; // Умножаем на 10 для более заметного эффекта в UI

            uiPanelToShake.localPosition = originalUIPosition + uiShakeOffset;
        }
    }

    // Сброс позиций при отключении
    private void OnDisable()
    {
        if (mainCamera != null)
            mainCamera.transform.localPosition = originalCamPosition;

        if (uiPanelToShake != null)
            uiPanelToShake.localPosition = originalUIPosition;
    }
}