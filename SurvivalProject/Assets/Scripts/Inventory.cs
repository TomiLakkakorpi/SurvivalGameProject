using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public Button CloseButton;
    private bool isOpen = false;
    //public static InventoryManager inventoryManager;

    void Start()
    {
        inventory.SetActive(false);
       
        CloseButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        //This Method keeps track what is looted and updates inventory every frame to see items
        InventoryManager.Instance.ListItems();

        if (Input.GetKeyDown(KeyCode.B)){
             
            isOpen = !isOpen;
            if (isOpen){
                inventory.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            else {
             inventory.SetActive(false);
             Cursor.lockState = CursorLockMode.Locked;
             Cursor.visible = true;
            }
        }
    }

    //When pressing Closebutton from inventory itself
    void TaskOnClick(){
        isOpen = !isOpen;
        inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
