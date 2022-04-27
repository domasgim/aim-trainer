using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AimTrainer.Utils;

public class AchievementChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject statsMenu;
    private int totalGamesPlayed, basicGamesPlayed, movingGamesPlayed, anticipationGamesPlayed;
    private int maxConsecutiveTargetsHitBasic;
    private int maxConsecutiveTargetsHitMoving;
    private int maxConsecutiveTargetsHitAnticipation;
    private bool areAnyNullTargetsHit;
    private bool sharpshooterBasicFlag;
    private bool sharpshooterMovingFlag;
    private bool sharpshooterAnticipationFlag;
    private bool maxScoreBasic;
    private bool maxScoreMoving;
    private bool maxScoreAnticipation;
    private bool timeFlagBasic;
    private bool timeFlagMoving;
    private bool timeFlagAnticipation;

    private void Awake()
    {
        Time.timeScale = 1f;
        ResetValues();
        PlayerPrefs.DeleteAll();
        LoadSavedStats();
        StopAllCoroutines();
        StartCoroutine(CheckForAchievements(1));
    }

    void ResetValues()
    {
        maxConsecutiveTargetsHitBasic = 0;
        maxConsecutiveTargetsHitMoving = 0;
        maxConsecutiveTargetsHitAnticipation = 0;
        areAnyNullTargetsHit = false;
        sharpshooterBasicFlag = false;
        sharpshooterMovingFlag = false;
        sharpshooterAnticipationFlag = false;
        maxScoreBasic = false;
        maxScoreMoving = false;
        maxScoreAnticipation = false;
        timeFlagBasic = false;
        timeFlagMoving = false;
        timeFlagAnticipation = false;
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
                if (instance.consecutive_targets_hit > maxConsecutiveTargetsHitBasic)
                {
                    maxConsecutiveTargetsHitBasic = instance.consecutive_targets_hit;
                }
                if (instance.targets_missed == LevelStats.BASIC_TARGETS_MAX)
                {
                    areAnyNullTargetsHit = true;
                }
                if (instance.targets_missed == 0)
                {
                    sharpshooterBasicFlag = true;
                }
                if (instance.score == LevelStats.BASIC_SCORE_MAX)
                {
                    maxScoreBasic = true;
                }
                if (instance.session_time < 5)
                {
                    timeFlagBasic = true;
                }
            }
            if (instance.level_name == "Moving targets")
            {
                movingGamesPlayed++;
                if (instance.consecutive_targets_hit > maxConsecutiveTargetsHitMoving)
                {
                    maxConsecutiveTargetsHitMoving = instance.consecutive_targets_hit;
                }
                if (instance.targets_missed == LevelStats.MOVING_TARGETS_MAX)
                {
                    areAnyNullTargetsHit = true;
                }
                if (instance.targets_missed == 0)
                {
                    sharpshooterMovingFlag = true;
                }
                if (instance.score == LevelStats.MOVING_SCORE_MAX)
                {
                    maxScoreMoving = true;
                }
                if (instance.session_time < 5)
                {
                    timeFlagMoving = true;
                }
            }
            if (instance.level_name == "Anticipation targets")
            {
                anticipationGamesPlayed++;
                if (instance.consecutive_targets_hit > maxConsecutiveTargetsHitAnticipation)
                {
                    maxConsecutiveTargetsHitAnticipation = instance.consecutive_targets_hit;
                }
                if (instance.targets_missed == LevelStats.ANTICIPATION_TARGETS_MAX)
                {
                    areAnyNullTargetsHit = true;
                }
                if (instance.targets_missed == 0)
                {
                    sharpshooterAnticipationFlag = true;
                }
                if (instance.score == LevelStats.ANTICIPATION_SCORE_MAX)
                {
                    maxScoreAnticipation = true;
                }
                if (instance.session_time < 5)
                {
                    timeFlagAnticipation = true;
                }
            }
        }
        totalGamesPlayed = basicGamesPlayed + movingGamesPlayed + anticipationGamesPlayed;

    }
    IEnumerator CheckForAchievements(int secs)
    {
        yield return new WaitForSeconds(secs);
        CheckAllAchievements();
    }
    private void CheckAllAchievements()
    {
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
        if (maxConsecutiveTargetsHitBasic >= 4)
        {
            AchievementManager.Instance.EarnAchievement("Jhin (basic)");
        }
        if (maxConsecutiveTargetsHitMoving >= 4)
        {
            AchievementManager.Instance.EarnAchievement("Jhin (moving)");
        }
        if (maxConsecutiveTargetsHitAnticipation >= 4)
        {
            AchievementManager.Instance.EarnAchievement("Jhin (anticipation)");
        }
        if (areAnyNullTargetsHit)
        {
            AchievementManager.Instance.EarnAchievement("Sleepy Joe");
        }
        if (sharpshooterBasicFlag)
        {
            AchievementManager.Instance.EarnAchievement("Sharpshooter (basic)");
        }
        if (sharpshooterMovingFlag)
        {
            AchievementManager.Instance.EarnAchievement("Sharpshooter (moving)");
        }
        if (sharpshooterAnticipationFlag)
        {
            AchievementManager.Instance.EarnAchievement("Sharpshooter (anticipation)");
        }
        if (maxScoreBasic)
        {
            AchievementManager.Instance.EarnAchievement("Money shot (basic)");
        }
        if (maxScoreMoving)
        {
            AchievementManager.Instance.EarnAchievement("Money shot (moving)");
        }
        if (maxScoreAnticipation)
        {
            AchievementManager.Instance.EarnAchievement("Money shot (anticipation)");
        }
        if (timeFlagBasic)
        {
            AchievementManager.Instance.EarnAchievement("Are you Dream? (basic)");
        }
        if (timeFlagMoving)
        {
            AchievementManager.Instance.EarnAchievement("Are you Dream? (moving)");
        }
        if (timeFlagAnticipation)
        {
            AchievementManager.Instance.EarnAchievement("Are you Dream? (anticipation)");
        }
    }
}
