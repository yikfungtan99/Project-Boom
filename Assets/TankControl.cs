using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    [SerializeField] private float turnRate;

    private int forward;
    private int flip = 0;

    private float curSpeed;

    // Update is called once per frame
    void Update()
    {
        forward = 0;

        if (Input.GetKey(KeyCode.W))
        {
            forward = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            forward = -1;
        }

        if (forward >= 0)
        {
            flip = 1;
        }
        else
        {
            flip = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.up, -turnRate * flip * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.up, turnRate * flip * Time.deltaTime);
        }

        curSpeed = moveSpeed * forward;
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.up * -9.821f, ForceMode.Acceleration);
        rb.AddForce(transform.forward * curSpeed, ForceMode.Acceleration);
    }
}
