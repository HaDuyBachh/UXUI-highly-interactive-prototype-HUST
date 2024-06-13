using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TodayChartControl : MonoBehaviour
{
    const string color = "#6F65E8";
    public PieChartControl chart;
    public TextMeshProUGUI totalTitle;
    private void Awake()
    {

    }

    private void Start()
    {
        var _data = FindObjectOfType<Database>().listOfKPI;
        

        float sum = 0f;
        foreach (var mt in _data)
        {
            sum += mt.getPercentDone() * mt.weight / 100f;
        }

        Debug.Log("KPI tổng là: " + sum);

        int kpiTotal = Mathf.CeilToInt(sum);

        chart.SetValue(kpiTotal, kpiTotal, 0, color);

        //int cnt = 0, cnt_done = 0;
        //foreach (var kpi in _data)
        //{
        //    foreach (var tc in kpi.listTieuChi)
        //    {
        //        foreach (var cv in tc.listCongViec)
        //        {
        //            Debug.Log("Thời gian công việc là: " + cv.startDate + "   " + cv.endDate);
        //            if ((cv.startDate <= DateTime.Now && DateTime.Now <= cv.endDate))
        //            {
        //                cnt+=1;
        //                if (cv.targetGoal == cv.target)
        //                {
        //                    cnt_done+=1;
        //                }
        //            }
        //        }
        //    }
        //}

        //cnt = 30;
        //cnt_done = 20;

        //totalTitle.text = cnt + "\nTotal";

        //if (cnt == 0)
        //{
        //    chart.SetValueWithNumer(100, 100, 36, ,
        //    (cnt - cnt_done), cnt_done);
        //}    
        //else
        //{
        //    chart.SetValueWithNumer((float)cnt_done / cnt * 100, (float)cnt_done / cnt * 100, 36, "#6F65E8",
        //    cnt, cnt_done);
        //}    
    }
}
