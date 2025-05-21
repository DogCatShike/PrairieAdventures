using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public float damage = 20;
    public WidgetStatus playerStatus;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        // playerStatus = other.GetComponent<WidgetStatus>();
        playerStatus = GameObject.FindWithTag("Player").GetComponent<WidgetStatus>();
        playerStatus.ApplyDamage(damage);
    }
}