using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    private float HealthFill;
    private float HungerFill;
    private float ThirstyFill;
    public static event Action OnPlayerDeath;
    public float HealthMultiplier = 0.0005f;
    private bool isInventoryOpen = false;

    //References
    private Animator mAnimator;

    [Header("Player health")]
    public Image HpBarImage;
    private float HealthMax = 100;
    private float HealthCurrent;
    
    [Header("Player Hunger")]
    public Image HungerBarImage;
    private float HungerMax = 100;
    private float HungerCurrent;
    public float StarvationMultiplier;

    [Header("Player Thirsty")]
    public Image ThirstyBarImage;
    private float ThirstyMax = 100;
    private float ThirstyCurrent;
    public float ThirstyMultiplier;

    private void Awake()
    {
        Instance = this;
    }
   
    void Start()
    {
        mAnimator = GetComponentInChildren<Animator>();
        HealthCurrent = HealthMax;
        HungerCurrent = HungerMax;
        ThirstyCurrent = ThirstyMax;
    }

    // Update is called once per frame
    void Update()
    {
        //reduce hunger by time, with multiplyer
        HungerCurrent = HungerMax - Time.timeSinceLevelLoad * StarvationMultiplier;

        //reduce thirsty by time
        ThirstyCurrent = ThirstyMax - Time.timeSinceLevelLoad * ThirstyMultiplier;
        
        GetCurrentFill();

        // DEBUGGING METHOD DELETE AFTER
        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            if(!isInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isInventoryOpen = true;
            }
            else if(isInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isInventoryOpen = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        HealthCurrent -= damage;
        mAnimator.SetTrigger("GetHit");
    }

    public void IncreaseHealth(int value)
    {
        HealthCurrent += value;
    }

     public void IncreaseFood(int value)
    {
        HungerCurrent += value;
    }

     public void IncreaseDrink(int value)
    {
        ThirstyCurrent += value;
    }

    
    public void GetCurrentFill()
    {
        //Fill amount of imagebars
        HealthFill = HealthCurrent / HealthMax;
        HpBarImage.fillAmount = HealthFill;

        HungerFill = HungerCurrent / HungerMax;
        HungerBarImage.fillAmount = HungerFill;

        ThirstyFill = ThirstyCurrent / ThirstyMax;
        ThirstyBarImage.fillAmount = ThirstyFill;
        
        //if statements when hungry and thirsty then reduce health
        if(HungerFill < 0)
        {
            HealthCurrent -= HealthMultiplier;
        }
        
        if(ThirstyFill < 0)
        {
            HealthCurrent -= HealthMultiplier; 
        } 

        if(HealthFill <= 0)
        {
            OnPlayerDeath?.Invoke();
            //This stops whole script
            enabled = false;
        }
    }

}
