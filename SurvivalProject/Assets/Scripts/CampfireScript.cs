using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireScript : MonoBehaviour
{
    public Transform rocks;
    public Transform logs;
    public Transform fire;

    public bool isPlayerNearCampfire = false;

    public bool areRocksPlaced = false;
    public bool areLogsPlaced = false;
    public bool isFireActive = false;

    void Start()
    {
        rocks.Translate(0, -10, 0);
        logs.Translate(0, -10, 0);
        fire.Translate(0, -10, 0);
    }

    void Update()
    {
        if(isPlayerNearCampfire == true && Input.GetKeyDown(KeyCode.T) && areRocksPlaced == false) /* && if player has 9 rocks in inventory*/
        {
            moveRocks();            
        }

        if(isPlayerNearCampfire == true && Input.GetKeyDown(KeyCode.T) && areLogsPlaced == false) /* && if player has 3 logs in inventory*/
        {
            moveLogs();
        }

        if(isPlayerNearCampfire == true && areRocksPlaced == true && areLogsPlaced == true && Input.GetKeyDown(KeyCode.Y) && isFireActive == false)
        {
            StartCoroutine(moveFire());
            moveFire();
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
        }
    }

    void moveRocks()
    {
        rocks.Translate(0, 10, 0);
        areRocksPlaced = true;
    }

    void moveLogs()
    {
        logs.Translate(0, 10, 0);
        areLogsPlaced = true;
    }

    IEnumerator moveFire()
    {
        fire.Translate(0, 10, 0);
        isFireActive = true;

        yield return new WaitForSeconds(60F);
        fire.Translate(0, -10, 0);
        isFireActive = false;

        logs.Translate(0, -10, 0);
        areLogsPlaced = false;
    }
}
