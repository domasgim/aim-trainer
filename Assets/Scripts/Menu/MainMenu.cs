using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameBasic()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayGameAnticipation()
    {
        SceneManager.LoadScene(2);
    }
    public void PlayGameMoving()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
    }
}
