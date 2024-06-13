using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MucTieuDetailPanel : MonoBehaviour
{
    public InputField tenMucTieu;
    public InputField moTa;
    public InputField trongSo;
    public MucTieuData data;
    public void MucTieuDetail(MucTieuData _data)
    {
        data = _data;
        tenMucTieu.text = _data.name;
        moTa.text = _data.description;
        trongSo.text = _data.weight.ToString();
    }    

    public void Save()
    {
        data.name = tenMucTieu.text;
        data.description = moTa.text;
        data.weight = int.Parse(trongSo.text);
        data.tab.GetComponent<MucTieuControl>().SetValue(data);
    }
}
