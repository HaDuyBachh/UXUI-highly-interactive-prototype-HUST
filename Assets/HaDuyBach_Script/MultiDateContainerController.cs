using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class MultiDateContainerController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    const float cSpace = 273.5f;
    const float hSpace = 1075.75f;
    [SerializeField]
    private List<DateContainerController> child = new();
    [SerializeField]
    private List<Vector3> currentChildPos = new();
    [SerializeField]
    private List<Vector3> anchoredPos = new();
    public float swipeAccel;
    public int currentCentrel = 1;
    public TextMeshProUGUI monthYearText;
    public List<CongViecData> listOfTask = new();

    /// Tính vận tốc vuốt
    private Vector2 anchor;
    public float startTime;
    public float endTime;
    public float touchAccel;
    public bool returnCurrent = false;

    /// Kiểm soát ngày tháng
    public DateTime dt;
    public DateTime currPointDate;
    private void SetArchoredPos()
    {
        for (int i = 0; i < child.Count; i++)
        {
            anchoredPos[i] = child[i].transform.localPosition;
        }
    }
    private void UpdatePosByAnchor(Vector2 current, Vector2 anchor)
    {
        float moveSpace = (current - anchor).x / hSpace * cSpace * 1.2f;
        for (int i = 0; i < child.Count; i++)
        {
            child[i].transform.localPosition = anchoredPos[i] + new Vector3(moveSpace, 0, 0);
        }
    }
    private void Returning()
    {
        bool kt = false;
        for (int i = 0; i < child.Count; i++)
        {
            var curr = child[i].transform.localPosition;
            child[i].transform.localPosition = Vector3.Lerp(curr, currentChildPos[i], 10 * Time.deltaTime);

            if (Mathf.Abs(child[i].transform.localPosition.x - currentChildPos[i].x) < 1)
            {
                child[i].transform.localPosition = currentChildPos[i];
            }
            else
            {
                kt = true;
            }
        }

        returnCurrent = kt;
    }
    private void UpdatePosAll(Vector3 pos)
    {
        foreach (var ch in child)
        {
            ch.transform.localPosition += pos;
        }
    }
    private int LeftC(int c)
    {
        c -= 1;
        if (c < 0) c += child.Count;
        c %= child.Count;
        return c;
    }
    private int RightC(int c)
    {
        return LeftC(c + 2);
    }
    private void Swiping(float accel)
    {
        ///Kiểm tra xem đã đạt đến giới hạn chuyển động chưa
        if (-cSpace < child[currentCentrel].transform.localPosition.x &&
                      child[currentCentrel].transform.localPosition.x < cSpace)
        {
            UpdatePosAll(new Vector3(accel * 100 * Time.deltaTime, 0, 0));
        }
        else
        {
            ///Tìm vị trí con centrel hiện tại
            currentCentrel = (accel > 0) ? LeftC(currentCentrel) : RightC(currentCentrel);

            ///Đặt lại vị trí con child đang nắm giữ vị trí hiện tại
            child[currentCentrel].transform.localPosition
                = new Vector3(0, child[currentCentrel].transform.localPosition.y, child[currentCentrel].transform.localPosition.z);

            child[LeftC(currentCentrel)].transform.localPosition
                = new Vector3(-cSpace, child[LeftC(currentCentrel)].transform.localPosition.y, child[LeftC(currentCentrel)].transform.localPosition.z);

            child[RightC(currentCentrel)].transform.localPosition
                = new Vector3(cSpace, child[RightC(currentCentrel)].transform.localPosition.y, child[RightC(currentCentrel)].transform.localPosition.z);

            // Đặt lại vị trí current child
            for (int i = 0; i < child.Count; i++)
            {
                currentChildPos[i] = child[i].transform.localPosition;
            }

            ///Hủy gia tốc hiện tại
            swipeAccel = 0;

            ///SetUp lại ngày tháng
            if (accel > 0)
                dt = PreDate(dt);
            else
                dt = NextDate(dt);

            UpdateMonthYear(dt);
            UpdateNextAndPreDate();
        }
    }
    private void UpdateNextAndPreDate()
    {
        child[LeftC(currentCentrel)].setUpDate(PreDate(dt), currPointDate);
        child[RightC(currentCentrel)].setUpDate(NextDate(dt), currPointDate);
    }
    private void OffPointSignNextAndPreDate()
    {
        child[LeftC(currentCentrel)].offPointDate();
        child[RightC(currentCentrel)].offPointDate();
    }
    private void UpdateMonthYear(DateTime _d)
    {
        monthYearText.text = "Tháng " + _d.Month + ", " + _d.Year;
    }
    private DateTime NextDate(DateTime _d) => NextDate(_d.Month, _d.Year);
    private DateTime NextDate(int _month, int _year)
    {
        if (_month == 12)
        {
            _year += 1;
            _month = 1;
        }
        else
            _month += 1;

        return new DateTime(_year, _month, 1);
    }
    private DateTime PreDate(DateTime _d) => PreDate(_d.Month, _d.Year);
    private DateTime PreDate(int _month, int _year)
    {
        if (_month == 1)
        {
            _year -= 1;
            _month = 12;
        }
        else
            _month -= 1;
        return new DateTime(_year, _month, 1);
    }

    public void ChangeDateAndTime(int dateInvoke)
    {
        currPointDate = new DateTime(dt.Year, dt.Month, dateInvoke, 12, 00, 00);
        child[currentCentrel].setPointDate(currPointDate);
        OffPointSignNextAndPreDate();
    }    
    public void ChangeDateAndTime(Button invoke)
    {
        var dateInvoke = int.Parse(invoke.GetComponent<TextMeshProUGUI>().text);
        ChangeDateAndTime(dateInvoke);
    }
    public void loadDataFromDataBase()
    {
        var data = FindObjectOfType<Database>().listOfKPI;

        foreach(var mt in data)
        {
            foreach(var tc in mt.listTieuChi)
            {
                foreach(var cv in tc.listCongViec)
                {
                    listOfTask.Add(cv);
                    Debug.Log(cv.startDate + "   " + cv.endDate);
                }    
            }    
        }
    }

    private void Start()
    {
        loadDataFromDataBase();
        ///Lấy và cập nhật ngày tháng
        dt = DateTime.Now;
        currPointDate = DateTime.Now;
        UpdateMonthYear(dt);
       
        ///thêm các child vào
        for (int i = 0; i < transform.childCount; i++)
        {
            //Debug.Log(transform.GetChild(i).name);
            if (transform.GetChild(i).TryGetComponent<DateContainerController>(out var ch))
            {
                child.Add(ch);
                currentChildPos.Add(ch.transform.localPosition);
                anchoredPos.Add(ch.transform.localPosition);
            }    
        }

        ///Khởi tạo 3 bảng lịch
        child[currentCentrel].setUpDate(dt,currPointDate);
        ChangeDateAndTime(currPointDate.Day);
        UpdateNextAndPreDate();
    }
    private void Update()
    {
        if (swipeAccel != 0) Swiping(swipeAccel);
        else
        if (returnCurrent) Returning();     
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (swipeAccel != 0 || returnCurrent) return;

        returnCurrent = false;
        UpdatePosByAnchor(eventData.position,anchor);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (swipeAccel != 0 || returnCurrent) return;

        startTime = Time.time;
        anchor = eventData.position;
        SetArchoredPos();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (swipeAccel != 0 || returnCurrent) return;

        endTime = Time.time;
        touchAccel = 2*Mathf.Abs((eventData.position - anchor).x / (hSpace)) / ((endTime - startTime) * (endTime - startTime));
        //returnCurrent = true;

        if (touchAccel > 9 || (Mathf.Abs(child[currentCentrel].transform.localPosition.x) > cSpace / 2))
        {
            swipeAccel = (eventData.position - anchor).x > 0 ? 6f : -6f;
        }
        else
        {
            returnCurrent = true;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        returnCurrent = true;
    }
}
