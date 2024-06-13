using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TieuChiDashTabControl : MonoBehaviour
{
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _duration;
    public TextMeshProUGUI _percentTotal;
    public TieuChiData _data;
    public Image background;
    public int characterLimit = 14;

    public void SetValue(TieuChiData _data)
    {
        var name = _data.name;
        if (name.Length > characterLimit)
        {
            name = name.Substring(0, characterLimit) + "...";
        }
        this._name.text = name;
        this._duration.text = _data.target + " " + _data.targetUnit;
        this._data = _data;

        var _per = Mathf.Ceil(_data.getPercentDone());
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

        this._percentTotal.text = Mathf.Ceil(_per).ToString()+"%";
    }
}
