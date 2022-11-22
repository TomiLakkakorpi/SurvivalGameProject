using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldOre : InventoryItemBase
{
    public int FoodAmount = 20;

    public override void OnUse()
    {
        // Add x amount of food to player
        PlayerStatus.Instance.IncreaseFood(FoodAmount);
        PlayerInventory.Instance.inventory.RemoveItem(this);
        Destroy(this.gameObject);
    }
}
