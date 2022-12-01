using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBottle : InventoryItemBase
{
    public int DrinkPoints = 20;

    public int WaterAmount;

    void Start()
    {
        WaterAmount = 100;
    }
    public override void OnUse()
    {
        // Add x amount of health to player
        WaterAmount = System.Math.Max(0, WaterAmount - DrinkPoints);
        if (WaterAmount >= DrinkPoints)
        {
            PlayerStatus.Instance.IncreaseDrink(DrinkPoints);
        }
        else if (WaterAmount < DrinkPoints)
        {
            PlayerStatus.Instance.IncreaseDrink(WaterAmount);
        }

        // PlayerInventory.Instance.inventory.RemoveItem(this);
        // Destroy(this.gameObject);
    }
}