using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieuChiData
{
    public string name;
    public string description;
    public int weight;
    public float target;
    public string targetUnit;
    public float targetGoal;
    public Transform body;
    public int iconId;
    public float tienDoTieuChi;
    public List<CongViecData> listCongViec = new();
    public TieuChiData(string tieuChiName, string tieuChiDescription, int tieuChiWeight, float tieuChitTarget, string tieuChiTargetUnit, Transform tieuChiBody, List<CongViecData> listCongViec)
    {
        this.name = tieuChiName;
        this.description = tieuChiDescription;
        this.weight = Mathf.Min(100, Mathf.Max(0, tieuChiWeight));
        this.body = tieuChiBody;
        this.target = tieuChitTarget;
        this.targetUnit = tieuChiTargetUnit;

        this.listCongViec.Clear();
        this.listCongViec.AddRange(listCongViec);
    }
    public float getPercentDone()
    {
        return getTienDoTieuChi();
    }
    public void UpdatetargetGoal(int goal)
    {
        targetGoal = goal;
    }

    public float getTienDoTieuChi()
    {
        targetGoal = 0;
        foreach (var cv in listCongViec)
        {
            targetGoal += cv.targetGoal;
        }
        tienDoTieuChi = (int)(targetGoal / target * 100f);
        return tienDoTieuChi;
    }
}
