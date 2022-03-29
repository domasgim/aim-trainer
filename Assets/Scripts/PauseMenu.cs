using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject playerFollowCamera;

    [SerializeField]
    public GameControl gameControl;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject gameUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameControl.gameStatus != GameControl.gameStatusEnum.FINISHED)
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
        gameControl.pauseStatus = GameControl.pauseStatusEnum.RESUMED;
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
        gameControl.pauseStatus = GameControl.pauseStatusEnum.PAUSED;
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

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
    }
}
