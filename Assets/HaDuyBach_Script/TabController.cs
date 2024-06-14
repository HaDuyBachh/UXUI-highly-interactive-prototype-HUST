using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TabController : MonoBehaviour
{
    public Database data;

    private bool _isloadData;
    public List<RectTransform> UITabLoadList;

    public InputField filter;

    public Transform TieuChiBody;
    public Transform CongViecBody;
    public Transform KPITab;

    public Transform backgroundPopup;
    public ThemMucTieuPanelController themMucTieuPanel;
    public ThemTieuChiPanelController themTieuChiPanel;
    public ThemCongViecPanelController themCongViecPanel;

    public void UpdateAllTab()
    {
        foreach (var mt in data.listOfKPI)
        {
            foreach (var tc in mt.listTieuChi)
            {
                foreach (var cv in tc.listCongViec)
                {
                    cv.body.GetComponent<CongViecControl>().setValue(cv);
                }
                tc.getPercentDone();
                tc.body.GetComponent<TieuChiControl>().SetValue(tc);
            }
            mt.getPercentDone();
            mt.tab.GetComponent<MucTieuControl>().SetValue(mt);
        }
    }
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
    public void RemoveUI(RectTransform rect)
    {
        UITabLoadList.Remove(rect);
    }
    public void XoaKPI(MucTieuControl mc)
    {
        RemoveUI(mc.GetComponent<RectTransform>());
        Destroy(mc._data.tab.gameObject);
        data.listOfKPI.Remove(mc._data);
        LoadAllUI();
    }
    public void LuuKPI()
    {
        var newKPI = Instantiate(KPITab);
        newKPI.gameObject.SetActive(true);
        newKPI.SetParent(transform);
        newKPI.localScale = KPITab.localScale;

        var temp = new MucTieuData(
            themMucTieuPanel.getName(), themMucTieuPanel.getDes(), themMucTieuPanel.getWeight(), newKPI, 0, new List<TieuChiData> { });

        Debug.Log(themMucTieuPanel.getName() + "  " + themMucTieuPanel.getDes() + " " + themMucTieuPanel.getWeight());

        newKPI.GetComponent<MucTieuControl>().SetValue(temp);

        data.listOfKPI.Add(temp);

        ImportUI(newKPI.GetComponent<RectTransform>());
        LoadAllUI();
    }
    public Transform ThemKPI(Transform trans, MucTieuData _data)
    {
        var newKPI = Instantiate(KPITab);
        newKPI.gameObject.SetActive(true);
        newKPI.SetParent(trans);
        newKPI.localScale = KPITab.localScale;
        newKPI.GetComponent<MucTieuControl>().SetValue(_data);

        ImportUI(newKPI.GetComponent<RectTransform>());
        LoadAllUI();

        return newKPI.transform;
    }
    public void XoaTieuChi(TieuChiControl tc)
    {
        RemoveUI(tc.GetComponent<RectTransform>());
        tc.transform.parent.GetComponent<MucTieuControl>()._data.listTieuChi.Remove(tc._data);
        Destroy(tc._data.body.gameObject);
        UpdateAllTab();
        StartCoroutine(LoadAllUIWith(0.05f));
    }

    public IEnumerator LoadAllUIWith(float time)
    {
        yield return new WaitForSeconds(time);
        LoadAllUI();
    }    

    public void ThemTieuChi(Button button)
    {
        var newTieuChi = Instantiate(TieuChiBody);
        newTieuChi.gameObject.SetActive(true);
        newTieuChi.SetParent(button.transform.parent.parent);
        newTieuChi.localScale = TieuChiBody.localScale;
        ImportUI(newTieuChi.GetComponent<RectTransform>());
        LoadAllUI();
    }
    public void LuuTieuChi()
    {
        var newTieuChi = Instantiate(TieuChiBody);
        newTieuChi.gameObject.SetActive(true);
        newTieuChi.SetParent(themTieuChiPanel.tieuChiContainer);
        newTieuChi.localScale = TieuChiBody.localScale;

        var temp = new TieuChiData(
            themTieuChiPanel.getName(), themTieuChiPanel.getDes(), themTieuChiPanel.getWeight(),
            themTieuChiPanel.getTarget(), themTieuChiPanel.getUnit(), newTieuChi, new List<CongViecData> { });

        Debug.Log(themTieuChiPanel.getName() + "  " + themTieuChiPanel.getDes() + " " + themTieuChiPanel.getWeight());

        newTieuChi.GetComponent<TieuChiControl>().SetValue(temp);

        themTieuChiPanel.kpi.listTieuChi.Add(temp);

        ImportUI(newTieuChi.GetComponent<RectTransform>());
        LoadAllUI();
    }
    public Transform ThemTienChi(Transform trans, TieuChiData tc)
    {
        var newTieuChi = Instantiate(TieuChiBody);
        newTieuChi.gameObject.SetActive(true);
        newTieuChi.SetParent(trans);
        newTieuChi.localScale = TieuChiBody.localScale;
        newTieuChi.GetComponent<TieuChiControl>().SetValue(tc);

        ImportUI(newTieuChi.GetComponent<RectTransform>());
        LoadAllUI();

        return newTieuChi.transform;
    }
    public void XoaCongViec(CongViecControl cv)
    {
        RemoveUI(cv.GetComponent<RectTransform>());
        cv.transform.parent.GetComponent<TieuChiControl>()._data.listCongViec.Remove(cv._data);
        Destroy(cv._data.body.gameObject);
        UpdateAllTab();
        LoadAllUI();
    }
    public void LuuCongViec()
    {
        var newCongViec = Instantiate(CongViecBody);
        newCongViec.gameObject.SetActive(true);
        newCongViec.SetParent(themCongViecPanel.congviecContainer);
        newCongViec.localScale = CongViecBody.localScale;

        var temp = new CongViecData(
            themCongViecPanel.getName(), themCongViecPanel.getDes(),
            themCongViecPanel.getTarget(), themCongViecPanel.getUnit(), themCongViecPanel.getStartDate(), themCongViecPanel.getEndDate(), newCongViec);

        newCongViec.GetComponent<CongViecControl>().setValue(temp);

        themCongViecPanel.tieuChi.listCongViec.Add(temp);

        Debug.Log(themCongViecPanel.getName() + "  " + themCongViecPanel.getDes() + " " + themCongViecPanel.tieuChi.name);

        ImportUI(newCongViec.GetComponent<RectTransform>());
        LoadAllUI();
    }
    public void ThemCongViec(Button button)
    {
        var newCongViec = Instantiate(CongViecBody);
        newCongViec.gameObject.SetActive(true);
        newCongViec.SetParent(button.transform.parent.parent);
        newCongViec.localScale = CongViecBody.localScale;
        LoadAllUI();
    }
    public Transform ThemCongViec(Transform trans, CongViecData cv)
    {
        var newCongViec = Instantiate(CongViecBody);
        newCongViec.gameObject.SetActive(true);
        newCongViec.SetParent(trans);
        newCongViec.localScale = CongViecBody.localScale;
        newCongViec.GetComponent<CongViecControl>().setValue(cv);

        LoadAllUI();

        return newCongViec.transform;
    }
    public void loadDataFromDataBase()
    {
        foreach (var _data in data.listOfKPI)
        {
            //Debug.Log("KPI là: " + _data.name);
            _data.tab = ThemKPI(transform, _data);

            foreach (var tc in _data.listTieuChi)
            {
                //Debug.Log("Tiêu Chí là: " + tc.name + "   "  + tc.getPercentDone());
                tc.body = ThemTienChi(_data.tab, tc);

                foreach (var cv in tc.listCongViec)
                {
                    //Debug.Log("Công việc là: " + cv.name);
                    cv.body = ThemCongViec(tc.body, cv);
                }
            }
        }
    }
    void Start()
    {
        ImportUI(transform.GetComponent<RectTransform>());
        LoadAllUI();
        data = FindObjectOfType<Database>();
    }
    private void Update()
    {
        if (!_isloadData)
        {
            _isloadData = true;
            loadDataFromDataBase();
        }
    }
    public void FilterSubmit()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            var mt = transform.GetChild(i).GetComponent<MucTieuControl>();
            transform.GetChild(i).gameObject.SetActive(mt._data.name.ToLower().Contains(filter.text.ToLower()));
        }
        LoadAllUI();
    }
}
