using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AimTrainer.Utils;
using TMPro;

public class Menu_Stats_StatGraph : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headingText;
    [SerializeField] private TextMeshProUGUI yLabelText;
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private RectTransform graphContainer;
    [SerializeField] private RectTransform labelTemplateX;
    [SerializeField] private RectTransform labelTemplateY;
    [SerializeField] private RectTransform dashTemplateX;
    [SerializeField] private RectTransform dashTemplateY;
    private List<GameObject> gameObjectList;

    private int maxVisibleValueAmmountLimit = 5;
    private int maxVisibleValueAmmount = 5;
    private bool listContainsFloatVals = false;
    private int gamesPlayed = 0;

    private List<float> valueList_cached;

    private List<float> scoreList = new List<float>();
    private List<float> accuracyList = new List<float>();
    private List<float> targetsHitList = new List<float>();
    private List<float> timeList = new List<float>();
    private List<float> killsPerSecList = new List<float>();
    private List<float> timeToKillList = new List<float>();



    public enum levelEnum
    {
        BASIC,
        ANTICIPATION,
        MOVING
    }

    public levelEnum levelType = levelEnum.BASIC;

    // Fill value lists with the appropirate level ones
    private void LoadSavedStats()
    {
        scoreList.Clear();
        accuracyList.Clear();
        targetsHitList.Clear();
        timeList.Clear();
        killsPerSecList.Clear();
        timeToKillList.Clear();

        gamesPlayed = 0;
        SessionData sessionData = SaveSystem.LoadSession();
        foreach (SessionData_instance instance in sessionData.session_list)
        {
            if (levelType == levelEnum.BASIC && instance.level_name == "Basic targets")
            {
                gamesPlayed++;
                scoreList.Add(instance.score);
                accuracyList.Add(instance.accuracy);
                targetsHitList.Add(LevelStats.BASIC_TARGETS_MAX - instance.targets_missed);
                timeList.Add(instance.session_time);
                killsPerSecList.Add(instance.kills_per_sec);
                timeToKillList.Add(instance.time_to_kill);
            }
            else if (levelType == levelEnum.MOVING && instance.level_name == "Moving targets")
            {
                gamesPlayed++;
                scoreList.Add(instance.score);
                accuracyList.Add(instance.accuracy);
                targetsHitList.Add(LevelStats.BASIC_TARGETS_MAX - instance.targets_missed);
                timeList.Add(instance.session_time);
                killsPerSecList.Add(instance.kills_per_sec);
                timeToKillList.Add(instance.time_to_kill);
            }
            else if (levelType == levelEnum.ANTICIPATION && instance.level_name == "Anticipation targets")
            {
                gamesPlayed++;
                scoreList.Add(instance.score);
                accuracyList.Add(instance.accuracy);
                targetsHitList.Add(LevelStats.BASIC_TARGETS_MAX - instance.targets_missed);
                timeList.Add(instance.session_time);
                killsPerSecList.Add(instance.kills_per_sec);
                timeToKillList.Add(instance.time_to_kill);
            }
        }
        scoreList.Reverse();
        accuracyList.Reverse();
        targetsHitList.Reverse();
        timeList.Reverse();
        killsPerSecList.Reverse();
        timeToKillList.Reverse();

        maxVisibleValueAmmountLimit = gamesPlayed;
    }

    public void SetLevelType(int levelTypeEnum)
    {
        levelType = (levelEnum)levelTypeEnum;
    }
    public void IncreaseMaxVisibleValueAmmount()
    {
        if (maxVisibleValueAmmount < maxVisibleValueAmmountLimit)
        {
            maxVisibleValueAmmount++;
        }
        ShowGraph(valueList_cached);  
    }
    public void DecreaseMaxVisibleValueAmmount()
    {
        if (maxVisibleValueAmmount > 1)
        {
            maxVisibleValueAmmount--;
        }
        ShowGraph(valueList_cached);
    }

    public void ShowScoreGraph()
    {
        headingText.text = "Score";
        yLabelText.text = "Score";
        listContainsFloatVals = false;
        LoadSavedStats();
        valueList_cached = scoreList;
        ShowGraph(valueList_cached);
    }

    public void ShowAccuracyGraph()
    {
        headingText.text = "Accuracy";
        yLabelText.text = "Precentage";
        listContainsFloatVals = false;
        LoadSavedStats();
        valueList_cached = accuracyList;
        ShowGraph(valueList_cached);
    }

    public void ShowTargetsHitGraph()
    {
        headingText.text = "Targets hit";
        yLabelText.text = "Target count";
        listContainsFloatVals = false;
        LoadSavedStats();
        valueList_cached = targetsHitList;
        ShowGraph(valueList_cached);
    }

    public void ShowTimeGraph()
    {
        headingText.text = "Time";
        yLabelText.text = "Seconds";
        listContainsFloatVals = false;
        LoadSavedStats();
        valueList_cached = timeList;
        ShowGraph(valueList_cached);
    }

    public void ShowKillsPerSecGraph()
    {
        headingText.text = "Kills per second";
        yLabelText.text = "Kill count";
        listContainsFloatVals = true;
        LoadSavedStats();
        valueList_cached = killsPerSecList;
        ShowGraph(valueList_cached);
    }

    public void ShowTimeToKillGraph()
    {
        headingText.text = "Time to kill";
        yLabelText.text = "Milliseconds";
        listContainsFloatVals = false;
        LoadSavedStats();
        valueList_cached = timeToKillList;
        ShowGraph(valueList_cached);
    }

    private void Start()
    {
        gameObjectList = new List<GameObject>();
        ShowScoreGraph();
    }
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<float> valueList)
    {
        // Destroy any contents of a pre-existing graph before
        // creating a new one
        foreach (GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();

        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;

        // Display last X values
        //int maxVisibleValueAmmount = 5;
        int valueListStartIndex = valueList.Count - maxVisibleValueAmmount;

        // Distance between each point on X axis
        float xSize = graphWidth / (maxVisibleValueAmmount + 1);
        float yMax = valueList[0];
        float yMin = valueList[0];

        if (valueListStartIndex < 0)
        {
            valueListStartIndex = 0;
        }

        for (int i = valueListStartIndex; i < valueList.Count; i++)
        {
            float value = valueList[i];
            if (value > yMax)
            {
                yMax = value;
            }
            if (value < yMin)
            {
                yMin = value;
            }
        }

        // Increase the max value so that it doesnt touch the very top of the graph
        yMax = yMax + ((yMax - yMin) * 0.1f);

        // Can be done with minimum value too but all of the statistics should logically
        // start from 0 in this case :^ )
        yMin = 0;
        //yMin = yMin - ((yMax - yMin) * 0.1f);

        GameObject lastCircleGameObject = null;

        int xIndex = 0;

        for (int i = valueListStartIndex; i < valueList.Count; i++)
        {
            float xPos = xSize + xIndex * xSize;

            // yMin is serving as the base 0 point
            float yPos = ((valueList[i] - yMin) / (yMax - yMin)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPos, yPos));
            gameObjectList.Add(circleGameObject);
            if (lastCircleGameObject != null)
            {
                GameObject dotConnectionGameObject = CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                gameObjectList.Add(dotConnectionGameObject);
            }
            lastCircleGameObject = circleGameObject;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPos, -7f);
            labelX.GetComponent<Text>().text = i.ToString();
            gameObjectList.Add(labelX.gameObject);

            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPos, 0);
            gameObjectList.Add(dashX.gameObject);

            xIndex++;
        }

        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            if (listContainsFloatVals) {
                labelY.GetComponent<Text>().text = (yMin + (normalizedValue * (yMax - yMin))).ToString("0.00");
            } else
            {
                labelY.GetComponent<Text>().text = Mathf.RoundToInt(yMin + (normalizedValue * (yMax - yMin))).ToString();
            }
            gameObjectList.Add(labelY.gameObject);

            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(0, normalizedValue * graphHeight);
            gameObjectList.Add(dashY.gameObject);
        }
    }
    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private GameObject CreateDotConnection(Vector2 dotPosA, Vector2 dotPosB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof (Image));
        gameObject.transform.SetParent(graphContainer, false);

        // Set color to be slightly transparent
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);

        RectTransform rectTransform = gameObject.GetComponent<RectTransform> ();

        // Calculate direction between A and B
        Vector2 dir = (dotPosB - dotPosA).normalized;

        // Calculate distance between A and B
        float distance = Vector2.Distance(dotPosA, dotPosB);

        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);

        // Place exactly in the middle between two points
        rectTransform.anchoredPosition = dotPosA + dir * distance * 0.5f;

        // Rotate towards the right direction
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));

        return gameObject;
    } 
}
