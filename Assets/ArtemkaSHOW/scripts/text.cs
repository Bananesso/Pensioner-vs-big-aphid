using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedTextFade : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private bool disableAfterFade = true;

    [Header("Additional Effects")]
    [SerializeField] private bool useScaleEffect = true;
    [SerializeField] private Vector3 targetScale = new Vector3(0.8f, 0.8f, 1f);
    [SerializeField] private bool useMoveEffect = false;
    [SerializeField] private Vector2 moveOffset = new Vector2(0, -50f);

    private Text textComponent;
    private Color originalColor;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
        originalColor = textComponent.color;
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    public void StartFadeOut()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(PerformFadeOut());
    }

    private IEnumerator PerformFadeOut()
    {
        float elapsedTime = 0f;
        Color startColor = textComponent.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        Vector3 startScale = transform.localScale;
        Vector3 endScale = useScaleEffect ? targetScale : startScale;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = useMoveEffect ? startPos + (Vector3)moveOffset : startPos;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            // Плавное изменение прозрачности
            textComponent.color = Color.Lerp(startColor, targetColor, t);

            // Дополнительные эффекты
            if (useScaleEffect)
            {
                transform.localScale = Vector3.Lerp(startScale, endScale, t);
            }

            if (useMoveEffect)
            {
                transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            }

            yield return null;
        }

        // Гарантируем, что конечные значения установлены точно
        textComponent.color = targetColor;
        if (useScaleEffect) transform.localScale = endScale;
        if (useMoveEffect) transform.localPosition = endPos;

        if (disableAfterFade)
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetState()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        textComponent.color = originalColor;
        transform.localScale = originalScale;
        transform.localPosition = originalPosition;
        gameObject.SetActive(true);
    }
}