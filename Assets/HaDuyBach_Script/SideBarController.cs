using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideBarController : MonoBehaviour
{
    public GameObject SiderBarBackground;
    public GameObject SideBar;
    public float posX;
    public bool _isOpenning = false;
    public bool _isOpenned = false;
    public bool _isClosing = false;
    public bool _isClosed = true;
    public void OpenSideBar()
    {
        _isOpenning = true;
        _isClosed = false;
        SiderBarBackground.gameObject.SetActive(true);
        posX = SideBar.transform.position.x;
    }
    public void OpenProcess()
    {
        var pos = SideBar.GetComponent<RectTransform>().position;
        SideBar.GetComponent<RectTransform>().position =
            Vector3.Lerp(pos, new Vector3(0, pos.y, pos.z), 6 * Time.deltaTime);

        var Color = SiderBarBackground.GetComponent<Image>().color;
        Color.a = Mathf.Lerp(Color.a, 0.5f, 5f* Time.deltaTime);
        SiderBarBackground.GetComponent<Image>().color = Color;


        if (SideBar.GetComponent<RectTransform>().position.x >= 0)
        {
            SideBar.GetComponent<RectTransform>().position =
                new Vector3(0, SideBar.GetComponent<RectTransform>().position.y, SideBar.GetComponent<RectTransform>().position.z);
            _isOpenned = true;
            _isOpenning = false;
        }
    }    
    public void CloseSideBar()
    {
        _isClosing = true;
        _isOpenning = false;
        _isOpenned = false;
    }    
    public void CloseProcess()
    {
        var pos = SideBar.GetComponent<RectTransform>().position;
        SideBar.GetComponent<RectTransform>().position =
            Vector3.Lerp(pos, new Vector3(posX, pos.y, pos.z), 6 * Time.deltaTime);

        var Color = SiderBarBackground.GetComponent<Image>().color;
        Color.a = Mathf.Lerp(Color.a, 0, 5f* Time.deltaTime);
        SiderBarBackground.GetComponent<Image>().color = Color; 

        if (SideBar.GetComponent<RectTransform>().position.x <= posX+30)
        {
            SideBar.GetComponent<RectTransform>().position =
               new Vector3(posX, SideBar.GetComponent<RectTransform>().position.y, SideBar.GetComponent<RectTransform>().position.z);
            SiderBarBackground.gameObject.SetActive(false);
            _isClosed = true;
            _isClosing = false;
        }
    }    
    public void Update()
    {
        if (_isOpenning && !_isOpenned) OpenProcess();
        if (_isClosing && !_isClosed) CloseProcess();
    }
}
