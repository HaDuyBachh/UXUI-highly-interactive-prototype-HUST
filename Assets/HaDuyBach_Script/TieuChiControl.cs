using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TieuChiControl : MonoBehaviour
{
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _percentDone;
    public TieuChiData _data;
    public PieChartControl chart;

    public void SetValue(TieuChiData _data)
    {
        var name = _data.name;
        if (name.Length > 14)
        {
            name = name.Substring(0, 14) + "...";
        }
        this._name.text = name;
        this._percentDone.text = (int)_data.getPercentDone() + "%";
        this._data = _data;

        chart.SetValue(_data.getPercentDone(), _data.getPercentDone(), 0);
    }

    public void SetValueResetParent(TieuChiData _data)
    {
        SetValue(_data);
        var mucTieu = transform.parent.GetComponent<MucTieuControl>();
        mucTieu._data.getPercentDone();
        mucTieu.SetValue(mucTieu._data);
    }    

}
