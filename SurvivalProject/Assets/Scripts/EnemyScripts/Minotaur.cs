using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minotaur : MonoBehaviour
{
    private Animator animCon;    
    public AudioSource source;
    public AudioClip attackSound;
    public AudioClip getHitSound;

    public bool isPlayerNearEnemy = false;
    private float nextPunchAttack;
    private float punchAttackCooldown = 0.8f;

    private float health;
    private float maxHealth = 160f;

    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {
        healthBarUI.SetActive(false);
        health = 160f;
        slider.value = CalculateHealth();
        animCon = GetComponent<Animator>();
    }

    void Update()
    {
        //Enemy can take damage
        if(isPlayerNearEnemy == true)
        {
            if(Input.GetMouseButtonDown(0) && Time.time > nextPunchAttack)
            {
                if(PlayerInventory.Instance.swordInHand == false){
                    nextPunchAttack = Time.time + punchAttackCooldown;
                    //Value of item damagestat here (This is punch)
                    TakeDamage(10);
                }
                if(PlayerInventory.Instance.swordInHand == true){
                    nextPunchAttack = Time.time + punchAttackCooldown;
                    //Value of item damagestat here (This is sword)
                    TakeDamage(50);
                }      
            }
        }

        //Health
        slider.value = CalculateHealth();

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

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player") 
        {
            healthBarUI.SetActive(true);
            isPlayerNearEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Player") 
        {
            healthBarUI.SetActive(false);
            isPlayerNearEnemy = false;
        }
    }

    public void TakeDamage(float damage)
    {
        GetHitSound();
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

    void AttackSound() 
    {
        source.clip = attackSound;
        source.Play();
    }

    void GetHitSound() 
    {
        source.clip = getHitSound;
        source.Play();
    }

}
