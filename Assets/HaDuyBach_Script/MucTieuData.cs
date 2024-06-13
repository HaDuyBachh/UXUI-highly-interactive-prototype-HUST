using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class dành cho một đối tượng KPI
/// </summary>
public class MucTieuData
{
    public string name;
    public string description;
    public int weight;
    public float tienDoMucTieu;
    public Transform tab;
    public int iconId;
    public List<TieuChiData> listTieuChi = new();
    public int totalPercentDone()
    {
        return 0;
    }
    public MucTieuData(string kpiName, string kpiDescription, int kpiWeight, Transform tab, int iconId, List<TieuChiData> ListTieuChi)
    {
        this.name = kpiName;
        this.description = kpiDescription;
        this.weight = Mathf.Min(100,Mathf.Max(0,kpiWeight));

        this.tab = tab;
        this.iconId = iconId;

        this.listTieuChi.Clear();
        this.listTieuChi.AddRange(ListTieuChi);
    }

    public float getPercentDone()
    {
        float sum = 0;
        foreach (var tc in listTieuChi)
        {
            sum += tc.getPercentDone() * tc.weight / 100f;
        }
        this.tienDoMucTieu = Mathf.Ceil(sum);
        return tienDoMucTieu;
    }
}
