using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
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
}
