using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class CongViecDetailPanel : MonoBehaviour
{
    public InputField tenMucTieu;
    public InputField moTa;
    public InputField chiTieu;
    public InputField donVi;
    public InputField ngayBatDau;
    public InputField ngayKetThuc;

    public CongViecData data;
    public void TieuChiDetail(CongViecData _data)
    {
        data = _data;
        tenMucTieu.text = _data.name;
        moTa.text = _data.description;
        chiTieu.text = _data.target.ToString();
        donVi.text = _data.targetUnit;
        ngayBatDau.text = _data.startDate.ToString("d/M/yyyy");
        ngayKetThuc.text = _data.endDate.ToString("d/M/yyyy");
    }

    public void Save()
    {
        data.name = tenMucTieu.text;
        data.description = moTa.text;
        data.target = float.Parse(chiTieu.text);
        data.targetUnit = donVi.text;
        data.startDate = DateTime.ParseExact(ngayBatDau.text, "d/M/yyyy", CultureInfo.InvariantCulture);
        data.endDate = DateTime.ParseExact(ngayKetThuc.text, "d/M/yyyy", CultureInfo.InvariantCulture);
        data.body.GetComponent<CongViecControl>().setValue(data);
    }
}
