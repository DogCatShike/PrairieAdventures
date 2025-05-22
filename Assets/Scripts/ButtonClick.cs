using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public WidgetInventory widgetInventory;
    public UILabel screwCountLabel;
    public UILabel gearCountLabel;

    void Start()
    {
        widgetInventory = GameObject.FindWithTag("Player").GetComponent<WidgetInventory>();
        screwCountLabel = GameObject.Find("ScrewLabel").GetComponent<UILabel>();
        gearCountLabel = GameObject.Find("GearLabel").GetComponent<UILabel>();
    }

    void Update()
    {
        screwCountLabel.text = widgetInventory.GetItemCount(WidgetInventory.InventoryItem.ENERGYPACK).ToString();
        gearCountLabel.text = widgetInventory.GetItemCount(WidgetInventory.InventoryItem.REPAIRKIT).ToString();
    }

    public void OnRepairButtonClick()
    {
        widgetInventory.UseItem(WidgetInventory.InventoryItem.REPAIRKIT, 1);
    }

    public void OnEnergyButtonClick()
    {
        widgetInventory.UseItem(WidgetInventory.InventoryItem.ENERGYPACK, 1);
    }
}