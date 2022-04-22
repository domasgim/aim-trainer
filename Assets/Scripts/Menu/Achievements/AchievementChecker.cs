using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject statsMenu;
    private int totalGamesPlayed, basicGamesPlayed, movingGamesPlayed, anticipationGamesPlayed;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        LoadSavedStats();
        StartCoroutine(CheckForAchievements(1));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            AchievementManager.Instance.EarnAchievement("Press W");
        }
        if (statsMenu.active == true)
        {
            AchievementManager.Instance.EarnAchievement("Much info");
        }
    }

    private void LoadSavedStats()
    {
        SessionData sessionData = SaveSystem.LoadSession();
        foreach (SessionData_instance instance in sessionData.session_list)
        {
            if (instance.level_name == "Basic targets")
            {
                basicGamesPlayed++;
                //save_score += instance.score;
                //save_time_to_kill += instance.time_to_kill;
                //save_targets_missed += instance.targets_missed;
                //save_accuracy += instance.accuracy;
                //save_kills_per_sec += instance.kills_per_sec;
                //save_session_time += instance.session_time;
            }
            if (instance.level_name == "Moving targets")
            {
                movingGamesPlayed++;
            }
            if (instance.level_name == "Anticipation targets")
            {
                anticipationGamesPlayed++;
            }
        }
        totalGamesPlayed = basicGamesPlayed + movingGamesPlayed + anticipationGamesPlayed;

        // Get average vals
        //save_score /= gamesPlayed;
        //save_time_to_kill /= gamesPlayed;
        //save_targets_missed /= gamesPlayed;
        //save_accuracy /= gamesPlayed;
        //save_kills_per_sec /= gamesPlayed;
        //save_session_time /= gamesPlayed;
    }
    IEnumerator CheckForAchievements(int secs)
    {
        yield return new WaitForSeconds(secs);

        if (totalGamesPlayed >= 1)
        {
            AchievementManager.Instance.EarnAchievement("Newborn");
        }
        if (totalGamesPlayed > 10)
        {
            AchievementManager.Instance.EarnAchievement("Old dog 1");
        }
        if (totalGamesPlayed > 100)
        {
            AchievementManager.Instance.EarnAchievement("Old dog 2");
        }
        if (totalGamesPlayed > 500)
        {
            AchievementManager.Instance.EarnAchievement("Old dog 3");
        }
    }
}
