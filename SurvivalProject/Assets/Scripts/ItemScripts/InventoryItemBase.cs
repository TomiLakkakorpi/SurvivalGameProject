using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    Default,
    Consumable,
    Weapon,
    Offhand,
    Helmet,
    Breastplate
}

public class InventoryItemBase : MonoBehaviour
{
    public InventorySlot Slot
    {
        get; set;
    }
    public EItemType ItemType;
    private Camera Cam;

    void Awake() 
    {
        Cam = GameObject.Find("Player").transform.Find("CameraRoot").Find("Camera").GetComponent<Camera>();
    }
    public string Name;
    
    public Sprite Image;

    public virtual void OnDrop()
    {
        RaycastHit Hit = new RaycastHit();
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out Hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = Hit.point + dropHeight;
            gameObject.transform.eulerAngles = DropRotation;

            gameObject.AddComponent<Rigidbody>();
        }
    }

    public virtual void OnPickup()
    {
        //If item has rigidbody or animator component, destroy it
        Destroy(gameObject.GetComponent<Rigidbody>());
        Destroy(gameObject.GetComponent<Animation>());
        gameObject.SetActive(false);
    }

    public virtual void OnUse()
    {
        transform.localPosition = PickPosition;
        transform.localEulerAngles = PickRotation;
    }

    public Vector3 PickPosition;
    public Vector3 PickRotation;
    public Vector3 DropRotation;
    public Vector3 dropHeight;
}
