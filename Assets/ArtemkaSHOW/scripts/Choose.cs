using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AdvancedModeSelection : MonoBehaviour
{
    [Header("TMP Elements")]
    public TMP_Text modeDisplayText;
    public string[] modeNames = { "Classic", "Endless", "Classic+", "Extreme" };

    [Header("Buttons")]
    public Button[] modeButtons;

    [Header("Appearance Settings")]
    public Color selectedColor = Color.yellow;
    public Color normalColor = Color.white;
    public float colorTransitionSpeed = 5f;

    [Header("Animation")]
    public Animator textAnimator;
    public string animationTrigger = "ModeChange";

    [Header("Audio")]
    public AudioClip buttonClickSound;
    public AudioClip modeChangeSound;
    [Range(0f, 2f)] public float modeChangeSoundDelay = 0.3f;
    public AudioMixerGroup soundOutput;
    [Range(0f, 1f)] public float volume = 0.8f;

    [Header("Confirmation")]
    public TMP_Text confirmationText;
    public GameObject confirmationPanel;
    public Button confirmButton;
    public Button cancelButton;

    private int currentSelectedMode = 0;
    private AudioSource audioSource;
    private bool isConfirming = false;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = soundOutput;
        audioSource.volume = volume;
    }

    void Start()
    {
        if (modeButtons.Length != modeNames.Length)
        {
            Debug.LogError("Количество кнопок не совпадает с количеством режимов!");
            return;
        }

        for (int i = 0; i < modeButtons.Length; i++)
        {
            int modeIndex = i;
            modeButtons[i].onClick.AddListener(() => SelectMode(modeIndex));
        }

        if (confirmButton != null) confirmButton.onClick.AddListener(ConfirmSelection);
        if (cancelButton != null) cancelButton.onClick.AddListener(CancelSelection);

        UpdateModeDisplay();
        if (confirmationPanel != null) confirmationPanel.SetActive(false);
    }

    void Update()
    {
        if (!isConfirming)
        {
            for (int i = 0; i < modeButtons.Length; i++)
            {
                TMP_Text buttonText = modeButtons[i].GetComponentInChildren<TMP_Text>();
                if (buttonText != null)
                {
                    Color targetColor = (i == currentSelectedMode) ? selectedColor : normalColor;
                    buttonText.color = Color.Lerp(buttonText.color, targetColor, Time.deltaTime * colorTransitionSpeed);
                }
            }
        }
    }

    void SelectMode(int modeIndex)
    {
        if (isConfirming) return;

        CancelInvoke("PlayModeChangeSound");
        currentSelectedMode = modeIndex;
        PlaySound(buttonClickSound);
        UpdateModeDisplay();
    }

    void UpdateModeDisplay()
    {
        if (modeDisplayText != null)
        {
            modeDisplayText.text = modeNames[currentSelectedMode];

            if (textAnimator != null && !string.IsNullOrEmpty(animationTrigger))
            {
                textAnimator.SetTrigger(animationTrigger);
            }
        }

        if (modeChangeSound != null)
        {
            Invoke("PlayModeChangeSound", modeChangeSoundDelay);
        }
    }

    private void PlayModeChangeSound()
    {
        PlaySound(modeChangeSound);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void ShowConfirmation()
    {
        if (confirmationPanel != null)
        {
            isConfirming = true;
            confirmationPanel.SetActive(true);
            if (confirmationText != null)
            {
                confirmationText.text = $"Start {modeNames[currentSelectedMode]} Mode?";
            }
        }
    }

    void ConfirmSelection()
    {
        PlaySound(buttonClickSound);
        Debug.Log($"Режим {modeNames[currentSelectedMode]} запущен!");
        isConfirming = false;
        if (confirmationPanel != null) confirmationPanel.SetActive(false);

        // Здесь можно добавить загрузку сцены:
        // SceneManager.LoadScene(modeNames[currentSelectedMode]);
    }

    void CancelSelection()
    {
        PlaySound(buttonClickSound);
        isConfirming = false;
        if (confirmationPanel != null) confirmationPanel.SetActive(false);
    }

    public string GetSelectedModeName()
    {
        return modeNames[currentSelectedMode];
    }

    void OnDestroy()
    {
        // Отменяем все запланированные вызовы при уничтожении объекта
        CancelInvoke();
    }
}