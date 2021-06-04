using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public Transform rotator;
    [SerializeField] private Rigidbody car;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float mouseSpeed;
    [SerializeField] private float rotateDecay;

    private bool released = true;

    Vector2 initMousePos = new Vector2(0, 0);

    private float mouseAngle;
    public float angleToRotate;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(released == true)
            {
                initMousePos = Input.mousePosition;
                released = false;
            }

            angleToRotate += (Input.mousePosition.x - initMousePos.x) * mouseSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            released = true;
        }

        if (released)
        {
            angleToRotate = Mathf.LerpAngle(angleToRotate, 0, rotateDecay * (car.velocity.magnitude/10) * Time.deltaTime);
        }

        if(angleToRotate > 360)
        {
            angleToRotate = -360;
        }

        if(angleToRotate < -360)
        {
            angleToRotate = 360;
        }

        rotator.localRotation = Quaternion.Euler(new Vector3(0, angleToRotate, 0));

    }
}
