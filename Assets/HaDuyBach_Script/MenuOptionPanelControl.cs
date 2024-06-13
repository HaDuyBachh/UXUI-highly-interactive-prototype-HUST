using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptionPanelControl : MonoBehaviour
{
    public Transform control;
    public TabController tabControl;
    public Transform Background;
    private Transform anchored;

    public MucTieuDetailPanel mucTieuDetailPanel;
    public TieuChiDetailPanel tieuChiDetailPanel;
    public CongViecDetailPanel congviecDetailPanel;
    public void Start()
    {
        tabControl = FindObjectOfType<TabController>();
    }
    public void Update()
    {
        this.transform.position = anchored.position;
    }

    public void Close()
    {
        Color trans = new Color(0, 0, 0, 0.6f);
        Background.GetComponent<Image>().color = trans;
        Background.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }    
    public void SetPos(Transform popPos)
    {
        anchored = popPos;
        control = popPos.transform.parent.parent;
        transform.position = anchored.position;
        Background.gameObject.SetActive(true);
        Color trans = new Color(0, 0, 0, 0);
        Background.GetComponent<Image>().color = trans;
    }

    public void DeletePress()
    {
        if (control.TryGetComponent<MucTieuControl>(out var mc))
        {
            tabControl.XoaKPI(mc);
        }
        else
        if (control.TryGetComponent<TieuChiControl>(out var tc))
        {
            tabControl.XoaTieuChi(tc);
        }
        else
        if (control.TryGetComponent<CongViecControl>(out var cv))
        {
            tabControl.XoaCongViec(cv);
        }
        Debug.Log("xóa thành công");
    }

    public void EditPress()
    {
        if (control.TryGetComponent<MucTieuControl>(out var mc))
        {
            mucTieuDetailPanel.gameObject.SetActive(true);
            mucTieuDetailPanel.MucTieuDetail(mc._data);
            Close();
            Background.gameObject.SetActive(true);
        }
        else
        if (control.TryGetComponent<TieuChiControl>(out var tc))
        {
            tieuChiDetailPanel.gameObject.SetActive(true);
            tieuChiDetailPanel.TieuChiDetail(tc._data);
            Close();
            Background.gameObject.SetActive(true);
        }
        else
        if (control.TryGetComponent<CongViecControl>(out var cv))
        {
            congviecDetailPanel.gameObject.SetActive(true);
            congviecDetailPanel.TieuChiDetail(cv._data);
            Close();
            Background.gameObject.SetActive(true);
        }
    }
}
