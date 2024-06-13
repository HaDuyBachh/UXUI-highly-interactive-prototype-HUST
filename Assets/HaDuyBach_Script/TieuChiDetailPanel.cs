using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TieuChiDetailPanel : MonoBehaviour
{
    public InputField tenMucTieu;
    public InputField moTa;
    public InputField trongSo;
    public InputField chiTieu;
    public InputField donVi;

    public TieuChiData data;
    public void TieuChiDetail(TieuChiData _data)
    {
        data = _data;
        tenMucTieu.text = _data.name;
        moTa.text = _data.description;
        trongSo.text = _data.weight.ToString();
        chiTieu.text = _data.target.ToString();
        donVi.text = _data.targetUnit;
    }

    public void Save()
    {
        data.name = tenMucTieu.text;
        data.description = moTa.text;
        data.weight = int.Parse(trongSo.text);
        data.target = float.Parse(chiTieu.text);
        data.targetUnit = donVi.text;
        data.body.GetComponent<TieuChiControl>().SetValue(data);
    }
}
