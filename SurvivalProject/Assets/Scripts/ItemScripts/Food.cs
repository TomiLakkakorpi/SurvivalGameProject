using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : InventoryItemBase
{
    public int FoodPoints = 20;

    public override void OnUse()
    {
        PlayerStatus.Instance.IncreaseFood(FoodPoints);

        PlayerInventory.Instance.inventory.RemoveItem(this);

        Destroy(this.gameObject);
    }
}
