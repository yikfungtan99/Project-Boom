using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private CarProperties car;

    [Header("Wheels")]
    [SerializeField] private WheelCollider[] wheels;
    [SerializeField] private Transform[] wheelModels;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private float steerAngle;
    [SerializeField] private float steerSpeed;

    [SerializeField] private bool modeActive;

    private int gear;

    private float curSpeed;
    private float curSteerAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    public void EnterMode()
    {
        modeActive = true;
    }

    public void ExitMode()
    {
        curSpeed = 0;
        modeActive = false;
    }
}
