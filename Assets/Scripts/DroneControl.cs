using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl : PlayerMode
{
    public CinemachineVirtualCamera droneCam;

    [Header("Movement")]
    [SerializeField] private Vector3 droneSpeed;

    [Header("Look")]
    [SerializeField] private Transform camTransform;
    [SerializeField] private float droneLookSensitivity;
    [SerializeField] private float droneLookSmooth;
    [SerializeField] private float droneLookMax;

    private Vector3 currentPlaneSpeed = Vector3.zero;

    private Rigidbody rb;

    private Vector3 moveX;
    private Vector3 moveZ;

    private float mouseX;
    private float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) return;
        if (!modeActive) return;

        DroneMovement();
        DroneRotation();
    }

    private void DroneRotation()
    {
        mouseX += Input.GetAxis("Mouse X") * droneLookSensitivity * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * droneLookSensitivity * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -droneLookMax, 0.1f);
    }

    private void DroneMovement()
    {
        currentPlaneSpeed = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentPlaneSpeed.y = droneSpeed.y;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentPlaneSpeed.y = -9.81f;
        }

        moveX = transform.right * Input.GetAxisRaw("Horizontal") * droneSpeed.x;
        moveZ = transform.forward * Input.GetAxisRaw("Vertical") * droneSpeed.z;
    }

    private void FixedUpdate()
    { 
        currentPlaneSpeed += moveX + moveZ;
        rb.velocity += currentPlaneSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.x, mouseX, transform.rotation.z), droneLookSmooth);
        camTransform.localRotation = Quaternion.Slerp(camTransform.localRotation, Quaternion.Euler(-mouseY, camTransform.localRotation.y, camTransform.localRotation.z), droneLookSmooth);
    }

    public override void ExitMode()
    {
        base.ExitMode();
        moveX = Vector3.zero;
        moveZ = Vector3.zero;
    }
}
