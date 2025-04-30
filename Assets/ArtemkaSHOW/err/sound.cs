using UnityEngine;

public class DelayedAudioPlayer : MonoBehaviour
{
    public AudioClip soundClip; // Перетащите сюда ваш аудиофайл через инспектор
    public float delayInSeconds = 5f; // Время задержки в секундах

    private AudioSource audioSource;

    void Start()
    {
        // Проверяем, есть ли компонент AudioSource, если нет - добавляем
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Настраиваем AudioSource
        audioSource.clip = soundClip;
        audioSource.playOnAwake = false;

        // Запускаем корутину с задержкой
        StartCoroutine(PlaySoundAfterDelay());
    }

    System.Collections.IEnumerator PlaySoundAfterDelay()
    {
        // Ждём указанное количество секунд
        yield return new WaitForSeconds(delayInSeconds);

        // Воспроизводим звук
        if (soundClip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioClip не назначен в инспекторе!");
        }
    }
}