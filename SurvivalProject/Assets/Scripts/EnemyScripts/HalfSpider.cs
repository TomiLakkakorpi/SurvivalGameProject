using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HalfSpider : MonoBehaviour
{
    private Animator animCon;
    public static HalfSpider Instance;  

    public bool isPlayerNearEnemy = false;
    private float nextPunchAttack;
    private float punchAttackCooldown = 0.8f;

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
        //Enemy can take damage
        if(isPlayerNearEnemy == true)
        {
            if(Input.GetMouseButtonDown(0) && Time.time > nextPunchAttack)
            {
                nextPunchAttack = Time.time + punchAttackCooldown;
                //Value of item damagestat here
                TakeDamage(10);
            }
        }

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
            health = 100f;
            healthBarUI.SetActive(false);
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

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player") 
        {
            isPlayerNearEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Player") 
        {
            isPlayerNearEnemy = false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    //Delete enemy function
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
