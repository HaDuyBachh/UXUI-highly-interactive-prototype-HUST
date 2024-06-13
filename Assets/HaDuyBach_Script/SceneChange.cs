using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public static void Scene_Lay_Mat_Khau()
    {
        SceneManager.LoadScene("Lấy Mật Khẩu");
    }

    public static void Scene_Dang_Ky()
    {
        SceneManager.LoadScene("Đăng Ký");
    }

    public static void Scene_Dang_Nhap_Va_Dang_Ky()
    {
        SceneManager.LoadScene("Đăng Nhập Và Đăng Ký");
    }

    public static void Scene_Dang_Nhap()
    {
        SceneManager.LoadScene("Đăng Nhập");
    }

    public static void Scene_Trang_Chu()
    {
        SceneManager.LoadScene("Trang Chủ");
    }

    public static void Scene_Dashboard()
    {
        SceneManager.LoadScene("Dashboard");
    }
    public static void Scene_LapLich()
    {
        SceneManager.LoadScene("Lập Lịch");
    }
    public static void Scene_TTCN()
    {
        SceneManager.LoadScene("Thông Tin Cá Nhân");
    }    
    public void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {

                return;
            }    
        }    
    }

    public void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
