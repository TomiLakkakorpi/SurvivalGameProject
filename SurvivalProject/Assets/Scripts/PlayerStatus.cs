using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Image HpBarImage;
    public float max;
    public float current;
    

    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();

        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }

    }

    public void TakeDamage(float damage)
    {
        current -= damage;
    }

    public void GetCurrentFill()
    {
        float fill = current / max;
        HpBarImage.fillAmount = fill;
        
    }
         
}
