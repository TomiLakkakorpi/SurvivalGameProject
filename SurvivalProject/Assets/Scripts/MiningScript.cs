using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningScript : MonoBehaviour
{
    public Transform rock;
    public GameObject rock4;
    public AudioSource source;
    public AudioClip stoneSound;

    public bool isRockMoved = false;
    public bool isPlayerNearRock = false;
    public bool soundPlaying = false;

    public int rockHitCount = 0;

    [SerializeField] private Transform Rock1SpawnPoint;
    [SerializeField] private Transform Rock2SpawnPoint;
    [SerializeField] private Transform Rock3SpawnPoint;
    [SerializeField] private Transform Rock4SpawnPoint;
    [SerializeField] private Transform Rock5SpawnPoint;

    //Function for mining sound
    public void PlayStoneminingSound()
    {
        source.clip = stoneSound;
        source.PlayOneShot(stoneSound);
    }
    
    void Update()
    {
        //Check if player is near the rock (And if the player is holding a pickaxe (Not working))
        if(isPlayerNearRock == true && PlayerInventory.Instance.pickaxeInHand )
        {
            //Check if mouse button has been pressed
            if (Input.GetMouseButtonDown(0)) 
            {
                //Check if sound is already playing
                if (soundPlaying == false)
                {
                    StartCoroutine(callSound());
                    callSound();
                }

                //Start hitcount coroutine
                StartCoroutine(addRockHitCount());
                addRockHitCount();

                //Check if enough hits have been done to mine the rock
                if(rockHitCount >= 5)
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

        //Get a random value between 1 and 3    
        int numberOfRocks = Random.Range(3,6);

        //Spawn 3 rocks if random value is 3
        if (numberOfRocks == 3)
        {
            Instantiate(rock4, Rock1SpawnPoint.position, Rock1SpawnPoint.rotation);
            Instantiate(rock4, Rock2SpawnPoint.position, Rock2SpawnPoint.rotation);
            Instantiate(rock4, Rock3SpawnPoint.position, Rock3SpawnPoint.rotation);

            rock.Translate(0, -10, 0);
            isRockMoved = true;
        }

        //Spawn 4 rocks if random value is 4
        if (numberOfRocks == 4)
        {
            Instantiate(rock4, Rock1SpawnPoint.position, Rock1SpawnPoint.rotation);
            Instantiate(rock4, Rock2SpawnPoint.position, Rock2SpawnPoint.rotation);
            Instantiate(rock4, Rock3SpawnPoint.position, Rock3SpawnPoint.rotation);
            Instantiate(rock4, Rock4SpawnPoint.position, Rock4SpawnPoint.rotation);

            rock.Translate(0, -10, 0);
            isRockMoved = true;
        }

        //Spawn 5 rocks if random value is 5
        if (numberOfRocks == 5)
        {
            Instantiate(rock4, Rock1SpawnPoint.position, Rock1SpawnPoint.rotation);
            Instantiate(rock4, Rock2SpawnPoint.position, Rock2SpawnPoint.rotation);
            Instantiate(rock4, Rock3SpawnPoint.position, Rock3SpawnPoint.rotation);
            Instantiate(rock4, Rock4SpawnPoint.position, Rock4SpawnPoint.rotation);
            Instantiate(rock4, Rock5SpawnPoint.position, Rock5SpawnPoint.rotation);

            rock.Translate(0, -10, 0);
            isRockMoved = true;
        }
    }

    // 0.8 second delay before 1 is added to hitcount
    IEnumerator addRockHitCount()
    {
        yield return new WaitForSeconds(0.8F);
        rockHitCount++;
    }

    //Sound function
    IEnumerator callSound()
    {
        //Turn soundPlaying = true so another sound cant start until this one is finished
        soundPlaying = true;

        source.clip = stoneSound;
        source.PlayOneShot(stoneSound);
        yield return new WaitForSeconds(0.8F);

        //Turn soundPlaying back to false so another sound can be played again
        soundPlaying = false;
    }
}
