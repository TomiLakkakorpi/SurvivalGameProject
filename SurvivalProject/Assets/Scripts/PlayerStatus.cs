using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStatus : MonoBehaviour
{
    
    [Header("Player Healt")]
    public Image HpBarImage;
    public float HealtMax;
    public float HealtCurrent;
    
    [Header("Player Hunger")]
    public Image HungerBarImage;
    public float HungerMax;
    public float HungerCurrent;

    [Header("Player Thirsty")]
    public Image ThirstyBarImage;
    public float ThirstyMax;
    public float ThirstyCurrent;

    private float multiplier = 0.0005f;

    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        //reduce hunger by time
        HungerCurrent = HungerMax - Time.time;

        //reduce thirsty by time
        ThirstyCurrent = ThirstyMax - Time.time;
        
        GetCurrentFill();

        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(float damage)
    {
        HealtCurrent -= damage;
    }

    
    public void GetCurrentFill()
    {
        //Fill amount of imagebars
        float HealtFill = HealtCurrent / HealtMax;
        HpBarImage.fillAmount = HealtFill;

        float HungerFill = HungerCurrent / HungerMax;
        HungerBarImage.fillAmount = HungerFill;

        float ThirstyFill = ThirstyCurrent / ThirstyMax;
        ThirstyBarImage.fillAmount = ThirstyFill;
        
        //if statements when hungry and thirsty then reduce healt
        if(HungerFill < 0)
        {
            HealtCurrent -= multiplier;
        }
        
        if(ThirstyFill < 0)
        {
            HealtCurrent -= multiplier;
        }
    }    
}
