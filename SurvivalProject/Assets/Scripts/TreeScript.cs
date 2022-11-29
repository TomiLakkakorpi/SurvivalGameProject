using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public Transform tree;
    public GameObject TestLog;
    public bool isTreeMoved = false;
    public bool isPlayerNearTree = false;
    public int treeHitCount = 0;

    [SerializeField] private Transform Log1SpawnPoint;
    [SerializeField] private Transform Log2SpawnPoint;
    [SerializeField] private Transform Log3SpawnPoint;

    void Update()
    {
        //Check if player is near the tree
        if(isPlayerNearTree == true && PlayerInventory.Instance.axeInHand)
        {
            //Check if mouse button has been pressed
            if (Input.GetMouseButtonDown(0)) 
            {
                //Start hitcount coroutine
                StartCoroutine(addHitCount());
                addHitCount();

                //Check if enough hits have been done to cut the tree
                if(treeHitCount >= 2)
                {
                    //Start coroutine and call delay
                    StartCoroutine(TimeBeforeTreeMoved());
                    TimeBeforeTreeMoved();

                    //Start coroutine and call delay
                    StartCoroutine(TreeRespawnDelay());
                    TreeRespawnDelay();
                    isTreeMoved = false;

                    //Reset tree hit count back to 0
                    treeHitCount = 0;
                }
            }
        }
    }

    //Turn "isPlayerNearTree" to true when player enters the tree´s vicinity
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            isPlayerNearTree = true;
        }
    }

    //Turn "isPlayerNearTree" to false when player exits the tree´s vicinity
    private void OnTriggerExit(Collider other)
    {
      if (other.tag == "Player")
        {
            isPlayerNearTree = false;
        }
    }

    //Function for tree respawn delay
    IEnumerator TreeRespawnDelay()
    {
        //Get a random value between 4 minutes and 5 minutes
        int RespawnWaitTime = Random.Range(240,300);

        //Wait before moving the tree back
        yield return new WaitForSeconds(RespawnWaitTime);
        tree.Translate(0, 10, 0);
    }

    IEnumerator TimeBeforeTreeMoved()
    {
        //Setting a 0.4 second wait time before tree is moved to match with the hit animation
        yield return new WaitForSeconds(0.4F);
        tree.Translate(0, -10, 0);
        isTreeMoved = true;

        //Get a random value between 1 and 3    
        int numberOfLogs = Random.Range(1,4);

        //Spawn 1 log if random value is 1
        if (numberOfLogs == 1)
        {
            Instantiate(TestLog, Log1SpawnPoint.position, Log1SpawnPoint.rotation);
        }

        //Spawn 2 logs if random value is 2
        if (numberOfLogs == 2)
        {
            Instantiate(TestLog, Log1SpawnPoint.position, Log1SpawnPoint.rotation);
            Instantiate(TestLog, Log2SpawnPoint.position, Log2SpawnPoint.rotation);
        }

        //Spawn 3 logs if random value is 3
        if (numberOfLogs == 3)
        {
            Instantiate(TestLog, Log1SpawnPoint.position, Log1SpawnPoint.rotation);
            Instantiate(TestLog, Log2SpawnPoint.position, Log2SpawnPoint.rotation);
            Instantiate(TestLog, Log3SpawnPoint.position, Log3SpawnPoint.rotation);
        }
    }

    // 0.8 second delay before 1 is added to hitcount
    IEnumerator addHitCount()
    {
        yield return new WaitForSeconds(0.8F);
        treeHitCount++;
    }
}
