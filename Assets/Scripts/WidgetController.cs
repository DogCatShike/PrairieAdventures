using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetController : MonoBehaviour
{
    public float rollSpeed = 6;
    public float fastSpeed = 2;
    public float rotateSpeed = 4;
    public float duckSpeed = 0.5f;
    public float gravity = 20;
    public float jumpSpeed = 8;
    public bool isControllable = true;

    public CharacterController controller;
    public WidgetStatus widgetStatus;

    float moveHorz = 0;
    float normalHeight = 2;
    float duckHeight = 1;
    Vector3 moveDir = Vector3.zero;
    Vector3 rotateDir = Vector3.zero;
    bool isGrounded = false;
    bool isBoosting = false;
    bool isDucking = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        widgetStatus = GetComponent<WidgetStatus>();
    }

    void FixedUpdate()
    {
        if (!isControllable)
        {
            Input.ResetInputAxes();
        }
        else
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
                    if (widgetStatus)
                    {
                        if (widgetStatus.energy > 0)
                        {
                            moveDir *= fastSpeed;
                            widgetStatus.energy -= widgetStatus.widgetBoostUsage * Time.deltaTime;
                            isBoosting = true;
                        }
                    }
                }
                if (Input.GetButtonUp("Boost"))
                {
                    isBoosting = false;
                }

                if (Input.GetButton("Duck"))
                {
                    controller.height = duckHeight;
                    controller.center = new Vector3(controller.center.x, (controller.height / 2) + 0.25f, controller.center.z);
                    moveDir *= duckSpeed;
                    isDucking = true;
                }
                if (Input.GetButtonUp("Duck"))
                {
                    controller.height = normalHeight;
                    controller.center = new Vector3(controller.center.x, (controller.height / 2), controller.center.z);
                    moveDir *= rollSpeed;
                    isDucking = false;
                }

                if (Input.GetKeyUp(KeyCode.P))
                {
                    widgetStatus.ApplyDamage(3);
                }
                if (Input.GetKeyUp(KeyCode.O))
                {
                    widgetStatus.AddHealth(3);
                }
            }
            moveDir.y -= gravity * Time.deltaTime;

            CollisionFlags flags = controller.Move(moveDir * Time.deltaTime);
            controller.transform.Rotate(rotateDir * Time.deltaTime, rotateSpeed);
            isGrounded = ((flags & CollisionFlags.CollidedBelow) != 0);
        }
    }
}
