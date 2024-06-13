using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CongViecControl : MonoBehaviour
{
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _percentDone;
    public CongViecData _data;
    public PieChartControl chart;
    public TextMeshProUGUI _description;

    public void setData(CongViecData _data)
    {
        this._data = _data;
        _data.body = this.transform;
    }    
    public void setValue(CongViecData _data)
    {
        var name = _data.name;
        if (name.Length > 12)
        {
            name = name.Substring(0, 12) + "...";
        }

        if (_name != null) this._name.text = name;
        if (_percentDone!=null) this._percentDone.text = ((int)Mathf.CeilToInt(_data.getPercentDone())) + "%";
        if (_data != null) this._data = _data;
        if (chart!=null) chart.SetValue(_data.getPercentDone(), _data.getPercentDone(), 0);
    }

    public void setValueResetParent(CongViecData _data)
    {
        setValue(_data);
        var tc = transform.parent.GetComponent<TieuChiControl>();
        tc._data.getPercentDone();
        tc.SetValueResetParent(tc._data);

        Debug.Log("thằng tiêu chí là: " + tc._data.target + "    " + tc._data.targetGoal);
    }    
}
