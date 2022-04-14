using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private RectTransform graphContainer;
    [SerializeField] private RectTransform labelTemplateX;
    [SerializeField] private RectTransform labelTemplateY;
    [SerializeField] private RectTransform dashTemplateX;
    [SerializeField] private RectTransform dashTemplateY;
    private List<GameObject> gameObjectList;
    private int maxVisibleValueAmmount = 5;

    private List<int> valueList_cached;
    private List<int> valueList1 = new List<int>() { 5, 3, 56, 27, 80, 46, 51, 10, 100, 100 };
    private List<int> valueList2 = new List<int>() { 80, 46, 72, 20, 30, 50, 40, 15, 6, 5 };
    private List<int> valueList3 = new List<int>() { 20, 30, 10, 20, 50, 10, 10, 50, 61, 40 };

    public void IncreaseMaxVisibleValueAmmount()
    {
        if (maxVisibleValueAmmount < valueList1.Count)
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

    public void ShowGraph1()
    {
        valueList_cached = valueList1;
        ShowGraph(valueList1);
    }

    public void ShowGraph2()
    {
        valueList_cached = valueList2;
        ShowGraph(valueList2);
    }

    public void ShowGraph3()
    {
        valueList_cached = valueList3;
        ShowGraph(valueList3);
    }

    private void Start()
    {
        gameObjectList = new List<GameObject>();
        valueList_cached = valueList1;
        //List<int> valueList = new List<int>() { 5, 3, 56, 27, 80, 46, 51, 10, 100, 100 };
        ShowGraph(valueList_cached);
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

    private void ShowGraph(List<int> valueList)
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
            int value = valueList[i];
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
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(yMin + (normalizedValue * (yMax - yMin))).ToString();
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
