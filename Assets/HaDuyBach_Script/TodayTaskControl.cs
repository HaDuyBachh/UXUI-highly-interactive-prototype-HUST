using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TodayTaskControl : MonoBehaviour
{
    public TextMeshProUGUI content;
    public void SetValue(CongViecData _data)
    {
        content.text = _data.name + "\n" + "Mô tả: " + (_data.description != "" ? _data.description : "Không có") +
            "\n" + "Tiến độ: " + _data.targetGoal + " / " + _data.target + " " + _data.targetUnit;

        transform.GetComponent<CongViecControl>().setData(_data);
    }
}
