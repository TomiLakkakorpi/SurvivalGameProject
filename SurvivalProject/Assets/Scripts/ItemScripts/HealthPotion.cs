using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : InventoryItemBase
{
    public int HealthPoints = 20;
    public override string Name
    {
        get {
            return "HealthPotion";
        }
    }

    public override void OnUse()
    {
        // Add x amount of health to player
        base.OnUse();
    }
}