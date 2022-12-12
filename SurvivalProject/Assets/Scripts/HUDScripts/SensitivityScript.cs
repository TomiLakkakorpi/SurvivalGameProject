using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class SensitivityScript : MonoBehaviour
{

    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Text sensitivityTextUI;
    public CinemachineFreeLook freeLookCam;

    private void Start()
    {
        LoadValues();
    }

    public void SensitivitySlider(float sensitivity)
    {
        sensitivityTextUI.text = sensitivity.ToString("0.0");
    }

    public void SaveValuesButton()
    {
        float sensitivityValue = sensitivitySlider.value;
        PlayerPrefs.SetFloat("SensitivityValue", sensitivityValue);
    }

    public void LoadValues()
    {
        float sensitivityValue = PlayerPrefs.GetFloat("SensitivityValue");
        sensitivitySlider.value = sensitivityValue;
        freeLookCam.m_XAxis.m_MaxSpeed = (sensitivityValue + 1) * 100;
    }
}
