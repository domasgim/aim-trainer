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

    public Image Moving_Score;
    public Image Moving_Accuracy;
    public Image Moving_TargetsHit;
    public Image Moving_Time;
    public Image Moving_KPS;
    public Image Moving_TTK;

    public Image Anticipation_Score;
    public Image Anticipation_Accuracy;
    public Image Anticipation_TargetsHit;
    public Image Anticipation_Time;
    public Image Anticipation_KPS;
    public Image Anticipation_TTK;

    private float[,] statArray = new float[3, 6];

    private int accuracy, score, targetsHit, time, killsPerSecond, timeToKill, gamesPlayed;

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
        //PrintDebug();
        LoadSavedStats(levelEnum.MOVING);
        //PrintDebug();
        LoadSavedStats(levelEnum.ANTICIPATION);
        //PrintDebug();
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
        Debug.Log("AAAAAAAAAAAAAAA");
    }

    private void SetImageValues()
    {
        Basic_Score.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.SCORE)], 1f, 1f);
        Basic_Accuracy.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.ACCURACY)], 1f, 1f);
        Basic_TargetsHit.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TARGETS_HIT)], 1f, 1f);
        Basic_Time.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TIME)], 1f, 1f);
        Basic_KPS.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.KPS)], 1f, 1f);
        Basic_TTK.color = Color.HSVToRGB(statArray[((int)levelEnum.BASIC), ((int)statTypeEnum.TTK)], 1f, 1f);

        Moving_Score.color = Color.HSVToRGB(statArray[((int)levelEnum.MOVING), ((int)statTypeEnum.SCORE)], 1f, 1f);
        Moving_Accuracy.color = Color.HSVToRGB(statArray[((int)levelEnum.MOVING), ((int)statTypeEnum.ACCURACY)], 1f, 1f);
        Moving_TargetsHit.color = Color.HSVToRGB(statArray[((int)levelEnum.MOVING), ((int)statTypeEnum.TARGETS_HIT)], 1f, 1f);
        Moving_Time.color = Color.HSVToRGB(statArray[((int)levelEnum.MOVING), ((int)statTypeEnum.TIME)], 1f, 1f);
        Moving_KPS.color = Color.HSVToRGB(statArray[((int)levelEnum.MOVING), ((int)statTypeEnum.KPS)], 1f, 1f);
        Moving_TTK.color = Color.HSVToRGB(statArray[((int)levelEnum.MOVING), ((int)statTypeEnum.TTK)], 1f, 1f);

        Anticipation_Score.color = Color.HSVToRGB(statArray[((int)levelEnum.ANTICIPATION), ((int)statTypeEnum.SCORE)], 1f, 1f);
        Anticipation_Accuracy.color = Color.HSVToRGB(statArray[((int)levelEnum.ANTICIPATION), ((int)statTypeEnum.ACCURACY)], 1f, 1f);
        Anticipation_TargetsHit.color = Color.HSVToRGB(statArray[((int)levelEnum.ANTICIPATION), ((int)statTypeEnum.TARGETS_HIT)], 1f, 1f);
        Anticipation_Time.color = Color.HSVToRGB(statArray[((int)levelEnum.ANTICIPATION), ((int)statTypeEnum.TIME)], 1f, 1f);
        Anticipation_KPS.color = Color.HSVToRGB(statArray[((int)levelEnum.ANTICIPATION), ((int)statTypeEnum.KPS)], 1f, 1f);
        Anticipation_TTK.color = Color.HSVToRGB(statArray[((int)levelEnum.ANTICIPATION), ((int)statTypeEnum.TTK)], 1f, 1f);
    }

    // Get value representation used in HSVtoRGB()
    // 0f - red
    // 0.33f - green
    private void CalculateRelativeHSVVals(levelEnum levelType)
    {
        // Dividing by 360 because the maximum H value in HSV color is 360
        // 0 is red and 120 is green
        // All of these values (0-360) needs to be converted to a float betweeen [0f - 1f]
        // which will be used in HSVtoRGB()
        statArray[((int)levelType), ((int)statTypeEnum.SCORE)] = 
            (float)((statArray[((int)levelType), ((int)statTypeEnum.SCORE)] * 1.2) / 360);

        statArray[((int)levelType), ((int)statTypeEnum.ACCURACY)] = 
            (float)((statArray[((int)levelType), ((int)statTypeEnum.ACCURACY)] * 1.2) / 360);

        statArray[((int)levelType), ((int)statTypeEnum.TARGETS_HIT)] = 
            (float)((statArray[((int)levelType), ((int)statTypeEnum.TARGETS_HIT)] * 1.2) / 360);

        statArray[((int)levelType), ((int)statTypeEnum.TIME)] = 
            (float)((statArray[((int)levelType), ((int)statTypeEnum.TIME)] * 1.2) / 360);

        statArray[((int)levelType), ((int)statTypeEnum.KPS)] = 
            (float)((statArray[((int)levelType), ((int)statTypeEnum.KPS)] * 1.2) / 360);

        statArray[((int)levelType), ((int)statTypeEnum.TTK)] = 
            (float)((statArray[((int)levelType), ((int)statTypeEnum.TTK)] * 1.2) / 360);
    }

    private void CalculateNormalizedVals(levelEnum levelType)
    {
        score = (int)((save_score * 100) / LevelStats.BASIC_SCORE_MAX);
        accuracy = (int)save_accuracy;
        if (levelType == levelEnum.ANTICIPATION)
        {
            Debug.Log("Accuracy = " + accuracy);
        }
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

        statArray[((int)levelType), ((int)statTypeEnum.SCORE)] = score;
        statArray[((int)levelType), ((int)statTypeEnum.ACCURACY)] = accuracy;
        statArray[((int)levelType), ((int)statTypeEnum.TARGETS_HIT)] = targetsHit;
        statArray[((int)levelType), ((int)statTypeEnum.TIME)] = time;
        statArray[((int)levelType), ((int)statTypeEnum.KPS)] = killsPerSecond;
        statArray[((int)levelType), ((int)statTypeEnum.TTK)] = timeToKill;
    }

    public void ResetSaveVals()
    {
        gamesPlayed = 0;
        save_score = 0;
        save_time_to_kill = 0;
        save_targets_missed = 0;
        save_accuracy = 0;
        save_kills_per_sec = 0;
        save_session_time = 0;
    }

    private void LoadSavedStats(levelEnum levelType)
    {
        ResetSaveVals();
        gamesPlayed = 0;
        SessionData sessionData = SaveSystem.LoadSession();
        foreach (SessionData_instance instance in sessionData.session_list)
        {
            if (levelType == levelEnum.BASIC && instance.level_name == "Basic targets")
            {
                gamesPlayed++;
                save_score += instance.score;
                save_time_to_kill += instance.time_to_kill;
                save_targets_missed += instance.targets_missed;
                save_accuracy += instance.accuracy;
                save_kills_per_sec += instance.kills_per_sec;
                save_session_time += instance.session_time;
            }

            if (levelType == levelEnum.MOVING && instance.level_name == "Moving targets")
            {
                gamesPlayed++;
                save_score += instance.score;
                save_time_to_kill += instance.time_to_kill;
                save_targets_missed += instance.targets_missed;
                save_accuracy += instance.accuracy;
                save_kills_per_sec += instance.kills_per_sec;
                save_session_time += instance.session_time;
            }

            if (levelType == levelEnum.ANTICIPATION && instance.level_name == "Anticipation targets")
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
        if (levelType == levelEnum.ANTICIPATION)
        {
            Debug.Log("Save_accuracy = " + save_accuracy + " / " + gamesPlayed);
        }
        save_accuracy /= gamesPlayed;
        if (levelType == levelEnum.ANTICIPATION)
        {
            Debug.Log("Answ = " + save_accuracy);
        }
        save_kills_per_sec /= gamesPlayed;
        save_session_time /= gamesPlayed;

        CalculateNormalizedVals(levelType);
        CalculateRelativeHSVVals(levelType);
    }
}
