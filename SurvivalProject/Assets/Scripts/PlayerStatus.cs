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

    [Header("Player health")]
    public Image HpBarImage;
    private float HealthMax = 100;
    [SerializeField] private float HealthCurrent;
    
    [Header("Player Hunger")]
    public Image HungerBarImage;
    private float HungerMax = 100;
    [SerializeField] private float HungerCurrent;
    public float HungerRate;

    [Header("Player Thirsty")]
    public Image ThirstyBarImage;
    private float ThirstyMax = 100;
    [SerializeField] private float ThirstyCurrent;
    public float ThirstRate;

    private void Awake()
    {
        Instance = this;
    }
   
    void Start()
    {
        HealthCurrent = HealthMax;
        HungerCurrent = HungerMax;
        HungerBarImage.fillAmount = HungerCurrent;
        ThirstyCurrent = ThirstyMax;
        ThirstyBarImage.fillAmount = ThirstyCurrent;

        InvokeRepeating("IncreaseHunger", 0, HungerRate);
        InvokeRepeating("IncreaseThirst", 0, ThirstRate);
    }

    // Update is called once per frame
    void Update()
    {   
        GetCurrentFill();

        // DEBUGGING METHOD DELETE AFTER
        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(float damage)
    {
        HealthCurrent -= damage;
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

        if(HealthFill <= 0)
        {
            OnPlayerDeath?.Invoke();
            //This stops whole script
            enabled = false;
        }
    }

    private void IncreaseHunger()
    {
        HungerCurrent--;
        if (HungerCurrent < 0)
            HungerCurrent = 0;

        HungerFill = HungerCurrent / HungerMax;
        HungerBarImage.fillAmount = HungerFill;

        if (HungerCurrent == 0) 
            HealthCurrent -= HealthMultiplier;
    }

    private void IncreaseThirst()
    {
        ThirstyCurrent--;
        if (ThirstyCurrent < 0)
            ThirstyCurrent = 0;

        ThirstyFill = ThirstyCurrent / ThirstyMax;
        ThirstyBarImage.fillAmount = ThirstyFill;

        if (ThirstyCurrent == 0) 
            HealthCurrent -= HealthMultiplier;
    }

}
