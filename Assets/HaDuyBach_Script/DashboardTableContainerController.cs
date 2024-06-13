using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DashboardTableContainerController : MonoBehaviour
{
    public bool _isloadData = false;

    public Transform KPITable;
    public Transform TieuChiTable;
    
    public Transform TieuChiTitle;
    public Transform TieuChiTopTile;

    public Transform connectIcon;
    
    public Transform kpiTabForClone;
    public Transform tieuChiForClone;
    
    public List<RectTransform> UITabLoadList;
    public Database data;
    public void LoadUI(RectTransform rect)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);

    }
    public void LoadAllUI()
    {
        foreach (var ui in UITabLoadList)
        {
            LoadUI(ui);
        }
    }
    public void ImportUI(RectTransform rect)
    {
        UITabLoadList.Add(rect);
    }
    public Transform ThemKPI(Transform trans, MucTieuData _data)
    {
        var newKPI = Instantiate(kpiTabForClone);
        newKPI.gameObject.SetActive(true);
        newKPI.SetParent(trans);
        newKPI.localScale = kpiTabForClone.localScale;
        newKPI.GetComponent<KPIDashTabControl>().SetValue(_data);

        ImportUI(newKPI.GetComponent<RectTransform>());
        LoadAllUI();

        return newKPI.transform;
    }
    public Transform ThemTieuChi(Transform trans, TieuChiData _data)
    {
        var newTieuChi = Instantiate(tieuChiForClone);
        newTieuChi.gameObject.SetActive(true);
        newTieuChi.SetParent(trans);
        newTieuChi.localScale = tieuChiForClone.localScale;
        newTieuChi.GetComponent<TieuChiDashTabControl>().SetValue(_data);

        ImportUI(newTieuChi.GetComponent<RectTransform>());
        LoadAllUI();

        return newTieuChi.transform;
    }
    public void ClearKPITable()
    {
        for(int i = 1; i<KPITable.childCount; i++)
        {
            Destroy(KPITable.GetChild(i).gameObject);
        }    
    }
    public void ClearTieuChiTable()
    {
        for (int i = 1; i < TieuChiTable.childCount; i++)
        {
            Destroy(TieuChiTable.GetChild(i).gameObject);
        }
    }
    public void LoadDataFromDataBase()
    {
        foreach (var _data in data.listOfKPI)
        {
            Debug.Log("KPI là: " + _data.name);
            ThemKPI(KPITable,_data);
        }
    }
    public void EnterTieuChiOfKPI(Button button)
    {
        KPITable.gameObject.SetActive(false);
        TieuChiTable.gameObject.SetActive(true);
        connectIcon.gameObject.SetActive(true);
        TieuChiTitle.gameObject.SetActive(true);
        TieuChiTopTile.gameObject.SetActive(true);

        var KPI = button.GetComponent<KPIDashTabControl>()._data;
        TieuChiTitle.GetComponent<TextMeshProUGUI>().text = KPI.name;

        foreach (var tc in KPI.listTieuChi)
        {
            ThemTieuChi(TieuChiTable, tc);
        }
    }
    public void BackToKPITable()
    {
        KPITable.gameObject.SetActive(true);
        TieuChiTable.gameObject.SetActive(false);
        TieuChiTitle.gameObject.SetActive(false);
        TieuChiTopTile.gameObject.SetActive(false);
        connectIcon.gameObject.SetActive(false);
        ClearTieuChiTable();
    }
    void Start()
    {
        ImportUI(GetComponent<RectTransform>());
        LoadAllUI();
        data = FindObjectOfType<Database>();
    }
    private void Update()
    {
        if (!_isloadData)
        {
            _isloadData = true;
            LoadDataFromDataBase();
        }
    }
}
