using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class ButtonSound : MonoBehaviour
{
    [Header("Sound Settings")]
    public AudioClip clickSound; // Звук, который будет воспроизводиться
    [Range(0f, 1f)] public float volume = 1f; // Громкость звука

    private Button button;
    private AudioSource audioSource;

    void Start()
    {
        // Получаем компоненты
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        // Настраиваем AudioSource
        audioSource.playOnAwake = false;
        audioSource.volume = volume;

        // Добавляем обработчик нажатия кнопки
        button.onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning("No click sound assigned!", this);
        }
    }

    void OnDestroy()
    {
        // Удаляем обработчик при уничтожении объекта
        button.onClick.RemoveListener(PlayClickSound);
    }
}