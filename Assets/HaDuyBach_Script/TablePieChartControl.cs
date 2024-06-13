using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TablePieChartControl : MonoBehaviour
{
    public Transform chartForClone;
    public Transform pieChartTable;
    public Transform noteForClone;
    public Transform noteTable;
    private Database _data;
    public TextMeshProUGUI noDataTitle;
    private readonly string[] color_arr =
    {
        "#FC744B", "#6F65E8", "#5EDD46", "#FFD800", "#FC62FF", "#FFA740", "#FF5D84", "#91F8FF", "7F00FF"
    };

    public void ThemKPIChart(string name, float per, float real_per, float distance, string color)
    {
        var newPieChart = Instantiate(chartForClone);
        newPieChart.gameObject.SetActive(true);
        newPieChart.SetParent(pieChartTable);
        newPieChart.localScale = pieChartTable.localScale;
        newPieChart.GetComponent<PieChartControl>().SetValue(per, real_per, distance, color);

        Debug.Log("Đang chạy ở đây ");

        var newNote = Instantiate(noteForClone);
        newNote.gameObject.SetActive(true);
        newNote.SetParent(noteTable);
        newNote.localScale = noteForClone.localScale;
        newNote.GetComponent<NoteControl>().SetValue(name, color);
    }
    private void Start()
    {
        _data = FindObjectOfType<Database>();

        float sum = 0f;
        foreach (var mt in _data.listOfKPI)
        {
            //Debug.Log(mt.name);
            sum += mt.getPercentDone() * mt.weight / 100f;
        }

        int c = 0;
        int SumPer = 0; ;
        foreach (var mt in _data.listOfKPI)
        {
            SumPer += Mathf.CeilToInt(mt.getPercentDone() * mt.weight / sum);
        }

        //Debug.Log("Tổng phần trăm là: " + SumPer);

        if (SumPer <= 0)
        {
            noDataTitle.gameObject.SetActive(true);
        } 
        else
        {
            noDataTitle.gameObject.SetActive(false);
        }    
            

        foreach (var mt in _data.listOfKPI)
        {
            var per = Mathf.CeilToInt(mt.getPercentDone() * mt.weight / sum);
            ThemKPIChart(mt.name, SumPer, per, 35.5f, color_arr[c++]);
            //Debug.Log("Phần trăm hiện tại là: " + per);
            SumPer -= per;
        }
    }
}
