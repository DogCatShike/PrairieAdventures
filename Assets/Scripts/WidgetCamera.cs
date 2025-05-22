using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5;
    public float height = 5;
    public float heightDamping = 2;
    public float rotationDamping = 3;
    public float distanceDampingX = 0.5f;
    public float distanceDampingZ = 0.2f;
    public float camSpeed = 2;
    public bool isSmoothed = true;

    float wantedRotationAngle;
    float wantedHeight;
    float wantedDistanceZ;
    float wantedDistanceX;

    float currentRotationAngle;
    float currentHeight;
    float currentDistanceZ;
    float currentDistanceX;

    Quaternion currentRotation;

    void LateUpdate()
    {
        if (!target)
        {
            return;
        }

        wantedRotationAngle = target.eulerAngles.y;
        wantedHeight = target.position.y + height;
        wantedDistanceZ = target.position.z - distance;
        wantedDistanceX = target.position.x - distance;

        currentRotationAngle = transform.eulerAngles.y;
        currentHeight = transform.position.y;
        currentDistanceZ = transform.position.z;
        currentDistanceX = transform.position.x;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        currentHeight = Mathf.LerpAngle(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        currentDistanceZ = Mathf.LerpAngle(currentDistanceZ, wantedDistanceZ, distanceDampingZ * Time.deltaTime);
        currentDistanceX = Mathf.LerpAngle(currentDistanceX, wantedDistanceX, distanceDampingX * Time.deltaTime);

        currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position -= currentRotation * Vector3.forward * distance;

        transform.position = new Vector3(currentDistanceX, currentHeight, currentDistanceZ);

        LookAtMe();
    }

    void LookAtMe()
    {
        if (isSmoothed)
        {
            Quaternion camRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, camRotation, Time.deltaTime * camSpeed);
        }
        else
        {
            transform.LookAt(target);
        }
    }
}