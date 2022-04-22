using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementButton : MonoBehaviour
{
    public GameObject achievementList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (!achievementList.active)
        {
            achievementList.SetActive(true);
        } else
        {
            achievementList.SetActive(false);
        }
    }
}
