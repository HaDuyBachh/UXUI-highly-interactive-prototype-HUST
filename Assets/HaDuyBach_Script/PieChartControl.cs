using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PieChartControl : MonoBehaviour
{
    public Image fill;
    public Slider slider;
    public TextMeshProUGUI perText;
    public bool isTextChangePos = true;
    public bool stillWriteZero = false;
    private void setPerText(TextMeshProUGUI textToSet, float percent, float real_percent, float distance, string text)
    {
        var p = ((percent - real_percent) + percent) / 2 / 100 * 360;
        var posX = Mathf.Sin(p / 360 * 2 * Mathf.PI) * distance;
        var posY = Mathf.Cos(p / 360 * 2 * Mathf.PI) * distance;

        posX = -posX;
        posY = -posY;

        if ((text == "0" && !stillWriteZero) || (text == "0%" && !stillWriteZero))
        {
            textToSet.gameObject.SetActive(false);
        }    
        else
        {
            if (isTextChangePos) textToSet.transform.localPosition = new Vector3(posX, posY, 0);
            textToSet.gameObject.SetActive(true);
            textToSet.text = text;
        }

    }
    public void SetValue(float percent, float real_percent, float distance)
    {
        slider.value = percent / 100;
        setPerText(perText, percent, real_percent, distance, Mathf.CeilToInt(real_percent) + "%");
    }
    public void SetValue(float percent, float real_percent, float distance, string color)
    {
        ColorUtility.TryParseHtmlString(color, out var clr);
        fill.color = clr;
        slider.value = percent / 100;

        setPerText(perText, percent, real_percent, distance, Mathf.CeilToInt(real_percent) + "%");
    }
    public void SetValueWithNumer(float percent, float real_percent, float distance, string color, int numTotal, int numOfDone)
    {
        SetValue(percent, real_percent, distance, color);
        setPerText(perText, percent, real_percent, distance, numOfDone.ToString());

        var newPerText = Instantiate(perText);
        newPerText.transform.SetParent(this.transform);
        newPerText.transform.localScale = perText.transform.localScale;
        newPerText.transform.localRotation = perText.transform.localRotation;
        setPerText(newPerText, 100f, 100f - percent, distance, (numTotal - numOfDone).ToString());
    }
}
