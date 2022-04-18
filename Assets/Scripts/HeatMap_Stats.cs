using AimTrainer.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatMap_Stats : MonoBehaviour
{
    public Image Basic_Score;
    public Image Basic_Accuracy;
    public Image Basic_TargetsHit;
    public Image Basic_Time;
    public Image Basic_KPS;
    public Image Basic_TTK;

    private float[,] statArray = new float[3, 6];

    private int accuracy, score, targetsHit, time, killsPerSecond, timeToKill, gamesPlayed;
    private float accuracy_HSV, score_HSV, targetsHit_HSV, time_HSV, killsPerSecond_HSV, timeToKill_HSV;

    private int save_score;
    private int save_time_to_kill;

    private float save_targets_missed;
    private float save_accuracy;
    private float save_kills_per_sec;
    private float save_session_time;

    public enum levelEnum
    {
        BASIC,
        ANTICIPATION,
        MOVING
    }

    public enum statTypeEnum
    {
        SCORE,
        ACCURACY,
        TARGETS_HIT,
        TIME,
        KPS,
        TTK
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetImageValues();
        LoadSavedStats(levelEnum.BASIC);
        SetImageValues();
        //PrintDebug();
    }

    private void PrintDebug()
    {
        Debug.Log("Accuracy - " + accuracy);
        Debug.Log("Score - " + score);
        Debug.Log("Targets Hit - " + targetsHit);
        Debug.Log("Time - " + time);
        Debug.Log("KPS - " + killsPerSecond);
        Debug.Log("TTK - " + timeToKill);
    }

    private void SetImageValues()
    {
        Basic_Score.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.SCORE)], 1f, 1f);
        Basic_Accuracy.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.ACCURACY)], 1f, 1f);
        Basic_TargetsHit.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TARGETS_HIT)], 1f, 1f);
        Basic_Time.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TIME)], 1f, 1f);
        Basic_KPS.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.KPS)], 1f, 1f);
        Basic_TTK.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TTK)], 1f, 1f);

        //if (gameStatus_Type == levelEnum.BASIC)
        //{
        //    Basic_Score.color = Color.HSVToRGB(score_HSV, 1f, 1f);
        //    Basic_Accuracy.color = Color.HSVToRGB(accuracy_HSV, 1f, 1f);
        //    Basic_TargetsHit.color = Color.HSVToRGB(targetsHit_HSV, 1f, 1f);
        //    Basic_Time.color = Color.HSVToRGB(time_HSV, 1f, 1f);
        //    Basic_KPS.color = Color.HSVToRGB(killsPerSecond_HSV, 1f, 1f);
        //    Basic_TTK.color = Color.HSVToRGB(timeToKill_HSV, 1f, 1f);
        //}
    }

    // Get value representation used in HSVtoRGB()
    // 0f - red
    // 0.33f - green
    private void CalculateRelativeHSVVals()
    {
        // Dividing by 360 because the maximum H value in HSV color is 360
        // 0 is red and 120 is green
        // All of these values (0-360) needs to be converted to a float betweeen [0f - 1f]
        // which will be used in HSVtoRGB()
        score_HSV = (float)((score * 1.2) / 360);
        accuracy_HSV = (float)((accuracy * 1.2) / 360);
        targetsHit_HSV = (float)((targetsHit * 1.2) / 360);
        time_HSV = (float)((time * 1.2) / 360);
        killsPerSecond_HSV = (float)((killsPerSecond * 1.2) / 360);
        timeToKill_HSV = (float)((timeToKill * 1.2) / 360);

        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.SCORE)] = (float)((score * 1.2) / 360);
        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.ACCURACY)] = (float)((accuracy * 1.2) / 360);
        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TARGETS_HIT)] = (float)((targetsHit * 1.2) / 360);
        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TIME)] = (float)((time * 1.2) / 360);
        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.KPS)] = (float)((killsPerSecond * 1.2) / 360);
        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TTK)] = (float)((timeToKill * 1.2) / 360);
    }

    private void CalculateNormalizedVals()
    {
        score = (int)((save_score * 100) / LevelStats.BASIC_SCORE_MAX);
        accuracy = (int)save_accuracy;
        targetsHit = (int)(((LevelStats.BASIC_TARGETS_MAX - save_targets_missed) * 100) / LevelStats.BASIC_TARGETS_MAX);
        time = (int)(((save_session_time - LevelStats.BASIC_TIME_MIN) * 100) / LevelStats.BASIC_TIME_MAX - LevelStats.BASIC_TIME_MIN);
        // must invert value
        time = 100 - time;
        if (time > 100)
        {
            time = 100;
        }
        else if (time < 0)
        {
            time = 0;
        }

        killsPerSecond = (int)(((save_kills_per_sec - LevelStats.BASIC_KPS_MIN) * 100) / LevelStats.BASIC_KPS_MAX - LevelStats.BASIC_KPS_MIN);
        timeToKill = (save_time_to_kill - LevelStats.BASIC_TTK_MIN) * 100 / (LevelStats.BASIC_TTK_MAX - LevelStats.BASIC_TTK_MIN);

        // We invert these values in refrence to 100 because we want to represent
        // minimum values in green and maximum values in red in HSV format
        score = 100 - score;
        accuracy = 100 - accuracy;
        targetsHit = 100 - targetsHit;
        time = 100 - time;
        killsPerSecond = 100 - killsPerSecond;
        timeToKill = 100 - timeToKill;

        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.SCORE)] = score;
        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.ACCURACY)] = accuracy;
        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TARGETS_HIT)] = targetsHit;
        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TIME)] = time;
        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.KPS)] = killsPerSecond;
        statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TTK)] = timeToKill;
    }

    private void LoadSavedStats(levelEnum gameStatus_Type)
    {
        gamesPlayed = 0;
        SessionData sessionData = SaveSystem.LoadSession();
        foreach (SessionData_instance instance in sessionData.session_list)
        {
            if (gameStatus_Type == levelEnum.BASIC && instance.level_name == "Basic targets")
            {
                gamesPlayed++;
                save_score += instance.score;
                save_time_to_kill += instance.time_to_kill;
                save_targets_missed += instance.targets_missed;
                save_accuracy += instance.accuracy;
                save_kills_per_sec += instance.kills_per_sec;
                save_session_time += instance.session_time;
            }
        }

        // Get average vals
        save_score /= gamesPlayed;
        save_time_to_kill /= gamesPlayed;
        save_targets_missed /= gamesPlayed;
        save_accuracy /= gamesPlayed;
        save_kills_per_sec /= gamesPlayed;
        save_session_time /= gamesPlayed;

        CalculateNormalizedVals();
        CalculateRelativeHSVVals();
    }
}
