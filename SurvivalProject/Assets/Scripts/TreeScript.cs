using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public Transform tree;

    public Vector3 normalTreePosition = new Vector3(0f, 0f, 0f);
    public Vector3 cutTreePosition = new Vector3(0f, 0f, 0f);
    
    public float moveSpeed = 5;
    private bool treeMoved = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(treeMoved)
        {
            tree.position = Vector3.Lerp(tree.position, cutTreePosition, Time.deltaTime * moveSpeed);
        }
        else
        {
            tree.position = Vector3.Lerp(tree.position, normalTreePosition, Time.deltaTime * moveSpeed);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            treeMoved = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            treeMoved = false;
        }
    }
}
