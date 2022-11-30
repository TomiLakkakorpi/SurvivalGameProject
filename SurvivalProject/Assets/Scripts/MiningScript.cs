using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningScript : MonoBehaviour
{
    public Transform rock;
    public GameObject rock4;
    public bool isRockMoved = false;
    public bool isPlayerNearRock = false;
    public int rockHitCount = 0;

    public AudioSource source;
    public AudioClip stoneSound;

    [SerializeField] private Transform Rock1SpawnPoint;
    [SerializeField] private Transform Rock2SpawnPoint;
    [SerializeField] private Transform Rock3SpawnPoint;
    [SerializeField] private Transform Rock4SpawnPoint;
    [SerializeField] private Transform Rock5SpawnPoint;

    public void PlayStoneminingSound()
    {
        source.clip = stoneSound;
        source.PlayOneShot(stoneSound);
    }
    
    void Update()
    {
        //Check if player is near the rock
        if(isPlayerNearRock == true)
        {
            //Check if mouse button has been pressed
            if (Input.GetMouseButtonDown(0)) 
            {
                PlayStoneminingSound();

                //Start hitcount coroutine
                StartCoroutine(addRockHitCount());
                addRockHitCount();

                //Check if enough hits have been done to mine the rock
                if(rockHitCount == 5)
                {

                //Start coroutine and call delay
                StartCoroutine(TimeBeforeRockMoved());
                TimeBeforeRockMoved();

                //Start coroutine and call delay
                StartCoroutine(RockRespawnDelay());
                RockRespawnDelay();

                isRockMoved = false;

                //Reset rock hit count back to 0
                rockHitCount = 0;
                }
            }
        }
    }

    //Turn "isPlayerNearRock" to true when player enters the tree´s vicinity
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            isPlayerNearRock = true;
        }
    }

    //Turn "isPlayerNearRock" to false when player exits the tree´s vicinity
    private void OnTriggerExit(Collider other)
    {
      if (other.tag == "Player")
        {
            isPlayerNearRock = false;
        }
    }

    //Function for rock respawn delay
    IEnumerator RockRespawnDelay()
    {
        //Get a random value between 4 minutes and 5 minutes
        int RespawnWaitTime = Random.Range(240,300);

        //Wait before moving the rock back
        yield return new WaitForSeconds(RespawnWaitTime);
        rock.Translate(0, 10, 0);
    }

    IEnumerator TimeBeforeRockMoved()
    {
        //Setting a 0.4 second wait time before rock is moved to match with the hit animation
        yield return new WaitForSeconds(0.4F);
        rock.Translate(0, -10, 0);
        isRockMoved = true;

        //Get a random value between 1 and 3    
        int numberOfRocks = Random.Range(3,6);

        //Spawn 3 rocks if random value is 3
        if (numberOfRocks == 3)
        {
            Instantiate(rock4, Rock1SpawnPoint.position, Rock1SpawnPoint.rotation);
            Instantiate(rock4, Rock2SpawnPoint.position, Rock2SpawnPoint.rotation);
            Instantiate(rock4, Rock3SpawnPoint.position, Rock3SpawnPoint.rotation);
        }

        //Spawn 4 rocks if random value is 4
        if (numberOfRocks == 4)
        {
            Instantiate(rock4, Rock1SpawnPoint.position, Rock1SpawnPoint.rotation);
            Instantiate(rock4, Rock2SpawnPoint.position, Rock2SpawnPoint.rotation);
            Instantiate(rock4, Rock3SpawnPoint.position, Rock3SpawnPoint.rotation);
            Instantiate(rock4, Rock4SpawnPoint.position, Rock4SpawnPoint.rotation);
        }

        //Spawn 5 rocks if random value is 5
        if (numberOfRocks == 5)
        {
            Instantiate(rock4, Rock1SpawnPoint.position, Rock1SpawnPoint.rotation);
            Instantiate(rock4, Rock2SpawnPoint.position, Rock2SpawnPoint.rotation);
            Instantiate(rock4, Rock3SpawnPoint.position, Rock3SpawnPoint.rotation);
            Instantiate(rock4, Rock4SpawnPoint.position, Rock4SpawnPoint.rotation);
            Instantiate(rock4, Rock5SpawnPoint.position, Rock5SpawnPoint.rotation);
        }
    }

    // 0.8 second delay before 1 is added to hitcount
    IEnumerator addRockHitCount()
    {
        //Play sound here?
        
        yield return new WaitForSeconds(0.8F);
        rockHitCount++;
    }
}
