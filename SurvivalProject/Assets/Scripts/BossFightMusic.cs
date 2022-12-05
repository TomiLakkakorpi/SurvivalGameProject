using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightMusic : MonoBehaviour
{
    public AudioSource source;
    public AudioClip music;

    public bool isPlayerInWitchLair = false;
    public bool isMusicPlaying = false;

    void Update()
    {
        //Check if player is in the WitchLair
        //Check if music is already playing to prevent multiple clips playing at once
        if (isPlayerInWitchLair == true && isMusicPlaying == false)
        {
            //call PlayMusic function
            playMusic();

            //Start coroutine and play music again if previous clip ends
            StartCoroutine(StartMusicAgain());
            StartMusicAgain();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            //Turn isPlayerInWitchLair to true when the player enters the Witch Lair
            isPlayerInWitchLair = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
    if (other.tag == "Player")
          {
            //Turn isPlayerInWitchLair to false when the player exits the Witch Lair and call stopMusic function
            isPlayerInWitchLair = false;
            stopMusic();
         }
    }

    void playMusic()
    {
        //Turn isMusicPlaying to true so another clip wont start playing
        isMusicPlaying = true;

        //Play the audio clip
        source.clip = music;
        source.Play();
    }

    void stopMusic()
    {
        //Stop playing the music
        source.Stop();

        //Turn isMusicPlaying to false so another clip can start again
        isMusicPlaying = false;
    }

    IEnumerator StartMusicAgain()
    {
        //35 second delay
        yield return new WaitForSeconds(35);

        //Turn isMusicPlaying to false after 35 seconds so another clip can play.
        isMusicPlaying = false;
    }
}
