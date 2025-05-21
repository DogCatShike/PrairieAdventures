using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint isActivePt;
    public CheckPoint firstPt;
    public WidgetStatus playerStatus;

    void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<WidgetStatus>();
        isActivePt = firstPt;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isActivePt != this)
        {
            isActivePt = this;
        }

        playerStatus.AddHealth(playerStatus.maxHealth);
        playerStatus.AddEnergy(playerStatus.maxEnergy);
    }
}