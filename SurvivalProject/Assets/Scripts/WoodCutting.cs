using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutting : MonoBehaviour
{
    public GameObject TestLog;
    public GameObject TestTreeStump;

    [SerializeField] private Transform Log1SpawnPoint;
    [SerializeField] private Transform Log2SpawnPoint;
    [SerializeField] private Transform StumpSpawnPoint;

    private void Start() 
    {
        Instantiate(TestTreeStump, StumpSpawnPoint.position, StumpSpawnPoint.rotation);
        Instantiate(TestLog, Log1SpawnPoint.position, Log1SpawnPoint.rotation);
        Instantiate(TestLog, Log2SpawnPoint.position, Log2SpawnPoint.rotation);
    }

    private void Update() 
    {
    
    }
}
