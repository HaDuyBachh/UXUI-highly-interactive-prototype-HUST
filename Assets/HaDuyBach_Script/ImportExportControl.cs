using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImportExportControl : MonoBehaviour
{
    public float timeToLoadDelta = 0.0f;
    public float timeToLoad = 2.5f;
    public List<MucTieuData> data;
    public TabController tab;
    public Transform loadingPage;
    public Transform linkPage;
    public InputField linkText;
    public Transform loadingPageBackground;

    public void Start()
    {
        tab = FindObjectOfType<TabController>();
        data = FindObjectOfType<Database>().listApped;
    }

    public void Import()
    {
        timeToLoadDelta = timeToLoad;
        ShowLoadingPage();
    }

    public void UpdateData()
    {
        foreach (var _data in data)
        {
            //Debug.Log("KPI là: " + _data.name);
            _data.tab = tab.ThemKPI(tab.transform, _data);

            foreach (var tc in _data.listTieuChi)
            {
                //Debug.Log("Tiêu Chí là: " + tc.name + "   "  + tc.getPercentDone());
                tc.body = tab.ThemTienChi(_data.tab, tc);

                foreach (var cv in tc.listCongViec)
                {
                    //Debug.Log("Công việc là: " + cv.name);
                    cv.body = tab.ThemCongViec(tc.body, cv);
                }
            }
        }
    }    

    public void ShowLoadingPage()
    {
        loadingPage.gameObject.SetActive(true);
        loadingPageBackground.gameObject.SetActive(true);
    }

    public void CloseLoadingPage()
    {
        loadingPage.gameObject.SetActive(false);
        loadingPageBackground.gameObject.SetActive(false);
    }

    public void ShowLoadLinkPage()
    { 
        linkPage.gameObject.SetActive(true);
        linkText.text = "https://docs.google.com/spreadsheets/d/1Nd-06N3HmNSE1-hEB-eRYhGqcJ3k4GNgcva9XHqSeNo/edit?usp=sharing";
    }    

    public void GetLink()
    {
        GUIUtility.systemCopyBuffer = linkText.text;
    }    

    private void Update()
    {
        if (timeToLoadDelta > 0.0f)
        {
            timeToLoadDelta -= Time.deltaTime;
            if (timeToLoadDelta <= 0)
            {
                UpdateData();
                CloseLoadingPage();
            }
        }    
    }
}
