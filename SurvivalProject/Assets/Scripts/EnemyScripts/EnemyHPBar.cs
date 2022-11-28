using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    private Camera playerCamera;

    void Start()
    {
        playerCamera = GameObject.Find("Camera").GetComponent<Camera>();
    }
    void Update()
    {
        transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.back, playerCamera.transform.rotation * Vector3.up);
    }
}
