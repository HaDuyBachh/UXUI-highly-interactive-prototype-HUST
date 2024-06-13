using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MucTieuControl : MonoBehaviour
{
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _totalPercentDone;
    public int characterLimit = 16;
    public MucTieuData _data;
    public PieChartControl chart;
    public void SetValue(MucTieuData _data)
    {
        var name = _data.name;
        if (name.Length> characterLimit)
        {
            name = name.Substring(0, characterLimit) + "...";
        }

        this._name.text = name;
        this._totalPercentDone.text = _data.totalPercentDone() + "%";
        this._data = _data;

        chart.SetValue(_data.getPercentDone(), _data.getPercentDone(), 0);
    }    

    public void Collapse()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.parent.GetComponent<TabController>().LoadAllUI();
    }   
    
    public void Expand()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        transform.parent.GetComponent<TabController>().LoadAllUI();
    }    
}
