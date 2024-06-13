using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KPIDashTabControl : MonoBehaviour
{
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _totalPercentDone;
    public TextMeshProUGUI _weight;
    public Image background;
    public int characterLimit = 16;
    public MucTieuData _data;
    public void SetValue(MucTieuData _data)
    {
        var name = _data.name;
        if (name.Length > characterLimit)
        {
            name = name.Substring(0, characterLimit) + "...";
        }

        var _per = _data.getPercentDone();
        this._name.text = name;
        this._totalPercentDone.text = _per + "%";
        this._data = _data;

        _weight.text = _data.weight.ToString() + "%";

        if (_per < 30)
        {
            ColorUtility.TryParseHtmlString("#FC744B", out var clr);
            background.color = clr;
        }
        else
        if (_per < 80)
        {
            ColorUtility.TryParseHtmlString("#FFD800", out var clr);
            background.color = clr;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#5EDD46", out var clr);
            background.color = clr;
        }
    }
}
