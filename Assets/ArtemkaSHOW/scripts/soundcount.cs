using UnityEngine;
using System.Collections.Generic;

public class DelayedSoundPlayer : MonoBehaviour
{
    [System.Serializable]
    public class SoundEntry
    {
        public AudioClip sound;
        public float delayInSeconds;
        [Range(0f, 1f)] public float volume = 1f;
    }

    public List<SoundEntry> soundEntries = new List<SoundEntry>();
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        foreach (var entry in soundEntries)
        {
            if (entry.sound != null)
            {
                StartCoroutine(PlaySoundWithDelay(entry));
            }
            else
            {
                Debug.LogWarning("Не назначен звуковой клип в одном из элементов списка!");
            }
        }
    }

    private System.Collections.IEnumerator PlaySoundWithDelay(SoundEntry entry)
    {
        yield return new WaitForSeconds(entry.delayInSeconds);
        audioSource.PlayOneShot(entry.sound, entry.volume);
    }
}