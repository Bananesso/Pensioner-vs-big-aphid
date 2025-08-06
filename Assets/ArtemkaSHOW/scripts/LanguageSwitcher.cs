using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour
{
    [Header("Кнопки переключения языка")]
    public Button englishButton;
    public Button russianButton;
    public Button otherLanguageButton; // Добавьте кнопки для других языков по необходимости

    private void Start()
    {
        // Подписываемся на события кнопок
        englishButton.onClick.AddListener(() => ChangeLanguage(0));
        russianButton.onClick.AddListener(() => ChangeLanguage(1));
        // otherLanguageButton.onClick.AddListener(() => ChangeLanguage(2));

        // Обновляем состояние кнопок при старте
        UpdateButtonStates();
    }

    private void ChangeLanguage(int languageIndex)
    {
        // Устанавливаем выбранный язык
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];

        // Обновляем состояние кнопок
        UpdateButtonStates();

        // Можно сохранить выбор языка в PlayerPrefs
        PlayerPrefs.SetInt("SelectedLanguage", languageIndex);
        PlayerPrefs.Save();
    }

    private void UpdateButtonStates()
    {
        // Получаем текущий выбранный язык
        var currentLocale = LocalizationSettings.SelectedLocale;

        // Делаем кнопки неактивными для текущего выбранного языка
        englishButton.interactable = currentLocale != LocalizationSettings.AvailableLocales.Locales[0];
        russianButton.interactable = currentLocale != LocalizationSettings.AvailableLocales.Locales[1];
        // otherLanguageButton.interactable = currentLocale != LocalizationSettings.AvailableLocales.Locales[2];
    }

    private void OnDestroy()
    {
        // Отписываемся от событий при уничтожении объекта
        englishButton.onClick.RemoveAllListeners();
        russianButton.onClick.RemoveAllListeners();
        // otherLanguageButton.onClick.RemoveAllListeners();
    }
}