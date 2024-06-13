using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NotificationControl : MonoBehaviour
{
    public TextMeshProUGUI _description;
    public TextMeshProUGUI _dateTime;

    private Database database;
    private string listMucTieu;
    private int congViecCount;
    public void Awake()
    {
        database = FindObjectOfType<Database>();
    }

    private void CheckDatabase()
    {
        var dateTime = DateTime.Now;
        listMucTieu = "";
        congViecCount = 0;
        foreach(var mt in database.listOfKPI)
        {
            bool have = false;
            foreach(var tc in mt.listTieuChi)
            {
                foreach (var cv in tc.listCongViec)
                {
                    if (cv.startDate <= dateTime && dateTime <=cv.endDate)
                    {
                        have = true;
                        congViecCount++;
                    }    
                }    
            }

            if (have)
            {
                if (listMucTieu != "") listMucTieu += ", ";
                listMucTieu += mt.name;
            }
        }    
    }    

    public void OnEnable()
    {
        var dateTime = DateTime.Now;
        CheckDatabase();
        _description.text = "Bạn có " + congViecCount + " công việc cần thực hiện hôm nay" +
            (congViecCount > 0 ? "trong mục tiêu: " + listMucTieu : "");

        _dateTime.text = dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year;
    }
}
