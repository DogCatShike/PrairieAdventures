using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnPoint : MonoBehaviour
{
    public float spawnRange = 40;
    public GameObject enemy;

    Transform target;
    GameObject currentEnemy;
    bool isOutsideRange;
    Vector3 disToPlayer;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        disToPlayer = transform.position - target.position;
        if (disToPlayer.magnitude < spawnRange)
        {
            if (!currentEnemy)
            {
                currentEnemy = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
            }
            isOutsideRange = false;
        }
        else
        {
            if (currentEnemy)
            {
                Destroy(currentEnemy);
            }
            isOutsideRange = true;
        }
    }
}