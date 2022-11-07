using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Slider healtBar;
    public static int currentHealt = 100;
   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }

    }

    public void TakeDamage(int Damage)
    {
        currentHealt -= Damage;
        healtBar.value = currentHealt;
    }
         
}
