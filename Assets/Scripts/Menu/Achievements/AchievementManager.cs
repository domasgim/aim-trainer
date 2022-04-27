using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public GameObject achievementMenu;
    public GameObject achievementPrefab;
    public GameObject visualAchievement;
    public Sprite unlockedSprite;
    public Sprite[] spriteList;
    public Dictionary<string, AchievementInstance> achievementDictionary = new Dictionary<string, AchievementInstance>();
    private AchievementButton activeButton;
    public ScrollRect scrollRect;
    public float fadeTime = 2;
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

    private void Awake()
    {
        LoadAchievements();
        activeButton = GameObject.Find("GeneralButton (1)").GetComponent<AchievementButton>();
        activeButton.Click();
        achievementMenu.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ActivateAchievementMenu()
    {
        achievementMenu.SetActive(true);
    }

    public void DeactivateAchievementMenu()
    {
        achievementMenu.SetActive(false);
    }

    public void LoadAchievements()
    {
        //PlayerPrefs.DeleteAll();
        CreateAchievement("GeneralCategory", "Press W", "Press W :)", 0);
        CreateAchievement("GeneralCategory", "Newborn", "Play your first game!", 0);
        CreateAchievement("GeneralCategory", "Old dog 1", "Play 50 games", 0);
        CreateAchievement("GeneralCategory", "Old dog 2", "Play 100 games", 0);
        CreateAchievement("GeneralCategory", "Old dog 3", "Play 500 games", 0);
        CreateAchievement("GeneralCategory", "Much info", "Enter stats page", 0);
        CreateAchievement("GeneralCategory", "Sleepy Joe", "Miss all targets on any level", 0);

        CreateAchievement("BasicCategory", "Sharpshooter (basic)", "Hit all targets", 0);
        CreateAchievement("BasicCategory", "Money shot (basic)", "Earn maximum score", 0);
        CreateAchievement("BasicCategory", "Are you Dream? (basic)", "Finish session in less than X seconds", 0);
        CreateAchievement("BasicCategory", "Jhin (basic)", "Hit 4 without missing", 0);

        CreateAchievement("MovingCategory", "Sharpshooter (moving)", "Hit all targets", 0);
        CreateAchievement("MovingCategory", "Money shot (moving)", "Earn maximum score", 0);
        CreateAchievement("MovingCategory", "Are you Dream? (moving)", "Finish session in less than X seconds", 0);
        CreateAchievement("MovingCategory", "Jhin (moving)", "Hit 4 without missing", 0);

        CreateAchievement("AnticipationCategory", "Sharpshooter (anticipation)", "Hit all targets", 0);
        CreateAchievement("AnticipationCategory", "Money shot (anticipation)", "Earn maximum score", 0);
        CreateAchievement("AnticipationCategory", "Are you Dream? (anticipation)", "Finish session in less than X seconds", 0);
        CreateAchievement("AnticipationCategory", "Jhin (anticipation)", "Hit 4 without missing", 0);

        foreach (GameObject achievementList in GameObject.FindGameObjectsWithTag("AchievementList"))
        {
            achievementList.SetActive(false);
        }
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
            StartCoroutine(FadeAchievement(achievement));
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

    public void ChangeCategory(GameObject button)
    {
        AchievementButton achievementButton = button.GetComponent<AchievementButton>();
        scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform>();
        // Deactivate this button
        achievementButton.Click();
        // Activate this button
        activeButton.Click();
        activeButton = achievementButton;
    }

    private IEnumerator FadeAchievement(GameObject achievementGameObject)
    {
        CanvasGroup canvasGroup = achievementGameObject.GetComponent<CanvasGroup>();
        float rate = 1.0f / fadeTime;
        int startAlpha = 0;
        int endAlpha = 1;

        for (int i = 0; i < 2; i++)
        {
            float progress = 0.0f;

            while (progress < 1.0f)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(2);
            startAlpha = 1;
            endAlpha = 0;
        }
        Destroy(achievementGameObject);
    }
}
