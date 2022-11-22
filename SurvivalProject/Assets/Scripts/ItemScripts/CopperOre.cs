using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperOre : InventoryItemBase
{
    public int WaterAmount = 20;

    public override void OnUse()
    {
        // Add x amount of food to player
        PlayerStatus.Instance.IncreaseDrink(WaterAmount);
        PlayerInventory.Instance.inventory.RemoveItem(this);
        Destroy(this.gameObject);
    }
}