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

    //References
    private Animator mAnimator;

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
        mAnimator = GetComponentInChildren<Animator>();
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(float damage)
    {
        // If player has armor, reduce damage
        if(PlayerInventory.Instance.armorEquipped)
            damage -= 5f;
        if(PlayerInventory.Instance.helmetEquipped)
            damage -= 2f;
        HealthCurrent -= damage;
        mAnimator.SetTrigger("GetHit");
    }

    public void IncreaseHealth(int value)
    {
        //HealthCurrent += value;
        HealthCurrent = Math.Min(100, HealthCurrent + value);
    }

    public void IncreaseFood(int value)
    {
        // HungerCurrent += value;
        HungerCurrent = Math.Min(100, HungerCurrent + value);
    }

    public void IncreaseDrink(int value)
    {
        ThirstyCurrent = Math.Min(100, ThirstyCurrent + value);
    }

    public void GetCurrentFill()
    {
        //Fill amount of imagebars
        HealthFill = HealthCurrent / HealthMax;
        HpBarImage.fillAmount = HealthFill;

        if (HealthFill <= 0)
        {
            //Death animation's function
            Death();

            //Disables player movement
            GetComponent<PlayerMovement>().enabled = false;

            //Freezes 3rdpersoncam under Camera
            GameObject.Find("Camera").GetComponent<ThirdPersonCam>().enabled = false;

            //Freezes 3rdpersoncam
            GameObject.Find("ThirdPersonCam").SetActive(false);

            OnPlayerDeath?.Invoke();
            //This stops whole script
            enabled = false;
        }

        if (HungerCurrent == 0)
            HealthCurrent -= HealthMultiplier;

        if (ThirstyCurrent == 0)
            HealthCurrent -= HealthMultiplier;
    }

    private void IncreaseHunger()
    {
        HungerCurrent--;
        if (HungerCurrent < 0)
            HungerCurrent = 0;

        HungerFill = HungerCurrent / HungerMax;
        HungerBarImage.fillAmount = HungerFill;
    }

    private void IncreaseThirst()
    {
        ThirstyCurrent--;
        if (ThirstyCurrent < 0)
            ThirstyCurrent = 0;

        ThirstyFill = ThirstyCurrent / ThirstyMax;
        ThirstyBarImage.fillAmount = ThirstyFill;
    }

    private void Death()
    {
        mAnimator.SetTrigger("Death");
    }
}
