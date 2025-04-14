using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimplePopupAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private RectTransform popupPanel;
    [SerializeField] private Image backgroundOverlay;

    [Header("Settings")]
    [SerializeField] private float animationDuration = 0.3f;
    [SerializeField] [Range(0, 1)] private float overlayMaxAlpha = 0.7f;
    [SerializeField] private AnimationCurve showCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve hideCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private Vector2 hiddenPosition;
    private Coroutine currentAnimation;
    private bool isVisible = false;

    private void Awake()
    {
        // Сохраняем начальную позицию (должна быть за пределами экрана внизу)
        hiddenPosition = new Vector2(0, -popupPanel.rect.height);
        popupPanel.anchoredPosition = hiddenPosition;

        // Настраиваем оверлей
        backgroundOverlay.color = new Color(0, 0, 0, 0);
        backgroundOverlay.gameObject.SetActive(false);

        // Подписываем кнопки
        openButton.onClick.AddListener(Show);
        closeButton.onClick.AddListener(Hide);
    }

    public void Show()
    {
        if (isVisible) return;

        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        currentAnimation = StartCoroutine(AnimatePopup(true));
    }

    public void Hide()
    {
        if (!isVisible) return;

        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        currentAnimation = StartCoroutine(AnimatePopup(false));
    }

    private IEnumerator AnimatePopup(bool show)
    {
        isVisible = show;
        backgroundOverlay.gameObject.SetActive(true);

        Vector2 startPos = popupPanel.anchoredPosition;
        Vector2 targetPos = show ? Vector2.zero : hiddenPosition;

        Color startColor = backgroundOverlay.color;
        Color targetColor = show ?
            new Color(0, 0, 0, overlayMaxAlpha) :
            new Color(0, 0, 0, 0);

        float elapsed = 0f;
        AnimationCurve curve = show ? showCurve : hideCurve;

        while (elapsed < animationDuration)
        {
            float t = curve.Evaluate(elapsed / animationDuration);

            popupPanel.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            backgroundOverlay.color = Color.Lerp(startColor, targetColor, t);

            elapsed += Time.unscaledDeltaTime; // Используем unscaledDeltaTime для работы при паузе
            yield return null;
        }

        popupPanel.anchoredPosition = targetPos;
        backgroundOverlay.color = targetColor;

        if (!show)
            backgroundOverlay.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        openButton.onClick.RemoveListener(Show);
        closeButton.onClick.RemoveListener(Hide);
    }
}