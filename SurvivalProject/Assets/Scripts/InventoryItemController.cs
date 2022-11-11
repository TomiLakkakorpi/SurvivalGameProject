using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;

    public Button RemoveButton;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    //If you have usable item. Defines what can you do with them.
    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion:
            PlayerStatus.Instance.IncreaseHealth(item.value);
                break;
            case Item.ItemType.Food:
            PlayerStatus.Instance.IncreaseFood(item.value);
                break;
            case Item.ItemType.Drink:
            PlayerStatus.Instance.IncreaseDrink(item.value);
                break;
        }

        RemoveItem();
    }
}
