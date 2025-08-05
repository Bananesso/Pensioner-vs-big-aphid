using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BaldiPlusCreditsTMPAdvanced : MonoBehaviour
{
    [System.Serializable]
    public class CreditEntry
    {
        [Header("Content")]
        public string text;
        public Sprite image;

        [Header("Timing")]
        public float duration = 3f;
        public float fadeTime = 0.5f;

        [Header("Appearance")]
        public TextAlignmentOptions alignment = TextAlignmentOptions.Center;
        public float imageOffset = -80f; // Смещение изображения относительно текста

        [Header("Movement Settings")]
        public bool enableMovement = true;
        public enum MovementType { Down, Left, Right, None }
        public MovementType movementType;
        public float movementDistance = 150f;
    }

    public List<CreditEntry> mainCredits;
    public List<CreditEntry> leftSideCredits;
    public List<CreditEntry> rightSideCredits;

    [Header("UI References")]
    public TMP_Text creditText;
    public Image creditImage;

    [Header("Settings")]
    public float defaultSpacing = 150f;
    public bool loopCredits = false;

    private RectTransform textRect;
    private RectTransform imageRect;
    private CanvasGroup textGroup;
    private CanvasGroup imageGroup;

    private void Awake()
    {
        textRect = creditText.GetComponent<RectTransform>();
        imageRect = creditImage.GetComponent<RectTransform>();

        textGroup = creditText.GetComponent<CanvasGroup>() ?? creditText.gameObject.AddComponent<CanvasGroup>();
        imageGroup = creditImage.GetComponent<CanvasGroup>() ?? creditImage.gameObject.AddComponent<CanvasGroup>();

        creditText.gameObject.SetActive(false);
        creditImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(RunCredits());
    }

    private IEnumerator RunCredits()
    {
        do
        {
            // Основные разработчики
            foreach (var entry in mainCredits)
            {
                yield return StartCoroutine(ShowCreditEntry(entry));
            }

            // Подписи и фото
            foreach (var entry in leftSideCredits)
            {
                yield return StartCoroutine(ShowCreditEntry(entry));
            }

            // Благодарности
            foreach (var entry in rightSideCredits)
            {
                yield return StartCoroutine(ShowCreditEntry(entry));
            }

            yield return new WaitForSeconds(1f);
        } while (loopCredits);

        gameObject.SetActive(false);
    }

    private IEnumerator ShowCreditEntry(CreditEntry entry)
    {
        // Установка контента
        creditText.gameObject.SetActive(true);
        creditText.text = entry.text;
        creditText.alignment = entry.alignment;

        if (entry.image != null)
        {
            creditImage.gameObject.SetActive(true);
            creditImage.sprite = entry.image;
        }
        else
        {
            creditImage.gameObject.SetActive(false);
        }

        // Начальная и конечная позиции
        Vector2 startPos = Vector2.zero;
        Vector2 endPos = Vector2.zero;

        if (entry.enableMovement)
        {
            switch (entry.movementType)
            {
                case CreditEntry.MovementType.Down:
                    startPos = new Vector2(0, entry.movementDistance);
                    endPos = new Vector2(0, -entry.movementDistance);
                    break;
                case CreditEntry.MovementType.Left:
                    startPos = new Vector2(entry.movementDistance, 0);
                    endPos = new Vector2(-entry.movementDistance, 0);
                    break;
                case CreditEntry.MovementType.Right:
                    startPos = new Vector2(-entry.movementDistance, 0);
                    endPos = new Vector2(entry.movementDistance, 0);
                    break;
                case CreditEntry.MovementType.None:
                    startPos = Vector2.zero;
                    endPos = Vector2.zero;
                    break;
            }
        }
        else
        {
            startPos = Vector2.zero;
            endPos = Vector2.zero;
        }

        textRect.anchoredPosition = startPos;
        if (creditImage.gameObject.activeSelf)
        {
            imageRect.anchoredPosition = startPos + new Vector2(0, entry.imageOffset);
        }

        // Появление (fade in)
        yield return StartCoroutine(Fade(0f, 1f, entry.fadeTime));

        // Движение (если включено)
        if (entry.enableMovement && entry.movementType != CreditEntry.MovementType.None)
        {
            float timer = 0f;
            while (timer < entry.duration)
            {
                timer += Time.deltaTime;
                float progress = timer / entry.duration;
                textRect.anchoredPosition = Vector2.Lerp(startPos, endPos, progress);
                if (creditImage.gameObject.activeSelf)
                {
                    imageRect.anchoredPosition = Vector2.Lerp(
                        startPos + new Vector2(0, entry.imageOffset),
                        endPos + new Vector2(0, entry.imageOffset),
                        progress);
                }
                yield return null;
            }
        }
        else
        {
            // Просто ждём указанное время, если движение отключено
            yield return new WaitForSeconds(entry.duration);
        }

        // Исчезновение (fade out)
        yield return StartCoroutine(Fade(1f, 0f, entry.fadeTime));

        creditText.gameObject.SetActive(false);
        creditImage.gameObject.SetActive(false);
    }

    private IEnumerator Fade(float from, float to, float time)
    {
        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, timer / time);
            textGroup.alpha = alpha;
            imageGroup.alpha = alpha;
            yield return null;
        }
    }
}