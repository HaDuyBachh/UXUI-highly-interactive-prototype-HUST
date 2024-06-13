using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System;

public class LineChart : MonoBehaviour
{
    [SerializeField]
    private Sprite circleSprite;
    private RectTransform graphContainer;
    public RectTransform lableTemplateX;
    public RectTransform lableTemplateY;
    public RectTransform dashTemplateX;
    public RectTransform dashTemplateY;

    Color color;
    Database data;

    void Awake()
    {
        graphContainer = transform.GetComponent<RectTransform>();

        //  CreateCircle(new Vector2(200, 300));


        //valueList = new List<int> { 5, 3, 4, 3, 1 , 4};
        //ColorUtility.TryParseHtmlString("#FC744B", out color);
        //ShowGraph(valueList);

    }
    private void Start()
    {
        List<int> valueList = new List<int>() { 5, 2, 5, 14, 32 };
        ShowGraph(valueList, "#6F65E8");

        data = FindObjectOfType<Database>();
    }
    // Update is called once per frame
    private void ShowGraph(List<int> valueList, string colorHex)
    {
        ColorUtility.TryParseHtmlString(colorHex, out color);
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMaximum = Mathf.Max(valueList.ToArray());
        float xSize = graphWidth / (valueList.Count - 1);

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            var cicleGameObject = CreateCircle(new Vector2(xPosition, yPosition));

            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                   cicleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = cicleGameObject;

            RectTransform lableX = Instantiate(lableTemplateX);
            lableX.SetParent(graphContainer, false);
            lableX.gameObject.SetActive(true);
            lableX.anchoredPosition = new Vector2(xPosition, -15f);
            var day = DateTime.Now.AddDays(i - valueList.Count + 1);
            lableX.GetComponent<Text>().text = day.Day + "/" + day.Month;

            RectTransform dashX = Instantiate(dashTemplateY);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, 0f);
        }


        int separatorCount = (valueList.Count - 1);
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(lableTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();

            RectTransform dashY = Instantiate(dashTemplateX);
            dashY.SetParent(graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
        }
    }
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {

        GameObject gameObject = new GameObject("Circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(3, 3);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }
    private void CreateDotConnection(Vector2 dotPosA, Vector2 dotPosB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPosB - dotPosA).normalized;
        float distance = Vector2.Distance(dotPosA, dotPosB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPosA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

}
