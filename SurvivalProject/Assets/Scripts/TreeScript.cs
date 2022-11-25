using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public Transform tree;
    public bool isTreeMoved = false;
    public bool isTreeHit = false;
    public bool isPlayerInsideArea = false;

    public GameObject TestLog;

    [SerializeField] private Transform Log1SpawnPoint;
    [SerializeField] private Transform Log2SpawnPoint;
    [SerializeField] private Transform Log3SpawnPoint;

    void Start()
    {
        
    }

    void Update()
    {
        //Check if button has been pressed
        if(Input.GetKeyDown(KeyCode.R))
        {
            //Check if player is near the tree
            if (isPlayerInsideArea == true) 
            {
        
            tree.Translate(0, -10, 0);
            isTreeMoved = true;

            //Start coroutine and call time delay
            StartCoroutine(TimeDelay());
            TimeDelay();
            isTreeMoved = false;

            //Get a random value between 1 and 3    
            int numberOfLogs = Random.Range(1,4);

            //Spawn logs based on the random value
            if (numberOfLogs == 1)
            {
                Instantiate(TestLog, Log1SpawnPoint.position, Log1SpawnPoint.rotation);
            }

            if (numberOfLogs == 2)
            {
                Instantiate(TestLog, Log1SpawnPoint.position, Log1SpawnPoint.rotation);
                Instantiate(TestLog, Log2SpawnPoint.position, Log2SpawnPoint.rotation);
            }

            if (numberOfLogs == 3)
            {
                Instantiate(TestLog, Log1SpawnPoint.position, Log1SpawnPoint.rotation);
                Instantiate(TestLog, Log2SpawnPoint.position, Log2SpawnPoint.rotation);
                Instantiate(TestLog, Log3SpawnPoint.position, Log3SpawnPoint.rotation);
            } 

            isTreeHit = false;
            }
        }
    }

    //Turn "isPlayerInsideArea" to true when player enters the tree´s vicinity
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            isPlayerInsideArea = true;
        }
    }

    //Turn "isPlayerInsideArea" to false when player exits the tree´s vicinity
    private void OnTriggerExit(Collider other)
    {
      if (other.tag == "Player")
        {
            isPlayerInsideArea = false;
        }
    }

    // Function for tree respawn delay
    IEnumerator TimeDelay()
    {
        //Adding the timer and after the timer tree will be moved back to its original position
        yield return new WaitForSeconds(5);
        tree.Translate(0, 10, 0);
    }
}
