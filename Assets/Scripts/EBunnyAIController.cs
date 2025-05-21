using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBunnyAIController : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 30;
    public float walkSpeed = 3;
    public float attackDis = 15;

    public float dirTraveltime = 2;
    public float idleTime = 1.5f;

    CharacterController controller;
    Animation animation;

    float timeToNewDir;
    Vector3 disToPlayer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animation = GetComponent<Animation>();

        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        animation.wrapMode = WrapMode.Loop;
        animation["EBunny_Death"].wrapMode = WrapMode.Once;

        animation["EBunny_Death"].layer = 5;
        animation["EBunny_Hit"].layer = 3;
        animation["EBunny_Attack"].layer = 1;

        StartCoroutine(InitEnemy());
    }

    IEnumerator InitEnemy()
    {
        while (true)
        {
            yield return StartCoroutine(Idle());
            // Attack();
        }
    }

    IEnumerator Idle()
    {
        while (true)
        {
            if (Time.time > timeToNewDir)
            {
                yield return new WaitForSeconds(idleTime);

                if (Random.value > 0.5f)
                {
                    transform.Rotate(new Vector3(0, 5, 0), rotateSpeed);
                }
                else
                {
                    transform.Rotate(new Vector3(0, -5, 0), rotateSpeed);
                }
                timeToNewDir = Time.time + dirTraveltime;
            }
            Vector3 walkForward = transform.TransformDirection(Vector3.forward);
            controller.SimpleMove(walkForward * walkSpeed);

            disToPlayer = transform.position - target.position;
            if (disToPlayer.magnitude < attackDis)
            {
                yield break;
            }
            yield return null;
        }
    }

    void Attack()
    {

    }
}