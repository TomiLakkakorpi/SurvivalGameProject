using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireScript : MonoBehaviour
{
    public Transform rocks;
    public Transform logs;
    public Transform fire;

    public HUD Hud;

    public bool isPlayerNearCampfire = false;

    public bool areRocksPlaced = false;
    public bool areLogsPlaced = false;
    public bool isFireActive = false;

    public int timesClickedRocks;
    public int timesClickedLogs;
    public int timesClickedFire;

    void Start()
    {
        rocks.Translate(0, -10, 0);
        logs.Translate(0, -10, 0);
        fire.Translate(0, -10, 0);
    }

    void Update()
    {      
        if(isPlayerNearCampfire == true && areRocksPlaced == false && Input.GetKeyDown(KeyCode.T) /* add check here to see if player has 9 rocks in hotbar */ )
        {
            timesClickedRocks++;

            if(timesClickedRocks == 1)
            {
                StartCoroutine(moveRocks());
                moveRocks();
            }
        }

        if(isPlayerNearCampfire == true && areRocksPlaced == true && areLogsPlaced == false && Input.GetKeyDown(KeyCode.T) /* add check here to see if player has 3 logs in hotbar */)
        {
            timesClickedLogs++;

            if (timesClickedLogs == 1)
            {
                StartCoroutine(moveLogs());
                moveLogs();
            }
        }

        if(isPlayerNearCampfire == true && areRocksPlaced == true && areLogsPlaced == true && isFireActive == false && Input.GetKeyDown(KeyCode.T))
        { 
            timesClickedFire++;

            if(timesClickedFire == 1)
            {
                StartCoroutine(moveFire());
                moveFire();
            }
        }

        if(isPlayerNearCampfire == true && areRocksPlaced == false && areLogsPlaced == false)
        {
            Hud.OpenMessagePanel("Press -T- to place the rocks");
        }

        if(isPlayerNearCampfire == true && areRocksPlaced == true && areLogsPlaced == false)
        {
            Hud.OpenMessagePanel("Press -T- to place the logs");
        }

        if(isPlayerNearCampfire == true && areRocksPlaced == true && areLogsPlaced == true && isFireActive == false)
        {
            Hud.OpenMessagePanel("Press -T- to light the campfire");
        }

        if(areRocksPlaced == true && areLogsPlaced == true && isFireActive == true)
        {
            Hud.CloseMessagePanel();
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
        logs.Translate(0, 10, 0);
        Hud.CloseMessagePanel();
        yield return new WaitForSeconds(0.5F);
        areLogsPlaced = true; 
        timesClickedLogs = 0;
    }

    IEnumerator moveFire()
    {
        int respawnTime = Random.Range(2, 10);

        fire.Translate(0, 10, 0);
        Hud.CloseMessagePanel();
        isFireActive = true;

        yield return new WaitForSeconds(respawnTime);
        fire.Translate(0, -10, 0);
        isFireActive = false;

        logs.Translate(0, -10, 0);
        areLogsPlaced = false;
        timesClickedFire = 0;
    }
}
