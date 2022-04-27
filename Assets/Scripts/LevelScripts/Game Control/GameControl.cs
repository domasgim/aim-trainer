using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameControl : MonoBehaviour
{
    public Target targetObj;

    [SerializeField]
    private GameObject resultsPanel;

    [SerializeField]
    private GameObject finalResultsPanel;

    //[SerializeField]
    //private Text scoreText, targetsHitText, shotsFiredText, accuracyText, currentTimeText;

    [SerializeField]
    private TextMeshProUGUI finalScoreText, finalTargetsHitText, finalShotsFiredText, finalAccuracyText, finalCurrentTimeText;

    [SerializeField]
    private TextMeshProUGUI currentTimeText, targetsHitText;

    [SerializeField]
    Camera cam;

    [SerializeField]
    public int targetsAmmountInitial;

    [SerializeField]
    public GameObject playerFollowCamera;

    public int score, targetsHit, time_to_kill, kills_per_second, consecutive_hits_max, consecutive_hits_current;

    /// <summary>
    /// Total targets including missed ones
    /// </summary>
    public static int targetNumber;

    private float shotsFired;
    public float accuracy;
    public float currentTime;
    private float targetTime;
    public float singleTargetLifeTime;


    public enum gameStatusEnum
    {
        STANDBY,
        STARTED,
        FINISHED
    }
    public enum targetStatusEnum
    {
        TARGETS_DISABLED,
        TARGETS_ENABLED
    }

    public enum pauseStatusEnum
    {
        PAUSED,
        RESUMED
    }

    public gameStatusEnum gameStatus = gameStatusEnum.STANDBY;
    public targetStatusEnum targetStatus = targetStatusEnum.TARGETS_DISABLED;
    public pauseStatusEnum pauseStatus = pauseStatusEnum.RESUMED;

    // Start is called before the first frame update
    void Start()
    {
        int unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        Debug.Log("Unix timestamp: " + unixTimestamp);
        targetsHit = 0;
        targetNumber = 0;
        currentTime = 0;
        targetTime = 0;
        time_to_kill = 0;
        kills_per_second = 0;
        consecutive_hits_max = 0;

        score = 0;
        shotsFired = 0;
        targetsHit = 0;
        accuracy = 0f;

        resultsPanel.SetActive(true);
        finalResultsPanel.SetActive(false);
        //scoreText.text = "Score: " + score;
        targetsHitText.text = "Targets Hit: " + targetsHit + "/" + targetsAmmountInitial;
        //shotsFiredText.text = "Shots Fired: " + shotsFired;
        //accuracyText.text = "Accuracy: " + accuracy.ToString("N2") + " %";
        currentTimeText.text = "Time: -";
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseStatus != pauseStatusEnum.PAUSED)
        {
            if (targetNumber >= targetsAmmountInitial)
            {
                pauseStatus = pauseStatusEnum.PAUSED;
                gameStatus = gameStatusEnum.FINISHED;
            }
            PrintTime();

            if (targetTime > singleTargetLifeTime)
            {
                targetNumber++;
                targetTime = 0f;
                targetObj.Hit(new Vector3(0, 0, 0));
            }
            if (gameStatus != gameStatusEnum.FINISHED)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        } else if (gameStatus == gameStatusEnum.FINISHED)
        {
            resultsPanel.SetActive(false);
            finalResultsPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            playerFollowCamera.SetActive(false);
            PrintTime();
        }
    }

    public void ShootRay()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Target target = hit.collider.gameObject.GetComponent<Target>();

            // A target is hit
            if (target != null)
            {
                if (gameStatus == gameStatusEnum.STARTED)
                {
                    consecutive_hits_current++;
                }
                if (consecutive_hits_current > consecutive_hits_max)
                {
                    consecutive_hits_max = consecutive_hits_current;
                }
                if (gameStatus == gameStatusEnum.STANDBY)
                {
                    gameStatus = gameStatusEnum.STARTED;
                    target.EnableTarget();
                    target.Hit(hit.point);
                    targetTime = 0f;

                    // This is nessecary because it doesn't make sense
                    // to count a shot if it is fired to start the game
                    shotsFired--;
                }
                else if (gameStatus == gameStatusEnum.STARTED)
                {
                    //score += 10;
                    targetsHit++;
                    targetNumber++;

                    // If all the targets are hit
                    if (targetNumber == targetsAmmountInitial)
                    {
                        pauseStatus = pauseStatusEnum.PAUSED;
                        gameStatus = gameStatusEnum.FINISHED;
                        score += target.Hit(hit.point);
                        time_to_kill = (int)(targetTime * 1000);
                        targetTime = 0f;

                        // We add a last shot because the shot counting
                        // logic will not work after this line
                        shotsFired++;
                    }
                    else
                    {
                        score += target.Hit(hit.point);
                        time_to_kill = (int)(targetTime * 1000);
                        targetTime = 0f;
                    }
                }
            } 
            else
            { 
                consecutive_hits_current = 0;
            }
        }
        if (gameStatus == gameStatusEnum.STARTED)
        {
            shotsFired++;
        }

        PrintResults();
    }

    private void PrintResults()
    {
        //scoreText.text = "Score: " + score;
        targetsHitText.text = "Targets Hit: " + targetsHit + "/" + targetsAmmountInitial;
        //shotsFiredText.text = "Shots Fired: " + shotsFired;
        // Else at the start, 'NaN' would be printed out
        if (shotsFired > 0)
        {
            accuracy = targetsHit / shotsFired * 100f;
        }
        //accuracyText.text = "Accuracy: " + accuracy.ToString("N2") + " %";
        finalScoreText.text = "Score: " + score;
        finalTargetsHitText.text = "Targets Hit: " + targetsHit + "/" + targetsAmmountInitial;
        finalShotsFiredText.text = "Shots Fired: " + shotsFired;
        finalAccuracyText.text = "Accuracy: " + accuracy.ToString("N2") + " %";
        finalCurrentTimeText.text = "Time: -";
    }

    private void PrintTime()
    {
        if (gameStatus == gameStatusEnum.STARTED)
        {
            currentTime = currentTime + Time.deltaTime;
            targetTime = targetTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //TimeSpan time = TimeSpan.FromSeconds(targetTime);
        currentTimeText.text = "Time: " + time.ToString("mm':'ss':'fff");
        finalCurrentTimeText.text = "Time: " + time.ToString("mm':'ss':'fff");
    }
}
