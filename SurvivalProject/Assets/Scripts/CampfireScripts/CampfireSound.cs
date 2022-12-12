using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireSound : MonoBehaviour
{
    [Header("Audio References")]
    public AudioSource source;
    public AudioClip clip;

    [Header("Boolean Variables")]
    public bool isClipPlaying = false;
    public bool isPlayerInArea = false;

    [Header("Access To Another Script")]
    [SerializeField] CampfireScript access;

    void Update()
    {
        if (access.isFireActive == true)
        {
            // if(isPlayerInArea == true)
            // {
            if (isClipPlaying == false)
            {
                playClip();
            }
            //}
        }

        if (access.isFireActive == false)
        {
            stopClip();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInArea = true;

            if (isClipPlaying == false && access.isFireActive == true)
            {
                playClip();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerInArea = false;
            stopClip();
        }
    }

    void playClip()
    {
        isClipPlaying = true;

        source.clip = clip;
        source.Play();
    }

    void stopClip()
    {
        source.Stop();
        isClipPlaying = false;
    }

    IEnumerator StartClipAgain()
    {
        yield return new WaitForSeconds(60);
        isClipPlaying = false;
    }
}
