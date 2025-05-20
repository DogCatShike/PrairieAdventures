using System;
using UnityEngine;

public class WidgetStatus : MonoBehaviour
{
    public float health = 10;
    public float maxHealth = 10;
    public float energy = 10;
    public float maxEnergy = 10;
    public float widgetBoostUsage = 5;

    public void AddHealth(float boost)
    {
        health += boost;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void AddEnergy(float boost)
    {
        energy += boost;
        if (energy >= maxEnergy)
        {
            energy = maxEnergy;
        }
    }

    public void Die()
    {
        Debug.Log("死亡");
    }
}