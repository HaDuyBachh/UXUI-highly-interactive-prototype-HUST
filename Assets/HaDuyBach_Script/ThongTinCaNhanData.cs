using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThongTinCaNhanData
{
    public string _name;
    public string _email;
    public string _phone;
    public string _password;
    public string _job;

    public ThongTinCaNhanData(string name, string email, string phone, string password, string job)
    {
        _name = name;
        _email = email;
        _password = password;
        _phone = phone;
        _job = job;
    }    
    public void SetValue(string name, string email, string phone, string password, string job)
    {
        _name = name;
        _email = email;
        _password = password;
        _phone = phone;
        _job = job;
    }    
}
