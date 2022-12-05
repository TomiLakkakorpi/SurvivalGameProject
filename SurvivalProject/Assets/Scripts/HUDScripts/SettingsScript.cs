using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class SettingsScript : MonoBehaviour
{

    public CinemachineFreeLook freeLookCam;

    //CinemachineOrbitalTransposer xAxis;

    private Slider sensitivitySlider = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
    //private Panel sensitivityDisplay = GameObject.Find("SensitivityPanel").GetComponent<Panel>();


    void update()
    {
        //sensitivityDisplay = sensitivitySlider.value;
    }
    
    public void sliderValue()
    {
        freeLookCam.m_YAxis.Value = sensitivitySlider.value;
    }
}
