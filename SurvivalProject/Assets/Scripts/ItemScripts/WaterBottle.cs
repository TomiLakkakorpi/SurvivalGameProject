using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBottle : InventoryItemBase
{
    public int DrinkPoints = 20;

    public override void OnUse()
    {
        PlayerStatus.Instance.IncreaseDrink(DrinkPoints);
        PlayerInventory.Instance.inventory.RemoveItem(this);
        Destroy(this.gameObject);
    }
}