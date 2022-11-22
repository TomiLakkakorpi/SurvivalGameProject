using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public GameObject Hand;
    public GameObject Head;
    public GameObject Body;
    
    public Inventory inventory;
    private InventoryItemBase mItemToPickUp = null;
    private InventoryItemBase mCurrentItem = null;
    public HUD Hud;
    public GameObject orientation;
    private bool isInventoryOpen = false;
    private Animator mAnimator;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        mAnimator = GetComponentInChildren<Animator>();
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
            mItemToPickUp = null;
        }   

        if(Input.GetKeyDown(KeyCode.B))
        {
            if(!isInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isInventoryOpen = true;
            }
            else if(isInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isInventoryOpen = false;
            }
        }

        // Drop Item
        if (mCurrentItem != null && Input.GetKeyDown(KeyCode.L))
        {
            DropCurrentItem();
        }
    }

    private void DropCurrentItem()
    {
        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;
        inventory.RemoveItem(mCurrentItem);

        Rigidbody rigidItem = goItem.AddComponent<Rigidbody>();
        rigidItem.AddForce(orientation.transform.forward * 5.0f, ForceMode.Impulse);

        mCurrentItem = null;
        mAnimator.SetBool("Armed", false);
    }

    private void SetItemToHand(InventoryItemBase item, bool active)
    {
        GameObject currentItem = (item as MonoBehaviour).gameObject;
        currentItem.SetActive(active);
        currentItem.transform.parent = active ? Hand.transform : null;

        Collider collider = currentItem.GetComponent<Collider>();
        if(collider != null)
        {
            collider.enabled = false;
        }
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        if (e.Item.ItemType != EItemType.Consumable)
        {
            if (e.Item.ItemType == EItemType.Weapon)
            {
                // If the player carries an item, un-use it (remove from player's hand)
                if (mCurrentItem != null)
                {
                    SetItemToHand(mCurrentItem, false);
                    mCurrentItem = null;
                }
                InventoryItemBase item = e.Item;
                // Use item (put it to hand of the player)
                SetItemToHand(item, true);
                mCurrentItem = e.Item;
                mAnimator.SetBool("Armed", true);
            }
            if (e.Item.ItemType == EItemType.Helmet)
            {
                InventoryItemBase item = e.Item;
                GameObject currentItem = (item as MonoBehaviour).gameObject;
                currentItem.SetActive(true);
                currentItem.transform.parent = Head.transform;
            }
            if (e.Item.ItemType == EItemType.Breastplate)
            {
                InventoryItemBase item = e.Item;
                GameObject currentItem = (item as MonoBehaviour).gameObject;
                currentItem.SetActive(true);
                currentItem.transform.parent = Body.transform;
            }
        }
        
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        InventoryItemBase item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);
        goItem.transform.parent = null;

        if (item == mCurrentItem)
        {
            mCurrentItem = null;
            mAnimator.SetBool("Armed", false);
        }

        Collider collider = goItem.GetComponentInChildren<Collider>();
        if(collider != null)
        {
            collider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InventoryItemBase item = other.GetComponent<InventoryItemBase>();
        if(item != null)
        {
            mItemToPickUp = item;
            Hud.OpenMessagePanel("");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InventoryItemBase item = other.GetComponent<InventoryItemBase>();
        if(item != null)
        {
            mItemToPickUp = null;
            Hud.CloseMessagePanel();
        }
    }
}
