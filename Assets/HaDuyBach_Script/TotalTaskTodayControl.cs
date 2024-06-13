using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class TotalTaskTodayControl : MonoBehaviour
{
    public TextMeshProUGUI todayTaskCount;
    void Start()
    {
        var data = FindObjectOfType<Database>().listOfKPI;
        int sum = 0;
        foreach(var mt in data)
        {
            foreach(var tc in mt.listTieuChi)
            {
                foreach(var cv in tc.listCongViec)
                {
                    if (cv.startDate <= DateTime.Now && DateTime.Now <= cv.endDate) sum++;
                }    
            }    
        }
        todayTaskCount.text = sum.ToString();
    }
}
