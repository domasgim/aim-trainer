using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu_Stats_1 : MonoBehaviour
{
    public TextMeshProUGUI averageAccuracyText, highScoreText, gamesPlayedText;

    private int highScore, gamesPlayed;
    private float accuracy;

    public enum levelEnum
    {
        BASIC,
        ANTICIPATION,
        MOVING
    }

    public levelEnum gameStatus = levelEnum.BASIC;

    public void UpdateStats()
    {
        highScore = 0;
        gamesPlayed = 0;
        accuracy = 0f;

        SessionData sessionData = SaveSystem.LoadSession();
        foreach (SessionData_instance instance in sessionData.session_list)
        {
            gamesPlayed++;
            accuracy += instance.accuracy;
            if (highScore < instance.score)
            {
                highScore = instance.score;
            }
        }
        accuracy /= gamesPlayed;

        averageAccuracyText.text = "Average accuracy: " + accuracy + "%";
        highScoreText.text = "High-score: " + highScore;
        gamesPlayedText.text = "Games played: " + gamesPlayed;
    }
}
