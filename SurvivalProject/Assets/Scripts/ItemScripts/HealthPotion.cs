using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : InventoryItemBase
{
    public int HealthPoints = 20;

    public override void OnUse()
    {
        // Add x amount of health to player
        PlayerStatus.Instance.IncreaseHealth(HealthPoints);
        PlayerInventory.Instance.inventory.RemoveItem(this);
        Destroy(this.gameObject);
    }
}