using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject Hand;
    public Inventory inventory;
    private IInventoryItem mItemToPickUp = null;
    private IInventoryItem mCurrentItem = null;
    public HUD Hud;

    void Start()
    {
        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;
    }
    
    void Update()
    {
        // Pickup item when near it by pressing F
        if(Input.GetKeyDown(KeyCode.F) && mItemToPickUp != null)
        {
            inventory.AddItem(mItemToPickUp);
            mItemToPickUp.OnPickup();
            Hud.CloseMessagePanel();
        }   
    }

    void FixedUpdate()
    {
        // Drop Item
        if (Input.GetKeyDown(KeyCode.L) && mCurrentItem != null)
        {
            DropCurrentItem();
        }
    }

    private void DropCurrentItem()
    {
        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;
        inventory.RemoveItem(mCurrentItem);

        Rigidbody rigidItem = goItem.AddComponent<Rigidbody>();
        rigidItem.AddForce(transform.forward * 2.0f, ForceMode.Impulse);

        Invoke("DoDropItem", 0.25f);
    }

    public void DoDropItem()
    {
        Destroy((mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());
    }

     private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent = Hand.transform;
        mCurrentItem = e.Item;
        mItemToPickUp = null;
        
        Collider collider = goItem.GetComponent<Collider>();
        if(collider != null)
        {
            collider.enabled = false;
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent = null;
        mItemToPickUp = null;

        Collider collider = goItem.GetComponentInChildren<Collider>();
        if(collider != null)
        {
            collider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if(item != null)
        {
            mItemToPickUp = item;
            Hud.OpenMessagePanel("");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if(item != null)
        {
            mItemToPickUp = null;
            Hud.CloseMessagePanel();
        }
    }
}
