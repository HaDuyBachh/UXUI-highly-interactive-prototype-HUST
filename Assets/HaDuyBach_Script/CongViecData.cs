using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class CongViecData
{
    public string name;
    public string description;
    public float target;
    public string targetUnit;
    public float targetGoal;
    public DateTime startDate;
    public DateTime endDate;
    public DateTime completeDate;
    public float tienDoCongViec;

    public Transform body;
    public int iconId;

    /// <summary>
    /// Thêm kiểu dữ liệu nhập vào từ database
    /// </summary>
    public CongViecData(string congViecName, string congViecDescription, float congViecTarget,
        string congViecTargetUnit, DateTime startDate, DateTime endDate,DateTime completeDate, Transform congViecBody)
    {
        this.name = congViecName;
        this.description = congViecDescription;
        this.body = congViecBody;
        this.target = congViecTarget;
        this.targetUnit = congViecTargetUnit;
        this.startDate = startDate;
        this.endDate = endDate;
        this.body = congViecBody;
        this.completeDate = completeDate;
    }

    /// <summary>
    /// Thêm kiểu dữ liệu từ input
    /// </summary>
    public CongViecData(string congViecName, string congViecDescription, 
        int congViecTarget, string congViecTargetUnit,string startDate, string endDate, Transform congViecBody)
    {
        this.name = congViecName;
        this.description = congViecDescription;
        this.body = congViecBody;
        this.target = congViecTarget;
        this.targetUnit = congViecTargetUnit;
        //Debug.Log(startDate + "  " + endDate);
        if (!IsTrueValueDate(startDate,out this.startDate) || !IsTrueValueDate(endDate, out this.endDate))
        {
            Debug.Log("Sai kiểu dữ liệu");
        }
        else
        {
            this.startDate = new DateTime
                (this.startDate.Year, this.startDate.Month, this.startDate.Day, 0, 0, 0);
            this.endDate = new DateTime
                (this.endDate.Year, this.endDate.Month, this.endDate.Day, 23, 59, 59);
        }    
        
    }

    public bool IsTrueValueDate(string date, out DateTime thisDate)
    {
        return DateTime.TryParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out thisDate);
    }
    public float getPercentDone()
    {
        return targetGoal / target * 100f;
    }
    public void UpdatetargetGoal(float goal)
    {
        targetGoal = goal;
        if (targetGoal >= target)
        {
            completeDate = DateTime.Now;
        }

        this.tienDoCongViec = targetGoal / target * 100f;
    }
}
