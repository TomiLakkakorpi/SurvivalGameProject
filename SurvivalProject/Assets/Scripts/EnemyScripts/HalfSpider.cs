using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HalfSpider : MonoBehaviour
{
    private Animator animCon;
    public static HalfSpider Instance;  

    private float health;
    private float maxHealth = 100f;

    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {
        healthBarUI.SetActive(false);
        health = 50f;
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
        if (EnemyAI.Instance.attacking == true && EnemyAI.Instance.chasing == false)
        {
            CombatIdle();
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

    private void CombatIdle()
    {
        animCon.SetTrigger("CombatIdle");
    }

    public void Attack()
    {
        //Animation without this delay is not in sync
        StartCoroutine(WaitBeforeDamage());
    }

    IEnumerator WaitBeforeDamage()
    {
        animCon.SetTrigger("Attack_01");
        yield return new WaitForSeconds(0.8f);
        PlayerStatus.Instance.TakeDamage(15);
    }

    
    float CalculateHealth()
    {
        return health / maxHealth;
    }
}