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

    public string Name
    {
        get 
        { 
            return name;
        }
        set
        { 
            name = value;
        }
    }
    public string Description
    {
        get
        { 
            return description;
        }
        set
        { 
            name = value;
        }
    }
    public bool Unlocked
    {
        get 
        { 
            return unlocked; 
        }
        set
        { 
            unlocked = value;
        }
    }
    public int SpriteIndex
    {
        get 
        {
            return spriteIndex;
        }
        set 
        { 
            spriteIndex = value;
        }
    }

    public AchievementInstance(string name, string description, int spriteIndex, GameObject achievementReference)
    {
        this.name = name;
        this.description = description;
        this.unlocked = false;
        this.spriteIndex = spriteIndex;
        this.achievementReference = achievementReference;
        LoadAchievement();
    }

    public bool EarnAchievement()
    {
        if (!unlocked)
        {
            achievementReference.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
            SaveAchievement(true);
            return true;
        }
        return false;
    }

    public void SaveAchievement(bool unlocked)
    {
        this.unlocked = unlocked;

        if (unlocked)
        {
            PlayerPrefs.SetInt(name, 1);
        }
        else
        {
            PlayerPrefs.SetInt(name, 0);
        }

        PlayerPrefs.Save();
    }

    public void LoadAchievement()
    {
        if (PlayerPrefs.GetInt(name) == 1)
        {
            unlocked = true;
        } 
        else
        {
            unlocked = false;
        }

        if (unlocked)
        {
            achievementReference.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
        }
    }
}
