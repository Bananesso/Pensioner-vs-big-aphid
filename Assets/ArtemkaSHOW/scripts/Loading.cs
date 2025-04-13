using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    [Header("Основные настройки")]
    public float loadingTime = 5f; // Время загрузки в секундах
    public string sceneToLoad = "MainScene"; // Название сцены для загрузки

    [Header("Аудио")]
    public AudioClip loadingMusic; // Музыка во время загрузки
    [Range(0f, 1f)] public float musicVolume = 0.5f;

    [Header("UI элементы")]
    public TMP_Text loadingText; // Текст загрузки (TMP)
    public TMP_Text progressText; // Текст прогресса (TMP)
    public Image progressBar; // Полоса загрузки
    public Image animatedImage; // Гиф-изображение (можно использовать AnimatedImage)
    public Button continueButton; // Кнопка продолжения
    public AudioClip buttonClickSound; // Звук кнопки

    private AudioSource audioSource;
    private bool isLoadingComplete = false;
    private float currentLoadTime = 0f;

    void Start()
    {
        // Настройка AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = musicVolume;

        // Начальная настройка UI
        continueButton.gameObject.SetActive(false);
        progressBar.fillAmount = 0f;

        // Запуск музыки
        if (loadingMusic != null)
        {
            audioSource.clip = loadingMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        // Настройка кнопки
        continueButton.onClick.AddListener(OnContinueButtonClick);

        // Запуск процесса загрузки
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;

        // Имитация загрузки
        while (currentLoadTime < loadingTime || asyncLoad.progress < 0.9f)
        {
            currentLoadTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentLoadTime / loadingTime);

            // Обновление UI
            progressBar.fillAmount = progress;
            progressText.text = $"{(int)(progress * 100)}%";

            // Анимация текста (точки)
            string dots = new string('.', (int)(Time.time % 4));
            loadingText.text = $"Загрузка{dots}";

            yield return null;
        }

        // Загрузка завершена
        isLoadingComplete = true;
        loadingText.text = "Загрузка завершена!";
        progressText.text = "100%";
        progressBar.fillAmount = 1f;

        // Активируем кнопку продолжения
        continueButton.gameObject.SetActive(true);
    }

    void OnContinueButtonClick()
    {
        // Воспроизводим звук кнопки
        if (buttonClickSound != null)
        {
            AudioSource.PlayClipAtPoint(buttonClickSound, Camera.main.transform.position);
        }

        // Переходим к загруженной сцене
        SceneManager.LoadScene(sceneToLoad);
    }

    void Update()
    {
        // Анимация гифки (если не используется AnimatedImage)
        if (animatedImage != null)
        {
            // Простейшая анимация - вращение
            animatedImage.transform.Rotate(0f, 0f, 30f * Time.deltaTime);
        }
    }
}