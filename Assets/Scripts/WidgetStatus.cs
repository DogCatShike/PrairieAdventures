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
    public AudioSource audio;
    public AudioClip hitSound;
    public AudioClip deathSound;

    public WidgetController playerController;
    public WidgetAnimation playerAnimation;
    public CharacterController controller;

    public GameObject body;
    public GameObject wheels;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        
        playerController = GetComponent<WidgetController>();
        playerAnimation = GetComponent<WidgetAnimation>();
        controller = GetComponent<CharacterController>();
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

        if (hitSound)
        {
            audio.clip = hitSound;
            audio.Play();
        }

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

        if (deathSound)
        {
            audio.clip = deathSound;
            audio.Play();
        }

        playerController.isControllable = false;

        playerAnimation.PlayDie();

        yield return StartCoroutine(WaitForDie());

        HideCharacter();

        yield return StartCoroutine(WaitForOneSecond());

        if (CheckPoint.isActivePt)
        {
            controller.transform.position = CheckPoint.isActivePt.transform.position;
            var pos = controller.transform.position;
            pos.y += 0.5f;
            controller.transform.position = pos;
        }

        ShowCharacter();

        playerAnimation.ReBorn();

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
        Debug.Log("隐藏角色");
        body.GetComponent<SkinnedMeshRenderer>().enabled = false;
        wheels.GetComponent<SkinnedMeshRenderer>().enabled = false;
        playerController.isControllable = false;
    }

    void ShowCharacter()
    {
        Debug.Log("显示角色");
        body.GetComponent<SkinnedMeshRenderer>().enabled = true;
        wheels.GetComponent<SkinnedMeshRenderer>().enabled = true;
        playerController.isControllable = true;
    }
}