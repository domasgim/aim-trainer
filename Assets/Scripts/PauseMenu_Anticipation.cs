using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu_Anticipation : MonoBehaviour
{
    [SerializeField]
    public GameObject playerFollowCamera;

    [SerializeField]
    public GameControl_Anticipation gameControl;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject gameUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameControl.gameStatus != GameControl_Anticipation.gameStatusEnum.FINISHED)
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        Resume();
    }

    public void Resume()
    {
        gameControl.pauseStatus = GameControl_Anticipation.pauseStatusEnum.RESUMED;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerFollowCamera.SetActive(true);
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        gameControl.pauseStatus = GameControl_Anticipation.pauseStatusEnum.PAUSED;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerFollowCamera.SetActive(false);
        pauseMenuUI.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Loading menu...");
    }

    public void LoadMenuSave()
    {
        SessionData_instance sessionData_instance = new SessionData_instance();
        sessionData_instance.level_name = "Basic targets";
        sessionData_instance.score = gameControl.score;
        sessionData_instance.accuracy = gameControl.accuracy;
        if (gameControl.targetsHit != 0)
        {
            sessionData_instance.time_to_kill = gameControl.time_to_kill / gameControl.targetsHit;
        }
        else
        {
            sessionData_instance.time_to_kill = 100;
        }
        sessionData_instance.kills_per_sec = gameControl.targetsHit / gameControl.currentTime;
        sessionData_instance.targets_missed = gameControl.targetsAmmountInitial - gameControl.targetsHit;
        sessionData_instance.session_time = gameControl.currentTime;
        sessionData_instance.unix_timestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        SaveSystem.SaveSession(sessionData_instance);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
    }
}
