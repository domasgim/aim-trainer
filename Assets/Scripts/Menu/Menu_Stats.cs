using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu_Stats : MonoBehaviour
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

    [SerializeField]
    private levelEnum levelType = levelEnum.BASIC;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        UpdateStats(levelType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateStats(levelEnum levelType)
    {
        highScore = 0;
        gamesPlayed = 0;
        accuracy = 0f;

        SessionData sessionData = SaveSystem.LoadSession();
        foreach (SessionData_instance instance in sessionData.session_list)
        {
            if (levelType == levelEnum.BASIC && instance.level_name == "Basic targets")
            {
                gamesPlayed++;
                accuracy += instance.accuracy;
                if (highScore < instance.score)
                {
                    highScore = instance.score;
                }
            } 
            else if (levelType == levelEnum.MOVING && instance.level_name == "Moving targets")
            {
                gamesPlayed++;
                accuracy += instance.accuracy;
                if (highScore < instance.score)
                {
                    highScore = instance.score;
                }
            }
            else if (levelType == levelEnum.ANTICIPATION && instance.level_name == "Anticipation targets")
            {
                gamesPlayed++;
                accuracy += instance.accuracy;
                if (highScore < instance.score)
                {
                    highScore = instance.score;
                }
            }
        }
        accuracy /= gamesPlayed;

        averageAccuracyText.text = "Average accuracy: " + accuracy.ToString("0.00") + "%";
        highScoreText.text = "High-score: " + highScore;
        gamesPlayedText.text = "Games played: " + gamesPlayed;
    }
}
