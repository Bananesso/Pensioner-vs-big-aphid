using UnityEngine;

public class SoundSwitchTrigger : MonoBehaviour
{
    public AudioClip firstSound; // Первый звук (перетащите в инспекторе)
    public AudioClip secondSound; // Второй звук (перетащите в инспекторе)
    public float volume = 1.0f; // Громкость звуков

    private AudioSource audioSource;
    private bool isFirstSound = true; // Флаг для отслеживания текущего звука

    void Start()
    {
        // Добавляем компонент AudioSource, если его нет
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, если объект, вошедший в триггер - игрок (можно изменить тег)
        if (other.CompareTag("Player"))
        {
            // Останавливаем текущий звук, если он играет
            audioSource.Stop();

            // Выбираем и воспроизводим следующий звук
            if (isFirstSound && firstSound != null)
            {
                audioSource.PlayOneShot(firstSound, volume);
            }
            else if (!isFirstSound && secondSound != null)
            {
                audioSource.PlayOneShot(secondSound, volume);
            }
            else
            {
                Debug.LogWarning("Sound clips are not assigned in the inspector!");
                return;
            }

            // Переключаем флаг для следующего звука
            isFirstSound = !isFirstSound;
        }
    }
}