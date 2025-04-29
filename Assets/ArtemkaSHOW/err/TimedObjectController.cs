using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TimeInterval
{
    public float enableDuration = 1f;
    public float disableDuration = 1f;
}

public class TimedObjectController : MonoBehaviour
{
    [Header("Object Settings")]
    public GameObject targetObject;
    public List<TimeInterval> objectIntervals = new List<TimeInterval>()
    {
        new TimeInterval { enableDuration = 2f, disableDuration = 1f },
        new TimeInterval { enableDuration = 1f, disableDuration = 3f }
    };

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public List<TimeInterval> audioIntervals = new List<TimeInterval>();

    [Header("Text Settings")]
    public TMP_Text textDisplay;
    public string displayText = "Hello World";
    public List<TimeInterval> textIntervals = new List<TimeInterval>();

    private void Start()
    {
        if (targetObject != null && objectIntervals.Count > 0)
            StartCoroutine(ControlObject());

        if (audioSource != null && audioIntervals.Count > 0)
            StartCoroutine(ControlAudio());

        if (textDisplay != null && textIntervals.Count > 0)
            StartCoroutine(ControlText());
    }

    private IEnumerator ControlObject()
    {
        int currentIndex = 0;
        while (true)
        {
            targetObject.SetActive(true);
            yield return new WaitForSeconds(objectIntervals[currentIndex].enableDuration);

            targetObject.SetActive(false);
            yield return new WaitForSeconds(objectIntervals[currentIndex].disableDuration);

            currentIndex = (currentIndex + 1) % objectIntervals.Count;
        }
    }

    private IEnumerator ControlAudio()
    {
        int currentIndex = 0;
        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioIntervals[currentIndex].enableDuration);

            audioSource.Stop();
            yield return new WaitForSeconds(audioIntervals[currentIndex].disableDuration);

            currentIndex = (currentIndex + 1) % audioIntervals.Count;
        }
    }

    private IEnumerator ControlText()
    {
        int currentIndex = 0;
        while (true)
        {
            textDisplay.text = displayText;
            textDisplay.gameObject.SetActive(true);
            yield return new WaitForSeconds(textIntervals[currentIndex].enableDuration);

            textDisplay.gameObject.SetActive(false);
            yield return new WaitForSeconds(textIntervals[currentIndex].disableDuration);

            currentIndex = (currentIndex + 1) % textIntervals.Count;
        }
    }

    // Public methods to modify intervals during runtime
    public void AddObjectInterval(float enable, float disable)
    {
        objectIntervals.Add(new TimeInterval { enableDuration = enable, disableDuration = disable });
    }

    public void ClearAllIntervals()
    {
        objectIntervals.Clear();
        audioIntervals.Clear();
        textIntervals.Clear();
    }
}