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
    private List<AchievementInstance> achievementDependencies = new List<AchievementInstance>();
    private string currentAchievementChild;

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
    public string CurrentAchievementChild
    {
        get 
        { 
            return currentAchievementChild; 
        }
        set 
        { 
            currentAchievementChild = value; 
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

    public void AddDependency(AchievementInstance dependency)
    {
        achievementDependencies.Add(dependency);
    }

    public bool EarnAchievement()
    {
        if (!unlocked)
        {
            bool dependenciesMatched = true;
            foreach (AchievementInstance instance in achievementDependencies)
            {
                if (instance.unlocked == false)
                {
                    dependenciesMatched = false;
                }
            }
            if (dependenciesMatched)
            {
                achievementReference.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
                SaveAchievement(true);

                if (currentAchievementChild != null)
                {
                    AchievementManager.Instance.EarnAchievement(currentAchievementChild);
                }

                return true;
            }
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
