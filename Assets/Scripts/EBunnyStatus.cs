using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBunnyStatus : MonoBehaviour
{
    public float health = 10;
    public int numHeldItemMin = 1;
    public int numHeldItemMax = 3;
    public GameObject pickup1;
    public GameObject pickup2;

    Animation anim;

    bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void ApplyDamage(float damage)
    {
        if (health <= 0)
        {
            return;
        }
        health -= damage;

        anim.Play("EBunny_Hit");

        if (health <= 0 && !isDead)
        {
            health = 0;
            isDead = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        anim.Stop();
        anim.Play("EBunny_Death");
        Destroy(this.GetComponent<EBunnyAIController>());
        yield return new WaitForSeconds(2);

        Vector3 itemLocation = this.transform.position;
        int rewardItems = Random.Range(numHeldItemMin, numHeldItemMax);
        for (int i = 0; i < rewardItems; i++)
        {
            Vector3 randomItemLocation = itemLocation;
            randomItemLocation += new Vector3(Random.Range(-2f, 2f), 0.2f, Random.Range(-2f, 2f));
            if (Random.value > 0.5f)
            {
                Instantiate(pickup1, randomItemLocation, pickup1.transform.rotation);
            }
            else
            {
                Instantiate(pickup2, randomItemLocation, pickup2.transform.rotation);
            }
        }

        Destroy(this.gameObject);
    }

    public bool IsDead()
    {
        return isDead;
    }
}