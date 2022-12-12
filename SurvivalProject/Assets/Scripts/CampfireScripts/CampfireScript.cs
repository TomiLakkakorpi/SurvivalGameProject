using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireScript : MonoBehaviour
{
    //Defining variables etc.
    [Header("GameObject References")]
    public Transform rocks;
    public Transform logs;
    public Transform fire;
    public HUD Hud;

    [Header("Boolean variables")]
    public bool isPlayerNearCampfire = false;
    public bool areRocksPlaced = false;
    public bool areLogsPlaced = false;
    public bool isFireActive = false;

    [Header("Click Counters")]
    public int timesClickedRocks;
    public int timesClickedLogs;
    public int timesClickedFire;

    [Header("Healing variables")]
    public int HealthAmount = 1;
    public bool isHealingActive = false;

    [SerializeField] CampfireDamage accessPoint;

    void Start()
    {
        rocks.Translate(0, -10, 0);
        logs.Translate(0, -10, 0);
        fire.Translate(0, -10, 0);
    }

    void Update()
    {   
        if(isPlayerNearCampfire == true)
        {   
            if (areRocksPlaced == false)
            {
                if(Input.GetKeyDown(KeyCode.T))
                {
                    timesClickedRocks++;

                    if(timesClickedRocks == 1)
                    {
                        StartCoroutine(moveRocks());
                        moveRocks();
                    }
                }
            }
        }

        if(isPlayerNearCampfire == true)
        {
            if(areRocksPlaced == true)
            {
                if(areLogsPlaced == false)
                {
                    if(Input.GetKeyDown(KeyCode.T))
                    {
                        timesClickedLogs++;

                        if (timesClickedLogs == 1)
                        {
                            StartCoroutine(moveLogs());
                            moveLogs();
                        }
                    }
                }
            }
        }

        if(isPlayerNearCampfire)
        { 
            if(areRocksPlaced == true)
            {
                if(areLogsPlaced == true)
                {
                    if(isFireActive == false)
                    {
                        if(Input.GetKeyDown(KeyCode.T))
                        {
                            timesClickedFire++;
                            
                            if(timesClickedFire == 1)
                            {
                                StartCoroutine(moveFire());
                                moveFire();
                            }
                        }
                    }
                }
            }
        }

        if(isPlayerNearCampfire)
        {
            if(areRocksPlaced == false)
            {
                if(areLogsPlaced == false)
                {
                    Hud.OpenMessagePanel("Press -T- to place the rocks");
                }
            }
        }

        if(isPlayerNearCampfire)
        {
            if(areRocksPlaced == true)
            {
                if(areLogsPlaced == false)
                {
                    Hud.OpenMessagePanel("Press -T- to place the logs");
                }
            }
        }

        if(isPlayerNearCampfire == true && areRocksPlaced == true && areLogsPlaced == true && isFireActive == false)
        {
            if(areRocksPlaced == true)
            {
                if(areLogsPlaced == true)
                {
                    if(isFireActive == false)
                    {
                        Hud.OpenMessagePanel("Press -T- to light the campfire");
                    }
                }
            }
        }

        if(areRocksPlaced == true)
        {
            if(areLogsPlaced == true)
            {
                if(isFireActive == true)
                {
                    Hud.CloseMessagePanel();
                }
            }
        }

        if(isPlayerNearCampfire == true)
        {
            if(isFireActive == true)
            {
                if(isHealingActive == false)
                {
                    if(accessPoint.isPlayerInFire == false)
                    {
                        isHealingActive = true;
                        StartCoroutine(AddHealth());
                        AddHealth();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            isPlayerNearCampfire = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.tag == "Player")
        {
            isPlayerNearCampfire = false;
            Hud.CloseMessagePanel();
        }
    }

    IEnumerator moveRocks()
    {
        rocks.Translate(0, 10, 0);
        Hud.CloseMessagePanel();
        yield return new WaitForSeconds(0.5F);
        areRocksPlaced = true;
        timesClickedRocks = 0;
    }

    IEnumerator moveLogs()
    {
        Hud.CloseMessagePanel();
        logs.Translate(0, 10, 0);
        yield return new WaitForSeconds(0.5F);
        areLogsPlaced = true; 
        timesClickedLogs = 0;
    }

    IEnumerator moveFire()
    {
        Hud.CloseMessagePanel();
        fire.Translate(0, 10, 0);
        isFireActive = true;

        int respawnTime = Random.Range(60, 240);
        yield return new WaitForSeconds(respawnTime);

        fire.Translate(0, -10, 0);
        isFireActive = false;

        logs.Translate(0, -10, 0);
        areLogsPlaced = false;

        timesClickedFire = 0;
    }

    IEnumerator AddHealth()
    {
        PlayerStatus.Instance.IncreaseHealth(HealthAmount);
        yield return new WaitForSeconds(1);
        isHealingActive = false;
    }
}