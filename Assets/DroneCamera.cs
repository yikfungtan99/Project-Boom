using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DroneCamera : MonoBehaviour
{
    [SerializeField] private Transform camTransform;
    [SerializeField] private float droneLookSensitivity;

    float xRot = 0;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
    }
}
