using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarControl : PlayerMode
{
    public Player owner;
    public CinemachineVirtualCamera carCam;

    [Header("Wheels")]
    [SerializeField] private WheelCollider[] wheels;
    [SerializeField] private Transform[] wheelModels;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private float steerAngle;
    [SerializeField] private float steerSpeed;

    private int gear;

    private float curSpeed;
    private float curSteerAngle;

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) return;
        WheelModelUpdate();
        if (!modeActive) return;
        Inputs();
    }

    private void Inputs()
    {
        gear = 0;

        if (Input.GetKey(KeyCode.W))
        {
            gear = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            gear = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            curSteerAngle = Mathf.MoveTowardsAngle(curSteerAngle, -steerAngle, steerSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            curSteerAngle = Mathf.MoveTowardsAngle(curSteerAngle, steerAngle, steerSpeed * Time.deltaTime);
        }

        wheels[0].steerAngle = curSteerAngle;
        wheels[2].steerAngle = curSteerAngle;

        curSpeed = moveSpeed * gear;
    }

    private void WheelModelUpdate()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheels[i].GetWorldPose(out pos, out quat);
            wheelModels[i].transform.position = pos;
            wheelModels[i].transform.rotation = quat;
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].motorTorque = curSpeed;
        }
    }

    public override void ExitMode()
    {
        curSpeed = 0;
        base.ExitMode();
    }
}
