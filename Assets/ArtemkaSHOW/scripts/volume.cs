using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Header("Настройки звука")]
    [SerializeField] private AudioMixer audioMixer; // Ссылка на Audio Mixer (если используется)
    [SerializeField] private string volumeParameter = "MasterVolume"; // Параметр громкости в микшере

    [Header("UI элементы")]
    [SerializeField] private Slider volumeSlider; // Ссылка на UI Slider

    private void Start()
    {
        // Инициализация ползунка текущим значением громкости
        if (audioMixer != null)
        {
            // Для AudioMixer
            audioMixer.GetFloat(volumeParameter, out float currentVolume);
            volumeSlider.value = Mathf.Pow(10, currentVolume / 20); // Преобразование из dB в линейное значение
        }
        else
        {
            // Для AudioListener (если не используется AudioMixer)
            volumeSlider.value = AudioListener.volume;
        }

        // Добавляем обработчик изменения значения ползунка
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        if (audioMixer != null)
        {
            // Для AudioMixer - преобразуем линейное значение в dB
            audioMixer.SetFloat(volumeParameter, Mathf.Log10(volume) * 20);
        }
        else
        {
            // Для AudioListener (если не используется AudioMixer)
            AudioListener.volume = volume;
        }

        // Сохраняем значение громкости (опционально)
        PlayerPrefs.SetFloat(volumeParameter, volume);
    }

    private void OnDestroy()
    {
        // Удаляем обработчик при уничтожении объекта
        volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
    }
}