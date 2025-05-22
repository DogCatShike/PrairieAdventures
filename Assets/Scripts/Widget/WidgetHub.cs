using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetHub : MonoBehaviour
{
    public UISlider widgetHealthBarSlider;
    public UISlider widgetEnergyBarSlider;
    public WidgetStatus widgetStatus;

    void Start()
    {
        widgetHealthBarSlider = GameObject.Find("WidgetHealthBar").GetComponent<UISlider>();
        widgetEnergyBarSlider = GameObject.Find("WidgetEnergyBar").GetComponent<UISlider>();
        widgetStatus = GameObject.FindWithTag("Player").GetComponent<WidgetStatus>();
    }

    void Update()
    {
        widgetHealthBarSlider.value = widgetStatus.health / widgetStatus.maxHealth;
        widgetEnergyBarSlider.value = widgetStatus.energy / widgetStatus.maxEnergy;
    }
    
}