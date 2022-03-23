using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    private GameObject resultsPanel;

    [SerializeField]
    private Text scoreText, targetsHitText, shotsFiredText, accuracyText, currentTimeText;

    [SerializeField]
    Camera cam;

    [SerializeField]
    private int targetsAmmountInitial;

    public static int score, targetsHit;

    private float shotsFired;
    private float accuracy;
    private float currentTime;

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

    public gameStatusEnum gameStatus = gameStatusEnum.STANDBY;
    public targetStatusEnum targetStatus = targetStatusEnum.TARGETS_DISABLED;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;

        score = 0;
        shotsFired = 0;
        targetsHit = 0;
        accuracy = 0f;

        resultsPanel.SetActive(true);
        scoreText.text = "Score: " + score;
        targetsHitText.text = "Targets Hit: " + targetsHit + "/" + targetsAmmountInitial;
        shotsFiredText.text = "Shots Fired: " + shotsFired;
        accuracyText.text = "Accuracy: " + accuracy.ToString("N2") + " %";
        currentTimeText.text = "Time: -";
    }

    // Update is called once per frame
    void Update()
    {
        PrintTime();

        // Mouse 1 pressed
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Target target = hit.collider.gameObject.GetComponent<Target>();

                // A target is hit
                if (target != null)
                {
                    if (gameStatus == gameStatusEnum.STANDBY)
                    {
                        gameStatus = gameStatusEnum.STARTED;
                        target.EnableTarget();
                        target.Hit(hit.point);

                        // This is nessecary because it doesn't make sense
                        // to count a shot if it is fired to start the game
                        shotsFired--;
                    }
                    else if (gameStatus == gameStatusEnum.STARTED)
                    {
                        //score += 10;
                        targetsHit++;

                        // If all the targets are hit
                        if (targetsHit == targetsAmmountInitial)
                        {
                            gameStatus = gameStatusEnum.FINISHED;

                            // We add a last shot because the shot counting
                            // logic will not work after this line
                            shotsFired++;
                        } else
                        {
                            score += target.Hit(hit.point);
                        }
                    }
                }
            }
            if (gameStatus == gameStatusEnum.STARTED)
            {
                shotsFired++;
            }

            PrintResults();
        }
    }

    private void PrintResults()
    {
        scoreText.text = "Score: " + score;
        targetsHitText.text = "Targets Hit: " + targetsHit + "/" + targetsAmmountInitial;
        shotsFiredText.text = "Shots Fired: " + shotsFired;
        // Else at the start, 'NaN' would be printed out
        if (shotsFired > 0)
        {
            accuracy = targetsHit / shotsFired * 100f;
        }
        accuracyText.text = "Accuracy: " + accuracy.ToString("N2") + " %";
    }

    private void PrintTime()
    {
        if (gameStatus == gameStatusEnum.STARTED)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = "Time: " + time.ToString("mm':'ss':'fff");
    }
}
