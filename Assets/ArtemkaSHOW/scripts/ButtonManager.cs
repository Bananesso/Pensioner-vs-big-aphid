using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public class ButtonSettings
{
    public Button button;
    public string normalText = "Button";
    public string hoverText = "Hover";
    public Color normalColor = Color.white;
    public Color hoverColor = new Color(0.7f, 0.7f, 0.7f);
    [Range(0.1f, 2f)] public float fadeDuration = 0.3f;
    [Range(0f, 5f)] public float appearDelay = 1f; // ???????? ?????????
}

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private List<ButtonSettings> buttons = new List<ButtonSettings>();

    private TMP_Text[] buttonTextsTMP;
    private Text[] buttonTextsLegacy;
    private Color[] targetColors;
    private Color[] startColors;
    private float[] fadeProgress;
    private bool[] isHovering;
    private bool[] usesTMP;
    private CanvasGroup[] buttonCanvasGroups;

    private void Awake()
    {
        InitializeButtons();
        StartCoroutine(ShowButtonsSequentially());
    }

    private void InitializeButtons()
    {
        buttonTextsTMP = new TMP_Text[buttons.Count];
        buttonTextsLegacy = new Text[buttons.Count];
        targetColors = new Color[buttons.Count];
        startColors = new Color[buttons.Count];
        fadeProgress = new float[buttons.Count];
        isHovering = new bool[buttons.Count];
        usesTMP = new bool[buttons.Count];
        buttonCanvasGroups = new CanvasGroup[buttons.Count];

        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;

            // ????????????? CanvasGroup ??? ???????? ?????????
            buttonCanvasGroups[i] = buttons[i].button.gameObject.AddComponent<CanvasGroup>();
            buttonCanvasGroups[i].alpha = 0f;
            buttonCanvasGroups[i].interactable = false;
            buttonCanvasGroups[i].blocksRaycasts = false;

            // ????????? ??? ???? ????????? ???????????
            buttonTextsTMP[i] = buttons[i].button.GetComponentInChildren<TMP_Text>();
            buttonTextsLegacy[i] = buttons[i].button.GetComponentInChildren<Text>();
            usesTMP[i] = buttonTextsTMP[i] != null;

            if (buttonTextsTMP[i] == null && buttonTextsLegacy[i] == null)
            {
                Debug.LogError($"Button {buttons[i].button.name} has no Text component!");
                continue;
            }

            // ????????????? ????????? ?????
            SetButtonText(i, buttons[i].normalText);
            SetButtonColor(i, buttons[i].normalColor);

            // ??????????? ???????
            EventTrigger trigger = buttons[i].button.gameObject.AddComponent<EventTrigger>();

            // Pointer Enter
            var pointerEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            pointerEnter.callback.AddListener((e) => OnPointerEnterButton(index));
            trigger.triggers.Add(pointerEnter);

            // Pointer Exit
            var pointerExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
            pointerExit.callback.AddListener((e) => OnPointerExitButton(index));
            trigger.triggers.Add(pointerExit);

            // Click
            buttons[i].button.onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private IEnumerator ShowButtonsSequentially()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            yield return new WaitForSeconds(buttons[i].appearDelay);

            // ??????? ????????? ??????
            float elapsed = 0f;
            while (elapsed < buttons[i].fadeDuration)
            {
                elapsed += Time.deltaTime;
                buttonCanvasGroups[i].alpha = Mathf.Clamp01(elapsed / buttons[i].fadeDuration);
                yield return null;
            }

            buttonCanvasGroups[i].alpha = 1f;
            buttonCanvasGroups[i].interactable = true;
            buttonCanvasGroups[i].blocksRaycasts = true;

            // ????????????? ???????? ?????? ??????
            if (i == 0)
            {
                buttons[i].button.Select();
            }
        }
    }

    private void SetButtonText(int index, string text)
    {
        if (usesTMP[index] && buttonTextsTMP[index] != null)
        {
            buttonTextsTMP[index].text = text;
        }
        else if (!usesTMP[index] && buttonTextsLegacy[index] != null)
        {
            buttonTextsLegacy[index].text = text;
        }
    }

    private void SetButtonColor(int index, Color color)
    {
        if (usesTMP[index] && buttonTextsTMP[index] != null)
        {
            buttonTextsTMP[index].color = color;
        }
        else if (!usesTMP[index] && buttonTextsLegacy[index] != null)
        {
            buttonTextsLegacy[index].color = color;
        }
    }

    private void UpdateButtonTransitions()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttonCanvasGroups[i].alpha < 1f) continue; // ???????? ?????? ? ???????? ????????

            if (fadeProgress[i] < 1f)
            {
                fadeProgress[i] += Time.deltaTime / buttons[i].fadeDuration;
                Color newColor = Color.Lerp(startColors[i], targetColors[i], fadeProgress[i]);
                SetButtonColor(i, newColor);
            }
        }
    }

    private void OnPointerEnterButton(int index)
    {
        if (index < 0 || index >= buttons.Count || buttonCanvasGroups[index].alpha < 1f) return;

        isHovering[index] = true;
        startColors[index] = usesTMP[index] ? buttonTextsTMP[index].color : buttonTextsLegacy[index].color;
        targetColors[index] = buttons[index].hoverColor;
        fadeProgress[index] = 0f;
        SetButtonText(index, buttons[index].hoverText);
    }

    private void OnPointerExitButton(int index)
    {
        if (index < 0 || index >= buttons.Count || buttonCanvasGroups[index].alpha < 1f) return;

        isHovering[index] = false;
        startColors[index] = usesTMP[index] ? buttonTextsTMP[index].color : buttonTextsLegacy[index].color;
        targetColors[index] = buttons[index].normalColor;
        fadeProgress[index] = 0f;
        SetButtonText(index, buttons[index].normalText);
    }

    private void OnButtonClicked(int index)
    {
        Debug.Log($"Button {index} clicked!");
    }

    public void AddNewButton()
    {
        buttons.Add(new ButtonSettings());
    }
}