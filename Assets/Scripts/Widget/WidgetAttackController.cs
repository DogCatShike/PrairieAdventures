using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetAttackController : MonoBehaviour
{
    public WidgetController controller;
    public Animator animator;

    public float attackTime = 1;
    public Vector3 attackPosition = new Vector3(0, 1, 0);
    public float attackRadius = 3;
    public float damage = 1;

    float timer = 0;
    bool isBusy = false;
    Vector3 ourLocation;
    GameObject[] enemies;
    GameObject wantedEnemy;

    void Start()
    {
        controller = GetComponent<WidgetController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!isBusy && Input.GetButtonDown("Attack") && timer >= attackTime)
        {
            StartCoroutine(DidAttack());
            isBusy = true;
            timer = 0;
        }
    }

    IEnumerator DidAttack()
    {
        animator.SetBool("isTaser", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("isTaser", false);

        ourLocation = transform.TransformPoint(attackPosition);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            EBunnyStatus enemyStatus = enemy.GetComponent<EBunnyStatus>();
            if (enemyStatus == null)
            {
                continue;
            }

            if (Vector3.Distance(enemy.transform.position, ourLocation) < attackRadius)
            {
                enemyStatus.ApplyDamage(damage);
            }
        }
        isBusy = false;
    }

    public GameObject GetClosetEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float disToEnemy = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float newDisToEnemy = Vector3.Distance(enemy.transform.position, this.transform.position);
            if (newDisToEnemy < disToEnemy)
            {
                disToEnemy = newDisToEnemy;
                wantedEnemy = enemy;
            }
        }

        return wantedEnemy;
    }
}