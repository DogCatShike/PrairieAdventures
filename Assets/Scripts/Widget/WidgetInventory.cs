using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetInventory : MonoBehaviour
{
    public enum InventoryItem
    {
        ENERGYPACK,
        REPAIRKIT,
    }

    public WidgetStatus playerStatus;
    float repairKitHealAmt = 5;
    float energyPackHealAmt = 10;

    Dictionary<InventoryItem, int> widgetDict;

    void Start()
    {
        playerStatus = GetComponent<WidgetStatus>();
        widgetDict = new Dictionary<InventoryItem, int>();

        widgetDict.Add(InventoryItem.ENERGYPACK, 1);
        widgetDict.Add(InventoryItem.REPAIRKIT, 2);
    }

    public void GetItem(InventoryItem item, int amount)
    {
        widgetDict[item] += amount;
    }

    public void UseItem(InventoryItem item, int amount)
    {
        if (widgetDict[item] <= 0)
        {
            return;
        }
        widgetDict[item] -= amount;

        switch (item)
        {
            case InventoryItem.ENERGYPACK:
                playerStatus.AddEnergy(energyPackHealAmt);
                break;
            case InventoryItem.REPAIRKIT:
                playerStatus.AddHealth(repairKitHealAmt);
                break;
        }
    }

    public bool CompareItemCount(InventoryItem compItem, int compNumber)
    {
        return widgetDict[compItem] >= compNumber;
    }

    public int GetItemCount(InventoryItem compItem)
    {
        return widgetDict[compItem];
    }
}