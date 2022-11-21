using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops : MonoBehaviour
{
    private Animator animCon;
    public static Cyclops Instance;    

    void Start()
    {
        animCon = GetComponent<Animator>();
    }

      private void Awake()
    {
        Instance = this;   
    }

    void Update()
    {
        if (EnemyAI.Instance.patrolling == true)
        {
            Walk();
        }
        if (EnemyAI.Instance.chasing == true)
        {
            Run();
        }
        // if(EnemyAI.Instance.chasing == true && EnemyAI.Instance.attacking == true)
        // {
        //     Walk();
        // }
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

}
