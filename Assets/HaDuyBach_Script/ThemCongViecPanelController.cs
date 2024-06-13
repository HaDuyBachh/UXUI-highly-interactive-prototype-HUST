using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemCongViecPanelController : MonoBehaviour
{
    public InputField _name;
    public InputField _descript;
    public InputField _target;
    public Text _unit;
    public InputField _startDate;
    public InputField _endDate;
    public TieuChiData tieuChi;
    public Transform congviecContainer;
    public void ThemCongViecPress(Button button)
    {
        congviecContainer = button.transform.parent.parent;
        tieuChi = congviecContainer.GetComponent<TieuChiControl>()._data;
        _unit.text = congviecContainer.GetComponent<TieuChiControl>()._data.targetUnit;
        Debug.Log(congviecContainer.name);
    }

    public string getName()
    {
        return _name.text;
    }
    public string getDes()
    {
        return _descript.text;
    }
    public int getTarget()
    {
        return int.Parse(_target.text);
    }
    public string getUnit()
    {
        return _unit.text;
    }
    public string getStartDate()
    {
        return _startDate.text;
    }    
    public string getEndDate()
    {
        return _endDate.text;
    }    
    public void OnEnable()
    {
        _name.text = "";
        _descript.text = "";
        _target.text = "";
        _unit.text = "";
        _startDate.text = "";
        _endDate.text = "";
        tieuChi = null;
        congviecContainer = null;
    }
}
