using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    public GameObject achievementPrefab;

    public Sprite[] sprites;

    public GameObject visualAchievement;

    public Dictionary<string, AchievementInstance> achievements = new Dictionary<string, AchievementInstance>();

    public Sprite unlockedSprite;

    private static Achievements singletonAchievementsInstance;

    public static Achievements SingletonAchievementsInstance 
    {
        get
        {
            if (singletonAchievementsInstance == null)
            {
                singletonAchievementsInstance = GameObject.FindObjectOfType<Achievements>();
            }
            return Achievements.singletonAchievementsInstance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateAchievement("GeneralCategory", "Press W", "Press W (simple as that)", 0);
        //CreateAchievement("GeneralCategory", "Old dog 1", "Play 50 games", 0);
        //CreateAchievement("GeneralCategory", "Old dog 2", "Play 100 games", 0);
        //CreateAchievement("GeneralCategory", "Old dog 3", "Play 500 games", 0);
        //CreateAchievement("GeneralCategory", "Much info", "Enter stats page", 0);
        //CreateAchievement("GeneralCategory", "Sleepy Joe", "Miss all targets", 0);

        //CreateAchievement("BasicCategory", "Old dog 1", "Play 50 games", 0);
        //CreateAchievement("BasicCategory", "Old dog 2", "Play 100 games", 0);
        //CreateAchievement("BasicCategory", "Old dog 3", "Play 500 games", 0);
        //CreateAchievement("BasicCategory", "Much info", "Enter stats page", 0);
        //CreateAchievement("BasicCategory", "Sleepy Joe", "Miss all targets", 0);
        //CreateAchievement("BasicCategory", "Old dog 1", "Play 50 games", 0);
        //CreateAchievement("BasicCategory", "Old dog 2", "Play 100 games", 0);
        //CreateAchievement("BasicCategory", "Old dog 3", "Play 500 games", 0);
        //CreateAchievement("BasicCategory", "Much info", "Enter stats page", 0);
        //CreateAchievement("BasicCategory", "Sleepy Joe", "Miss all targets", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            EarnAchievement("Press W");
        }
    }

    public void EarnAchievement(string title)
    {
        if (achievements[title].EarnAchievement())
        {
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            SetAchievementInfo("EarnCanvas", achievement, title);
            StartCoroutine(HideAchievement(achievement));
        }
    }

    public IEnumerator HideAchievement(GameObject achievement)
    {
        yield return new WaitForSeconds(3);
        Destroy(achievement);
    }

    public void CreateAchievement(string parent, string title, string description, int spriteIndex)
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);

        AchievementInstance newAchievementInstance = new AchievementInstance(name, description, spriteIndex, achievement);

        achievements.Add(title, newAchievementInstance);

        SetAchievementInfo(parent, achievement, title);
    }

    public void SetAchievementInfo(string parent, GameObject achievement, string title)
    {
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title;
        achievement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = achievements[title].Description;
        achievement.transform.GetChild(2).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
    }
}
