using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementInstance
{
    private string name;
    private string description;
    private bool unlocked;
    private int spriteIndex;
    private GameObject achievementReference;
    public AchievementInstance(string name, string description, int spriteIndex, GameObject achievementReference)
    {
        this.Name = name;
        this.Description = description;
        this.Unlocked = false;
        this.SpriteIndex = spriteIndex;
        this.AchievementReference = achievementReference;
    }

    public string Name 
    { 
        get => name; 
        set => name = value; 
    }

    public string Description 
    { 
        get => description;
        set => description = value;
    }

    public bool Unlocked
    { 
        get => unlocked; 
        set => unlocked = value;
    }

    public int SpriteIndex 
    { 
        get => spriteIndex; 
        set => spriteIndex = value;
    }

    public GameObject AchievementReference 
    {
        get => achievementReference; 
        set => achievementReference = value; 
    }

    public bool EarnAchievement()
    {
        if (!Unlocked)
        {
            achievementReference.GetComponent<Image>().sprite = Achievements.SingletonAchievementsInstance.unlockedSprite;
            Unlocked = true;
            return true;
        }
        return false;
    }
}
