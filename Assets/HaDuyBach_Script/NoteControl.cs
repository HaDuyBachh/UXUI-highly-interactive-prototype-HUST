using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NoteControl : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Image icon;
    public void SetValue(string title, string color)
    {
        this.title.text = title;
        ColorUtility.TryParseHtmlString(color, out var clr);
        icon.color = clr;
    }    
}
