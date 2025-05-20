using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetController : MonoBehaviour
{
    public float rollSpeed = 6;
    public float fastSpeed = 2;
    public float rotateSpeed = 4;
    public float gravity = 20;
    public float jumpSpeed = 8;
    public CharacterController controller;

    float moveHorz = 0;
    Vector3 moveDir = Vector3.zero;
    Vector3 rotateDir = Vector3.zero;
    bool isGrounded = false;
    bool isBoosting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            moveDir = new Vector3(h, 0, v);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= rollSpeed;

            moveHorz = Input.GetAxis("Horizontal");
            if (moveHorz > 0)
            {
                rotateDir = new Vector3(0, 1, 0);
            }
            else if (moveHorz < 0)
            {
                rotateDir = new Vector3(0, -1, 0);
            }
            else
            {
                rotateDir = Vector3.zero;
            }

            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }

            if (Input.GetButton("Boost"))
            {
                moveDir *= fastSpeed;
                isBoosting = true;
            }
            if (Input.GetButtonUp("Boost"))
            {
                isBoosting = false;
            }
        }
        moveDir.y -= gravity * Time.deltaTime;

        CollisionFlags flags = controller.Move(moveDir * Time.deltaTime);
        controller.transform.Rotate(rotateDir * Time.deltaTime, rotateSpeed);
        isGrounded = ((flags & CollisionFlags.CollidedBelow) != 0);
    }
}
