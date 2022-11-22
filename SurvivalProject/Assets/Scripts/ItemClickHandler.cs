using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory inventory;
    public KeyCode Key;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if(Input.GetKeyDown(Key))
        {
            FadeToColor(button.colors.pressedColor);

            // Click the button
            button.onClick.Invoke();
        }
        else if(Input.GetKeyUp(Key))
        {
            FadeToColor(button.colors.normalColor);
        }
    }

    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, button.colors.fadeDuration, true, true);
    }

    public void OnItemClicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();

        InventoryItemBase item = dragHandler.Item;

        if (item != null)
            inventory.UseItem(item);
    }
}
