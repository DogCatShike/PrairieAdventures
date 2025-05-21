using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public WidgetInventory.InventoryItem itemType;
    public int itemAmount = 1;
    bool pickedUp = false;

    void OnTriggerEnter(Collider other)
    {
        if (pickedUp)
        {
            return;
        }

        WidgetInventory widgetInventory = other.GetComponent<WidgetInventory>();
        widgetInventory.GetItem(itemType, itemAmount);

        pickedUp = true;
        Destroy(gameObject);
    }
}