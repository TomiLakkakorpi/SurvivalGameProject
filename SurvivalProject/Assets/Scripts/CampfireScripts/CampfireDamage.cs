using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireDamage : MonoBehaviour
{
    [Header("Damage Variables")]
    public bool isPlayerInFire = false;
    public bool isFireDamageActive = false;

    [Header("Damage value")]
    public int Amount = -5;

    void Update()
    {
        if(isPlayerInFire == true)
        {
            if(isFireDamageActive == false)
            {
                StartCoroutine(RemoveHealth());
                RemoveHealth();
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            isPlayerInFire = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.tag == "Player")
        {
            isPlayerInFire = false;
        }
    }

    IEnumerator RemoveHealth()
    {
        isFireDamageActive = true;
        PlayerStatus.Instance.IncreaseHealth(Amount);
        yield return new WaitForSeconds(1);
        isFireDamageActive = false;
    }
}
