using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minotaur : MonoBehaviour
{
    private Animator animCon;
    public static Minotaur Instance;    

    private float health;
    private float maxHealth = 100f;

    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {
        healthBarUI.SetActive(false);
        health = 100f;
        slider.value = CalculateHealth();
        animCon = GetComponent<Animator>();
    }

      private void Awake()
    {
        Instance = this;   
    }

    void Update()
    {
        //Health
        slider.value = CalculateHealth();

        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }

        //Animations
        if (EnemyAI.Instance.patrolling == true)
        {
            Walk();
        }
        if (EnemyAI.Instance.chasing == true)
        {
            Run();
        }
        if(EnemyAI.Instance.chasing == true && EnemyAI.Instance.attacking == true)
        {
            Walk();
        }

    }

    private void Idle()
    {
        animCon.SetFloat("Speed", 0, 0.15f, Time.deltaTime);
    }

    private void Walk()
    {
        animCon.SetFloat("Speed", 0.5f, 0.15f, Time.deltaTime);
    }

    private void Run()
    {
        animCon.SetFloat("Speed", 1, 0.15f, Time.deltaTime);
    }

     float CalculateHealth()
    {
        return health / maxHealth;
    }
}
