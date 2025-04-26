using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro; // Добавляем пространство имен для TextMeshPro

public class LogoAndRandomText : MonoBehaviour
{
    [Header("Настройки логотипа")]
    [Tooltip("Изображение логотипа (UI Image)")]
    public Image logoImage;

    [Tooltip("Время показа логотипа в секундах")]
    public float logoDisplayTime = 2f;

    [Tooltip("Длительность плавного появления/исчезновения")]
    public float fadeDuration = 1f;

    [Header("Настройки текста")]
    [Tooltip("Текстовый элемент (поддерживает Text и TextMeshPro)")]
    public MonoBehaviour textComponent; // Базовый тип для обоих вариантов

    [Tooltip("Список возможных текстовых сообщений")]
    [TextArea(2, 4)]
    public List<string> textOptions = new List<string>()
    {
        "Добро пожаловать!",
        "Привет, игрок!",
        "Наслаждайтесь игрой!"
    };

    [Tooltip("Время показа текста в секундах")]
    public float textDisplayTime = 3f;

    // Для кэширования компонентов
    private Text legacyText;
    private TMP_Text textMeshPro;
    private Graphic textGraphic; // Общий интерфейс для работы с прозрачностью

    private void Start()
    {
        // Проверяем и находим текстовый компонент
        InitializeTextComponent();

        // Проверяем необходимые компоненты
        if (logoImage == null)
            Debug.LogError("Не назначено изображение логотипа!");

        if (textComponent == null)
            Debug.LogError("Не назначен текстовый элемент!");

        // Начинаем последовательность
        StartCoroutine(ShowLogoAndText());
    }

    private void InitializeTextComponent()
    {
        if (textComponent == null) return;

        // Пытаемся определить тип текстового компонента
        if (textComponent is Text)
        {
            legacyText = (Text)textComponent;
            textGraphic = legacyText;
        }
        else if (textComponent is TMP_Text)
        {
            textMeshPro = (TMP_Text)textComponent;
            textGraphic = textMeshPro;
        }
        else
        {
            Debug.LogError("Назначенный текстовый компонент не является ни Text, ни TextMeshPro!");
        }
    }

    private IEnumerator ShowLogoAndText()
    {
        // Сначала показываем логотип, если он есть
        if (logoImage != null)
        {
            // Настраиваем начальное состояние
            logoImage.gameObject.SetActive(true);
            SetAlpha(logoImage, 0f);

            if (textGraphic != null)
                textGraphic.gameObject.SetActive(false);

            // Плавное появление
            yield return Fade(logoImage, 0f, 1f, fadeDuration);

            // Ждем указанное время
            yield return new WaitForSeconds(logoDisplayTime);

            // Плавное исчезновение
            yield return Fade(logoImage, 1f, 0f, fadeDuration);

            logoImage.gameObject.SetActive(false);
        }

        // Затем показываем текст, если он есть и есть варианты текста
        if (textGraphic != null && textOptions.Count > 0)
        {
            textGraphic.gameObject.SetActive(true);
            SetAlpha(textGraphic, 0f);

            // Устанавливаем текст в зависимости от типа компонента
            string randomText = textOptions[Random.Range(0, textOptions.Count)];
            if (legacyText != null)
                legacyText.text = randomText;
            else if (textMeshPro != null)
                textMeshPro.text = randomText;

            // Плавное появление текста
            yield return Fade(textGraphic, 0f, 1f, fadeDuration);

            // Ждем указанное время
            yield return new WaitForSeconds(textDisplayTime);

            // Плавное исчезновение текста
            yield return Fade(textGraphic, 1f, 0f, fadeDuration);

            textGraphic.gameObject.SetActive(false);
        }
    }

    // Универсальная функция для плавного изменения прозрачности
    private IEnumerator Fade(Graphic graphic, float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            SetAlpha(graphic, alpha);
            yield return null;
        }
        SetAlpha(graphic, to);
    }

    // Устанавливает прозрачность для любого UI элемента
    private void SetAlpha(Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }
}