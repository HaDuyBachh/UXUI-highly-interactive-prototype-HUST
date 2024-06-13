using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapNhatTienDoPanelController : MonoBehaviour
{
    public InputField updateTargetGoalInput;
    public Text unit;
    public Transform congViecTab;
    public CongViecData cv;
    public void CapNhatTienDoPress(Button button)
    {
        Debug.Log(button.transform.name);
        congViecTab = button.transform.parent.parent;
        cv = congViecTab.GetComponent<CongViecControl>()._data;

        updateTargetGoalInput.text = cv.targetGoal.ToString();
        unit.text = "/ " + cv.target + " " + cv.targetUnit;
    }    

    public void Save()
    {
        cv.UpdatetargetGoal(float.Parse(updateTargetGoalInput.text));
        congViecTab.GetComponent<CongViecControl>().setValueResetParent(cv);
        Debug.Log("Lưu dữ liệu thành công");


    }  
}
