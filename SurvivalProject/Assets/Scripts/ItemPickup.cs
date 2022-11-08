using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    //Adds Item to the list of items and removes it from the scene
    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    //Item pickup method?
    private void OnMouseDown()
    {
        Pickup();
    }
}
