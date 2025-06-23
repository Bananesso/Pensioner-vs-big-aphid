using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SpaceHoldToLoadScene : MonoBehaviour
{
    [Header("Settings")]
    public string targetSceneName;  // Сцена для загрузки
    public float holdDuration = 3f; // Время удержания (сек)
    public float resetSpeed = 2f;  // Скорость сброса прогресса
    public float autoStartDelay = 10f; // Через сколько секунд автостарт

    [Header("UI References")]
    public Image progressCircle;    // Круг заполнения (Fill Radial 360)
    public TMP_Text countdownText;  // Текст с таймером (TextMeshPro)
    public TMP_Text promptText;    // Текст подсказки (опционально)

    private float currentHoldTime = 0f;
    private float idleTime = 0f;
    private bool isHolding = false;
    private bool isResetting = false;
    private bool autoStarted = false;
    private Coroutine autoStartCoroutine;

    void Start()
    {
        if (promptText != null)
        {
            promptText.text = "Hold SPACE to continue";
        }

        // Запускаем отсчет для автостарта
        autoStartCoroutine = StartCoroutine(AutoStartCountdown());
    }

    void Update()
    {
        // Нажатие пробела - начинаем удержание
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartHold();
            // Отменяем автостарт если был активен
            if (autoStartCoroutine != null)
            {
                StopCoroutine(autoStartCoroutine);
                autoStarted = false;
            }
        }

        // Отпускание пробела - плавный сброс
        if (Input.GetKeyUp(KeyCode.Space) && !isResetting && !autoStarted)
        {
            StartCoroutine(SmoothReset());
        }

        // Обновление прогресса при удержании
        if (isHolding)
        {
            currentHoldTime += Time.deltaTime;
            UpdateUI();

            if (currentHoldTime >= holdDuration)
            {
                LoadTargetScene();
            }
        }
    }

    private IEnumerator AutoStartCountdown()
    {
        yield return new WaitForSeconds(autoStartDelay);

        if (!isHolding && !autoStarted)
        {
            autoStarted = true;
            if (promptText != null)
            {
                promptText.text = "Starting automatically...";
            }
            StartHold();
        }
    }

    private void StartHold()
    {
        StopAllCoroutines(); // Отменяем сброс, если был
        isResetting = false;
        isHolding = true;

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            countdownText.alpha = 1f;
        }
    }

    private IEnumerator SmoothReset()
    {
        isHolding = false;
        isResetting = true;

        float startFill = progressCircle.fillAmount;
        float startAlpha = (countdownText != null) ? countdownText.alpha : 1f;
        float resetProgress = 0f;

        while (resetProgress < 1f)
        {
            resetProgress += Time.deltaTime * resetSpeed;

            progressCircle.fillAmount = Mathf.Lerp(startFill, 0f, resetProgress);

            if (countdownText != null)
            {
                countdownText.alpha = Mathf.Lerp(startAlpha, 0f, resetProgress);
            }

            yield return null;
        }

        progressCircle.fillAmount = 0f;
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
            countdownText.alpha = 1f;
        }

        currentHoldTime = 0f;
        isResetting = false;

        // Перезапускаем отсчет автостарта
        if (!autoStarted)
        {
            autoStartCoroutine = StartCoroutine(AutoStartCountdown());
        }
    }

    private void UpdateUI()
    {
        float fillAmount = currentHoldTime / holdDuration;
        progressCircle.fillAmount = fillAmount;

        if (countdownText != null)
        {
            float remainingTime = holdDuration - currentHoldTime;
            countdownText.text = Mathf.Ceil(remainingTime).ToString("0");
        }
    }

    private void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}