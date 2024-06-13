using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DateContainerController : MonoBehaviour
{
    const int noticeDis = 50;
    [SerializeField]
    private List<List<Transform>> week = new List<List<Transform>>();
    private bool Init = false;
    public Transform pointSign;
    public Transform noticeSign;
    /// <summary>
    /// DateTime của cái DateConTainer này chứ không phải của hệ thống
    /// </summary>
    public DateTime thisDateTime;
    public int dateCount;
    public Transform[] dateTrans = new Transform[32];
    public MultiDateContainerController dateContainer;
    public List<Transform> noticeList = new();
    public TodayTaskControl taskForClone;
    public Transform taskTable;
    public void Awake()
    {
        InitValue();
    }
    public void InitValue()
    {
        pointSign = transform.GetChild(0);
        pointSign.gameObject.SetActive(false);
        noticeSign = transform.GetChild(1);
        noticeSign.gameObject.SetActive(false);

        Init = true;
        for (int i = 2; i < transform.childCount; i++)
        {
            var h = transform.GetChild(i);
            week.Add(new List<Transform>());
            for (int j = 0; j < h.childCount; j++)
            {
                week[i - 2].Add(h.GetChild(j));
            }
        }

        dateContainer = FindObjectOfType<MultiDateContainerController>();
    }

    private string gettDate(int date)
    {
        if (date < 10) return "0" + date;
        else return "" + date;
    }

    public void offPointDate() => pointSign.gameObject.SetActive(false);

    public void ClearTaskTable()
    {
        Debug.Log("xóa bảng có số phần tử: " + taskTable.childCount);
        for (int i = 0; i < taskTable.childCount; i++)
        {
            Destroy(taskTable.GetChild(i).gameObject);
        }
    }

    public void setPointDate(DateTime _setDate)
    {
        offPointDate();
        if (thisDateTime.Year == _setDate.Year && thisDateTime.Month == _setDate.Month)
        {
            ClearTaskTable();
            pointSign.position = dateTrans[_setDate.Day].position;
            pointSign.gameObject.SetActive(true);
            foreach (var cv in dateContainer.listOfTask)
            {
                if (cv.startDate <= _setDate && _setDate <= cv.endDate)
                {
                    var task = Instantiate(taskForClone);
                    task.gameObject.SetActive(true);
                    task.transform.SetParent(taskTable);
                    task.transform.localScale = taskForClone.transform.localScale;
                    task.SetValue(cv);
                }
            }
        }
    }

    public void AddNotice(Transform trans)
    {
        noticeList.Add(trans);
    }
    public void ClearNotice()
    {
        foreach (var n in noticeList)
        {
            Destroy(n.gameObject);
        }
        noticeList.Clear();
    }

    public bool CheckNoticeDayInMonth(DateTime currentDay)
    {
        foreach (var task in dateContainer.listOfTask)
        {
            if (task.startDate <= currentDay && currentDay <= task.endDate)
            {
                Debug.Log(currentDay.Date + " co hoat dong");
                return true;
            }
        }
        return false;
    }

    public void setUpDate(DateTime _d, DateTime _curDate)
    {
        if (!Init) InitValue();
        ClearNotice();

        ///DateTime của cái DateConTainer này chứ không phải của hệ thống
        thisDateTime = _d;
        DateTime dateTime = new(_d.Year, _d.Month, 1);

        ///Dãy bỏ đầu
        for (int j = 0; j < (int)dateTime.DayOfWeek; j++)
        {
            week[0][j].gameObject.SetActive(false);
        }

        ///Dãy đầu
        dateCount = 1;
        var _dateLimit = DateTime.DaysInMonth(_d.Year, _d.Month);
        for (int j = (int)dateTime.DayOfWeek; j < week[0].Count; j++)
        {
            week[0][j].gameObject.SetActive(true);
            dateTrans[dateCount] = week[0][j].transform;

            if (CheckNoticeDayInMonth(new DateTime(_d.Year, _d.Month, dateCount, 12, 00, 00)))
            {
                var n = Instantiate(noticeSign);
                n.SetParent(noticeSign.parent);
                n.localScale = noticeSign.localScale;

                n.gameObject.SetActive(true);
                n.position = dateTrans[dateCount].position - new Vector3(0, noticeDis, 0);
                AddNotice(n);
            }

            week[0][j].GetComponent<TextMeshProUGUI>().text = gettDate(dateCount++);
        }

        ///Dãy còn lại
        for (int i = 1; i < week.Count; i++)
        {
            for (int j = 0; j < week[i].Count; j++)
            {
                if (dateCount <= _dateLimit)
                {
                    week[i][j].gameObject.SetActive(true);
                    dateTrans[dateCount] = week[i][j].transform;

                    if (CheckNoticeDayInMonth(new DateTime(_d.Year, _d.Month, dateCount, 12, 00, 00)))
                    {
                        var n = Instantiate(noticeSign);
                        n.SetParent(noticeSign.parent);
                        n.localScale = noticeSign.localScale;

                        n.gameObject.SetActive(true);
                        n.position = dateTrans[dateCount].position - new Vector3(0, noticeDis, 0);
                        AddNotice(n);
                    }

                    week[i][j].GetComponent<TextMeshProUGUI>().text = gettDate(dateCount++);
                }
                else
                {
                    week[i][j].gameObject.SetActive(false);
                }
            }
        }


        ///Hiển thị đánh dấu ngày hiện tại
        setPointDate(_curDate);
    }
}
