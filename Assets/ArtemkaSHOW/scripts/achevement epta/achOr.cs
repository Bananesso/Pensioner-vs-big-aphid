using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AchievementSystem : MonoBehaviour
{
    [Header("Achievement List")]
    public List<Achievement> achievements = new List<Achievement>();

    [Header("UI References")]
    public CanvasGroup achievementPanel;
    public Image achievementIcon;
    public Text achievementTitle;
    public Text achievementDescription;
    public Image rarityBackground;
    public ParticleSystem rarityParticles;

    [Header("Settings")]
    public float showDuration = 3f;
    public float fadeDuration = 0.5f;
    public AudioClip unlockSound;

    [Header("Rarity Settings")]
    public Color commonColor = Color.white;
    public Color uncommonColor = Color.green;
    public Color rareColor = Color.blue;
    public Color epicColor = Color.magenta;
    public Color legendaryColor = Color.yellow;

    public ParticleSystem commonParticles;
    public ParticleSystem uncommonParticles;
    public ParticleSystem rareParticles;
    public ParticleSystem epicParticles;
    public ParticleSystem legendaryParticles;

    private AudioSource audioSource;
    private Queue<Achievement> achievementQueue = new Queue<Achievement>();
    private bool isShowingAchievement = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        achievementPanel.alpha = 0f;
        achievementPanel.gameObject.SetActive(false);
    }

    public void UnlockAchievement(string achievementId)
    {
        Achievement achievement = achievements.Find(a => a.id == achievementId);
        if (achievement != null && !achievement.isUnlocked)
        {
            achievement.isUnlocked = true;
            achievement.onUnlock?.Invoke();
            achievementQueue.Enqueue(achievement);

            if (!isShowingAchievement)
            {
                StartCoroutine(ShowNextAchievement());
            }
        }
    }

    private IEnumerator ShowNextAchievement()
    {
        while (achievementQueue.Count > 0)
        {
            isShowingAchievement = true;
            Achievement achievement = achievementQueue.Dequeue();

            // Skip hidden achievements
            if (achievement.isHidden) continue;

            // Setup UI
            achievementTitle.text = achievement.title;
            achievementDescription.text = achievement.description;
            achievementIcon.sprite = achievement.icon;

            // Set rarity effects
            SetRarityEffects(achievement.rarity);

            // Play sound
            if (unlockSound != null)
            {
                audioSource.PlayOneShot(unlockSound);
            }

            // Show panel with fade in
            achievementPanel.gameObject.SetActive(true);
            yield return StartCoroutine(FadePanel(0f, 1f, fadeDuration));

            // Wait for duration
            yield return new WaitForSeconds(showDuration);

            // Hide panel with fade out
            yield return StartCoroutine(FadePanel(1f, 0f, fadeDuration));
            achievementPanel.gameObject.SetActive(false);

            // Small delay between achievements
            yield return new WaitForSeconds(0.5f);
        }

        isShowingAchievement = false;
    }

    private IEnumerator FadePanel(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            achievementPanel.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        achievementPanel.alpha = to;
    }

    private void SetRarityEffects(AchievementRarity rarity)
    {
        // Set background color and particles
        switch (rarity)
        {
            case AchievementRarity.Common:
                rarityBackground.color = commonColor;
                if (commonParticles != null) commonParticles.Play();
                break;
            case AchievementRarity.Uncommon:
                rarityBackground.color = uncommonColor;
                if (uncommonParticles != null) uncommonParticles.Play();
                break;
            case AchievementRarity.Rare:
                rarityBackground.color = rareColor;
                if (rareParticles != null) rareParticles.Play();
                break;
            case AchievementRarity.Epic:
                rarityBackground.color = epicColor;
                if (epicParticles != null) epicParticles.Play();
                break;
            case AchievementRarity.Legendary:
                rarityBackground.color = legendaryColor;
                if (legendaryParticles != null) legendaryParticles.Play();
                break;
        }
    }

    [ContextMenu("Test Common Achievement")]
    public void TestCommonAchievement()
    {
        if (achievements.Count > 0)
        {
            UnlockAchievement(achievements[0].id);
        }
    }
}