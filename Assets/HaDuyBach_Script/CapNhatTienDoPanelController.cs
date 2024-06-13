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
    public RecentlySeen recently;
    
    private void UpdatePanel()
    {
        updateTargetGoalInput.text = cv.targetGoal.ToString();
        unit.text = "/ " + cv.target + " " + cv.targetUnit;
    }    
    public void CapNhatTienDoPress(Button button)
    {
        congViecTab = button.transform.parent.parent;
        cv = congViecTab.GetComponent<CongViecControl>()._data;
        UpdatePanel();
    } 
    public void CapNhatTienDoPress(Transform congViecTab)
    {
        this.congViecTab = congViecTab;
        cv = congViecTab.GetComponent<CongViecControl>()._data;
        UpdatePanel();
    }    

    public void Save()
    {
        cv.UpdatetargetGoal(float.Parse(updateTargetGoalInput.text));
        
        var cvcontrol = congViecTab.GetComponent<CongViecControl>();
        cvcontrol.setValueResetParent(cv);

        recently.UpdateRecently(cvcontrol);
    }  

    public void SaveWithoutParent()
    {
        cv.UpdatetargetGoal(float.Parse(updateTargetGoalInput.text));
        congViecTab.GetComponent<CongViecControl>().setValue(cv);
        if (congViecTab.TryGetComponent<TodayTaskControl>(out var todayTaskControl))
        {
            todayTaskControl.ResetValue();
        }    
    }    
}
