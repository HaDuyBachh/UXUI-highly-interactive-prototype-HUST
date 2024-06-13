using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecentlySeen : MonoBehaviour
{
    public TextMeshProUGUI _name;
    public Transform tab;
    public Transform anchor;
    public bool isDrag = false;
    
    private MucTieuData _data;


    public void Update()
    {
        if (isDrag)
        {
            //tab.transform.position += new Vector3(0,((anchor.position.y > _data.tab.position.y) ? 1 : -1) * Time.deltaTime * 600f,0);

            var v3 = tab.position + new Vector3(0, anchor.position.y - _data.tab.position.y, 0);

            var _before = _data.tab.position.y;

            tab.position = Vector3.Lerp(tab.position, v3, 6.0f * Time.deltaTime);

            Debug.Log(anchor.position.y + "    " + _data.tab.position.y + "  " + tab.position);

            if (Mathf.Abs(anchor.position.y - _data.tab.position.y) < 10f)
            {
                isDrag = false;
            } 
                
        }    
    }

    public void UpdateRecently(CongViecControl congViecTab)
    {   
        var mucTieu = congViecTab.transform.parent.parent.GetComponent<MucTieuControl>();
        _data = mucTieu._data;
        _name.text = mucTieu._data.name;
        if (!gameObject.activeSelf) gameObject.SetActive(true);
    }

    public void LoadRecentlyMucTieu()
    {
        isDrag = true;
    }    
}
