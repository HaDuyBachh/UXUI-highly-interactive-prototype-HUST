using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemTieuChiPanelController : MonoBehaviour
{
    public InputField _name;
    public InputField _descript;
    public InputField _weight;
    public InputField _target;
    public InputField _unit;
    public MucTieuData kpi;
    public Transform tieuChiContainer;
    public void ThemTieuChiPress(Button button)
    {
        tieuChiContainer = button.transform.parent.parent;
        kpi = tieuChiContainer.GetComponent<MucTieuControl>()._data;
        Debug.Log(tieuChiContainer.name);
    }    

    public string getName()
    {
        return _name.text;
    }
    public string getDes()
    {
        return _descript.text;
    }
    public int getWeight()
    {
        return int.Parse(_weight.text);
    }
    public int getTarget()
    {
        return int.Parse(_target.text);
    }    
    public string getUnit()
    {
        return _unit.text;
    }    
    public void OnEnable()
    {
        _name.text = "";
        _descript.text = "";
        _weight.text = "";
        _target.text = "";
        _unit.text = "";
        kpi = null;
        tieuChiContainer = null;
    }
}
