using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class LoadingScreen : MonoBehaviour
{
    [Header("Настройки загрузки")]
    [SerializeField] private float minLoadTime = 2f;
    [SerializeField] private float maxLoadTime = 5f;
    [SerializeField] private string sceneToLoad = "GameScene";

    [Header("UI Элементы")]
    [SerializeField] private Image progressFill; 
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text tipText;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private Animator backgroundAnimator;

    [Header("Звуки")]
    [SerializeField] private AudioClip loadingMusic;
    [SerializeField] private AudioClip buttonAppearSound;
    [SerializeField] private AudioClip buttonClickSound;

    [Header("Советы")]
    [SerializeField]
    private List<string> tips = new List<string>()
    {
        "Используйте укрытия для защиты от вражеского огня.",
        "Не забывайте пополнять запасы здоровья.",
        "Исследуйте каждый уголок уровня, чтобы найти секреты.",
        "Комбинируйте способности для более эффективных атак.",
        "Следите за индикатором запасов боеприпасов."
    };

    private AudioSource audioSource;
    private float loadProgress = 0f;
    private bool isLoading = false;
    private bool loadComplete = false;
    private float loadDuration;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        continueButton.SetActive(false);

        
        if (progressFill != null)
        {
            progressFill.type = Image.Type.Filled;
            progressFill.fillMethod = Image.FillMethod.Horizontal; 
            progressFill.fillAmount = 0f;
        }
    }

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        isLoading = true;
        loadComplete = false;
        loadDuration = Random.Range(minLoadTime, maxLoadTime);
        float elapsedTime = 0f;

        
        if (loadingMusic != null)
        {
            audioSource.clip = loadingMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

     
        ShowRandomTip();

        
        if (backgroundAnimator != null)
        {
            backgroundAnimator.SetTrigger("StartLoading");
        }

        while (elapsedTime < loadDuration)
        {
            elapsedTime += Time.deltaTime;
            loadProgress = Mathf.Clamp01(elapsedTime / loadDuration);

            
            UpdateProgressUI(loadProgress);

            yield return null;
        }

        loadProgress = 1f;
        UpdateProgressUI(loadProgress);

        
        OnLoadComplete();
    }

    private void UpdateProgressUI(float progress)
    {
        if (progressFill != null)
        {
            progressFill.fillAmount = progress; 
        }

        if (progressText != null)
        {
            progressText.text = $"{(progress * 100):0}%";
        }
    }

    private void ShowRandomTip()
    {
        if (tips.Count > 0 && tipText != null)
        {
            int randomIndex = Random.Range(0, tips.Count);
            tipText.text = $"{tips[randomIndex]}";
        }
    }

    private void OnLoadComplete()
    {
        isLoading = false;
        loadComplete = true;

        
        if (continueButton != null)
        {
            continueButton.SetActive(true);

            if (buttonAnimator != null)
            {
                buttonAnimator.SetTrigger("Appear");
            }

            if (buttonAppearSound != null)
            {
                audioSource.PlayOneShot(buttonAppearSound);
            }
        }
        else
        {
           
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void OnContinueButtonClicked()
    {
        if (!loadComplete) return;

        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }

        
        SceneManager.LoadScene(sceneToLoad);
    }
}