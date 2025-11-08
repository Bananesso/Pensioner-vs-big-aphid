using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BassScaleController : MonoBehaviour
{
    public AudioSource audioSource;
    public float sensitivity = 1.5f;
    public float maxScale = 2.0f;
    public float minScale = 1.0f;
    public float smoothTime = 0.3f;

    private float[] spectrumData;
    private float currentVelocity;
    private Vector3 originalScale;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        spectrumData = new float[1024];
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            // Получаем спектр звука
            audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

            // Анализируем низкие частоты (басы)
            float bassLevel = GetBassLevel();

            // Преобразуем уровень басов в масштаб
            float targetScale = Mathf.Lerp(minScale, maxScale, bassLevel * sensitivity);

            // Плавно изменяем масштаб
            float currentScale = transform.localScale.x / originalScale.x;
            float newScale = Mathf.SmoothDamp(currentScale, targetScale, ref currentVelocity, smoothTime);

            transform.localScale = originalScale * newScale;
        }
    }

    float GetBassLevel()
    {
        // Анализируем первые 10% данных спектра (низкие частоты)
        int bassSamples = Mathf.FloorToInt(spectrumData.Length * 0.1f);
        float sum = 0f;

        for (int i = 0; i < bassSamples; i++)
        {
            sum += spectrumData[i];
        }

        return Mathf.Clamp01(sum / bassSamples * 10f);
    }
}