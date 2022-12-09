using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class SettingsScript : MonoBehaviour
{

    public CinemachineFreeLook freeLookCam;

    

    private Slider sensitivitySlider = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
    


    void update()
    {
        
    }
    
    public void sliderValue()
    {
        freeLookCam.m_YAxis.Value = sensitivitySlider.value;
    }
}
