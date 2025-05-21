using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetAnimation : MonoBehaviour
{
    public WidgetController playerController;
    public Animator animator;

    void Start()
    {
        playerController = GetComponent<WidgetController>();
        animator = GetComponent<Animator>();

        if (animator.layerCount == 2)
        {
            animator.SetLayerWeight(1, 1);
        }
    }

    void Update()
    {
        if (playerController.IsGrounded())
        {
            animator.SetBool("isFallDown", false);

            if (playerController.IsBoosting())
            {
                animator.SetBool("isBoost", true);
            }
            else
            {
                animator.SetBool("isBoost", false);
            }

            if (playerController.IsDucking())
            {
                animator.SetBool("isDuck", true);
            }
            else
            {
                animator.SetBool("isDuck", false);
            }

            if (playerController.IsMoving())
            {
                animator.SetFloat("Speed", Input.GetAxis("Vertical"));
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("isJump", true);
            }
            if (Input.GetButtonUp("Jump"))
            {
                animator.SetBool("isJump", false);
            }

            if (!playerController.IsGrounded())
            {
                animator.SetBool("isFallDown", true);
            }
        }
    }

    public void GetHit()
    {
        animator.SetBool("isGotHit", true);
    }

    public void PlayDie()
    {
        animator.SetBool("isDead", true);
    }

    public void ReBorn()
    {
        animator.SetBool("isDead", false);
    }
}