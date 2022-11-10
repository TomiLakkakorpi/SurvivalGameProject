using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerStatus : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    private float Multiplier = 0.0005f;
    [Header("Player health")]
    public Image HpBarImage;
    public float HealthMax;
    public float HealthCurrent;
    
    [Header("Player Hunger")]
    public Image HungerBarImage;
    public float HungerMax;
    public float HungerCurrent;
    public float StarvationMultiplier;

    [Header("Player Thirsty")]
    public Image ThirstyBarImage;
    public float ThirstyMax;
    public float ThirstyCurrent;
    public float ThirstyMultiplier;

   
    // Update is called once per frame
    void Update()
    {
        //reduce hunger by time, with multiplyer you
        HungerCurrent = HungerMax - Time.time * StarvationMultiplier;

        //reduce thirsty by time
        ThirstyCurrent = ThirstyMax - Time.time * ThirstyMultiplier;
        
        GetCurrentFill();

        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(float damage)
    {
        HealthCurrent -= damage;
    }

    
    public void GetCurrentFill()
    {
        //Fill amount of imagebars
        float HealthFill = HealthCurrent / HealthMax;
        HpBarImage.fillAmount = HealthFill;

        float HungerFill = HungerCurrent / HungerMax;
        HungerBarImage.fillAmount = HungerFill;

        float ThirstyFill = ThirstyCurrent / ThirstyMax;
        ThirstyBarImage.fillAmount = ThirstyFill;
        
        //if statements when hungry and thirsty then reduce health
        if(HungerFill < 0)
        {
            HealthCurrent -= Multiplier;
        }
        
        if(ThirstyFill < 0)
        {
            HealthCurrent -= Multiplier; 
        } 

        if(HealthFill < 0)
        {
            Debug.Log("Olet kuollut");
            OnPlayerDeath?.Invoke();
            Debug.Log("Stop");
            enabled = false;
            Debug.Log("Stop2");
        }
    }    
}
