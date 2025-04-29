using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum AchievementRarity
{
    Common,      // Обычная
    Uncommon,    // Необычная
    Rare,        // Редкая
    Epic,        // Эпическая
    Legendary    // Легендарная
}

[System.Serializable]
public class Achievement
{
    public string id;
    public string title;
    [TextArea] public string description;
    public Sprite icon;
    public AchievementRarity rarity;
    public bool isHidden;
    public bool isUnlocked;
    public UnityEvent onUnlock;
}