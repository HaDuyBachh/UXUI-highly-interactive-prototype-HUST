using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThongTinCaNhanControl : MonoBehaviour
{
    public InputField _name;
    public InputField _email;
    public InputField _phone;
    public InputField _password;
    public InputField _job;
    public ThongTinCaNhanData _data;

    private void Start()
    {
        _data = FindObjectOfType<Database>().thongTinCaNhan;
        if (_data == null) 
            Debug.Log("No Database");
        else 
            SetValue(_data);
    }

    public void SetValue(ThongTinCaNhanData tt)
    {
        _name.text = tt._name;
        _email.text = tt._email;
        _phone.text = tt._phone;
        _password.text = tt._password;
        _job.text = tt._job;
    }    

    public void Save()
    {
        FindObjectOfType<Database>().thongTinCaNhan = new ThongTinCaNhanData(_name.text, _email.text, _phone.text, _password.text, _job.text);
    }    
}
