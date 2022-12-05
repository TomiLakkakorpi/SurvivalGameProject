using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWellScript : MonoBehaviour
{
    private bool isPlayerNearWell = false;
    private int waterBottlesPicked = 0;
    public HUD Hud;
    public Inventory inventory;
    public InventoryItemBase waterBottle;
    public InventoryItemBase waterBottle1;
    public InventoryItemBase waterBottle2;

    void Start()
    {
        waterBottlesPicked = 0;
    }

    void Update()
    {
        if(isPlayerNearWell == true)
        {
            if (Input.GetKeyDown(KeyCode.F)) 
            {
                switch (waterBottlesPicked)
                {
                    case 0:
                        inventory.AddItem(waterBottle);
                        waterBottlesPicked += 1;
                        break;
                    case 1:
                        inventory.AddItem(waterBottle1);
                        waterBottlesPicked += 1;
                        break;
                    case 2:
                        inventory.AddItem(waterBottle2);
                        waterBottlesPicked += 1;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            if(waterBottlesPicked <= 2)
            {
                isPlayerNearWell = true;
                Hud.OpenMessagePanel("Press -F- to get water");
            }
            else {
                Hud.OpenMessagePanel("Waterwell is dry.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerNearWell = false;
            Hud.CloseMessagePanel();
        }
    }
}
