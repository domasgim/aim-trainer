using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;
using Random = System.Random;

public class GameControl_Anticipation : MonoBehaviour
{
    [SerializeField]
    private GameObject resultsPanel;

    [SerializeField]
    private Text scoreText, targetsHitText, shotsFiredText, accuracyText, currentTimeText;

    [SerializeField]
    Camera cam;

    [SerializeField]
    private int targetsAmmountInitial;

    public PathFollower followerPrefab;
    public Transform spawnPoint;
    public PathCreator[] pathPrefabs;

    public static int score, targetsHit;

    private float shotsFired;
    private float accuracy;
    public float currentTime;

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

    public enum targetMoveStatusEnum
    {
        TARGET_START,
        TARGET_STOP
    }

    public gameStatusEnum gameStatus = gameStatusEnum.STANDBY;
    public targetStatusEnum targetStatus = targetStatusEnum.TARGETS_DISABLED;
    public targetMoveStatusEnum targetMoveStatus = targetMoveStatusEnum.TARGET_STOP;

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
                Target_Anticipation target = hit.collider.gameObject.GetComponent<Target_Anticipation>();

                // A target is hit
                if (target != null)
                {
                    if (gameStatus == gameStatusEnum.STANDBY)
                    {
                        gameStatus = gameStatusEnum.STARTED;
                        target.Hit(hit.point);
                        SpawnRandomPath();
                    }
                    else if (gameStatus == gameStatusEnum.STARTED)
                    {
                        int hitReturn = 0;
                        if ((hitReturn = target.Hit(hit.point)) != 0)
                        {
                            if (targetsHit < targetsAmmountInitial)
                            {
                                score += hitReturn;
                                targetsHit++;
                                if (targetsHit == targetsAmmountInitial)
                                {
                                    Debug.Log("VISKAS");
                                    gameStatus = gameStatusEnum.FINISHED;
                                    targetStatus = targetStatusEnum.TARGETS_DISABLED;
                                    target.DisableTarget();
                                } else
                                {
                                    SpawnRandomPath();
                                }
                            } 
                        }
                    }
                }
            }
            if (targetStatus == targetStatusEnum.TARGETS_ENABLED)
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
        if (targetStatus == targetStatusEnum.TARGETS_ENABLED)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = "Time: " + time.ToString("mm':'ss':'fff");
    }

    public void SpawnRandomPath()
    {
        DestroyClone();
        Random random = new Random();
        int index = random.Next(0, pathPrefabs.Length);
        PathCreator p = pathPrefabs[index];
        var path = Instantiate(p, spawnPoint.position, spawnPoint.rotation);
        var follower = Instantiate(followerPrefab);
        //followerPrefab.pathCreator = path;
        follower.pathCreator = path;
        path.transform.parent = spawnPoint;
    }

    public void DestroyClone()
    {
        string name = "Follower_target(Clone)";
        GameObject go = GameObject.Find(name);
        if (go)
        {
            Destroy(go.gameObject);
        }
    }
}
