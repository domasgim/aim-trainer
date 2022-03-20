using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl_V2 : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Texture2D cursorTexture;

    [SerializeField]
    private Text getReadyText;

    [SerializeField]
    private GameObject resultsPanel;

    [SerializeField]
    private Text scoreText, targetsHitText, shotsFiredText, accuracyText, currentTimeText;

    public static int score, targetsHit;

    private float shotsFired;
    private float accuracy;
    private int targetsAmmount;

    [SerializeField]
    private int targetsAmmountInitial;

    /* Timer */
    float currentTime;
    public int startMinutes;
    bool timerActive = true;

    private gameStatusEnum gameStatus = gameStatusEnum.STANDBY;
    enum gameStatusEnum
    {
        STANDBY,
        STARTED,
        FINISHED
    }

    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;

        targetsAmmount = targetsAmmountInitial;
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

    // TODO:
    // * game started
    // * 

    // Update is called once per frame
    void Update()
    {
        if (gameStatus == gameStatusEnum.STARTED)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //currentTimeText.text = "Time: " + time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
        currentTimeText.text = "Time: " + time.ToString("mm':'ss':'fff");

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
                        target.Hit();

                        // This is nessecary because it doesn't make sense
                        // to count a shot if it is fired to start the game
                        shotsFired--;
                    }
                    else if (gameStatus == gameStatusEnum.STARTED)
                    {
                        score += 10;
                        targetsHit++;
                        targetsAmmount--;

                        // If all the targets are hit
                        if (targetsHit == targetsAmmountInitial)
                        {
                            gameStatus = gameStatusEnum.FINISHED;

                            // We add a last shot because the shot counting
                            // logic will not work after this line
                            shotsFired++;
                        } else
                        {
                            target.Hit();
                        }
                    }
                }
            }
            if (gameStatus == gameStatusEnum.STARTED)
            {
                shotsFired++;
            }

            scoreText.text = "Score: " + score;
            targetsHitText.text = "Targets Hit: " + targetsHit + "/" + targetsAmmountInitial;
            shotsFiredText.text = "Shots Fired: " + shotsFired;
            accuracy = targetsHit / shotsFired * 100f;
            accuracyText.text = "Accuracy: " + accuracy.ToString("N2") + " %";
        }
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
