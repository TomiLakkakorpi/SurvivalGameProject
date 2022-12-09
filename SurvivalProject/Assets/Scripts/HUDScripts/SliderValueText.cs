using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class SliderValueText : MonoBehaviour
{
    private Slider slider;
    private Text textComp;
    public CinemachineFreeLook freeLookCam;

    void Awake()
    {
        slider = GetComponentInParent<Slider>();
        textComp = GetComponent<Text>();
    }

    void Start()
    {
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
        
        
    }

    public void UpdateText(float val)
    {
        textComp.text = slider.value.ToString();
        freeLookCam.m_YAxis.m_MaxSpeed = slider.value;
        freeLookCam.m_XAxis.m_MaxSpeed = (slider.value + 1) * 100;
        
    }
}
