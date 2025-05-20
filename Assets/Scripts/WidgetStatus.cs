using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetStatus : MonoBehaviour
{
    public float health = 10;
    public float maxHealth = 10;
    public float energy = 10;
    public float maxEnergy = 10;
    public float widgetBoostUsage = 5;

    public WidgetController playerController;

    void Start()
    {
        playerController = GetComponent<WidgetController>();
    }

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
            StartCoroutine(Die());
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

    IEnumerator Die()
    {
        Debug.Log("死亡");

        playerController.isControllable = false;

        yield return StartCoroutine(WaitForDie());

        HideCharacter();

        yield return StartCoroutine(WaitForOneSecond());

        ShowCharacter();

        health = maxHealth;
    }

    IEnumerator WaitForDie()
    {
        yield return new WaitForSeconds(3.5f);
    }

    IEnumerator WaitForOneSecond()
    {
        yield return new WaitForSeconds(1.0f);
    }

    void HideCharacter()
    {
        GameObject.FindGameObjectWithTag("Body").GetComponent<SkinnedMeshRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("Wheels").GetComponent<SkinnedMeshRenderer>().enabled = false;
        playerController.isControllable = false;
    }

    void ShowCharacter()
    {
        GameObject.FindGameObjectWithTag("Body").GetComponent<SkinnedMeshRenderer>().enabled = true;
        GameObject.FindGameObjectWithTag("Wheels").GetComponent<SkinnedMeshRenderer>().enabled = true;
        playerController.isControllable = true;
    }
}