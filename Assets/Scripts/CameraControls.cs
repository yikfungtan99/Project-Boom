using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private CameraRotator cam_rotator;

    [SerializeField] private CinemachineVirtualCamera drive_vcam;
    [SerializeField] private CinemachineVirtualCamera artillery_vcam;
    [SerializeField] private CinemachineVirtualCamera drone_vcam;

    // Start is called before the first frame update
    void Start()
    {     
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentMode == null) return;

        switch (player.currentMode.mode)
        {
            case ModeType.DRIVE:

                drive_vcam.m_Priority = 1;
                artillery_vcam.m_Priority = 0;
                drone_vcam.m_Priority = 0;

                break;

            case ModeType.ARTILLERY:

                drive_vcam.m_Priority = 0;
                artillery_vcam.m_Priority = 1;
                drone_vcam.m_Priority = 0;

                break;

            case ModeType.DRONE:

                drive_vcam.m_Priority = 0;
                artillery_vcam.m_Priority = 0;
                drone_vcam.m_Priority = 1;

                break;

            default:
                break;
        }
    }
}
