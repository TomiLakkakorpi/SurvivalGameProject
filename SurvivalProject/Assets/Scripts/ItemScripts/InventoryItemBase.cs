using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    private Camera Cam;
    void Awake() {
        Cam = GameObject.Find("Player").transform.Find("CameraRoot").GetChild(0).GetComponent<Camera>();
    }
    public virtual string Name
    {
        get {
            return "base_item";
        }
    }

    public Sprite _Image; 
    public Sprite Image
    {
        get { return _Image; }
    }

    public virtual void OnDrop()
    {
        RaycastHit Hit = new RaycastHit();
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out Hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = Hit.point;
            gameObject.transform.eulerAngles = DropRotation;
        }
    }

    public virtual void OnPickup()
    {
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
}
