using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenChiTietCongViecPanel : MonoBehaviour
{

    public Transform control;
    public Transform Background;
    public CongViecDetailPanel congviecDetailPanel;

    public void SetPos(Transform popPos)
    {
        control = popPos;
        Background.gameObject.SetActive(true);
        Color trans = new Color(0, 0, 0, 0);
        Background.GetComponent<Image>().color = trans;
    }

    public void Close()
    {
        Color trans = new Color(0, 0, 0, 0.6f);
        Background.GetComponent<Image>().color = trans;
        Background.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Press()
    {
        if (control.TryGetComponent<CongViecControl>(out var cv))
        {
            Close();
            congviecDetailPanel.gameObject.SetActive(true);
            congviecDetailPanel.TieuChiDetail(cv._data);
            Background.gameObject.SetActive(true);
        }
    }
}
