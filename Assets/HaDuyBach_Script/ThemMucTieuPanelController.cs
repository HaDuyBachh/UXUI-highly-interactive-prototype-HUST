using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemMucTieuPanelController : MonoBehaviour
{
    public InputField _name;
    public InputField _descript;
    public InputField _weight;

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

    public void OnEnable()
    {
        _name.text = "";
        _descript.text = "";
        _weight.text = "";
    }
}
