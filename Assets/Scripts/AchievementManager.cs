using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public GameObject achievementPrefab;
    public GameObject visualAchievement;
    public Sprite unlockedSprite;
    public Sprite[] spriteList;
    public Dictionary<string, AchievementInstance> achievementDictionary = new Dictionary<string, AchievementInstance>();

    private static AchievementManager instance;

    public static AchievementManager Instance
    {
        get 
        { 
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AchievementManager>();
            }
            return AchievementManager.instance; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateAchievement("GeneralCategory", "Press W", "Press W :)", 0);
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
        if (achievementDictionary[title].EarnAchievement())
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
        GameObject achievementGameObject = (GameObject)Instantiate(achievementPrefab);
        AchievementInstance newAchievementInstance = new AchievementInstance(title, description, spriteIndex, achievementGameObject);
        achievementDictionary.Add(title, newAchievementInstance);
        SetAchievementInfo(parent, achievementGameObject, title);
    }

    public void SetAchievementInfo(string parent, GameObject achievementGameObject, string title)
    {
        achievementGameObject.transform.SetParent(GameObject.Find(parent).transform);
        achievementGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title;
        achievementGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = achievementDictionary[title].Description;
        achievementGameObject.transform.GetChild(2).GetComponent<Image>().sprite = spriteList[achievementDictionary[title].SpriteIndex];
    }
}
