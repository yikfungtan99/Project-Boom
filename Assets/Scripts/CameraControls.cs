using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private static CameraControls _instance;
    public static CameraControls Instance { get => _instance; }

    public Player player;

    public CinemachineVirtualCamera car_vcam;
    public CinemachineVirtualCamera artillery_vcam;
    public CinemachineVirtualCamera drone_vcam;

    private void Awake()
    {
        _instance = this;
    }

    public void SetUp(Player player, CarControl car, ArtilleryControl art, DroneControl drone)
    {
        this.player = player;

        car_vcam = car.carCam;
        artillery_vcam = art.artilleryCam;
        drone_vcam = drone.droneCam;

        car_vcam.Priority = 1;
        artillery_vcam.Priority = 0;
        drone_vcam.Priority = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        if (player.currentMode == null) return;

        switch (player.currentMode.mode)
        {
            case ModeType.DRIVE:

                car_vcam.m_Priority = 1;
                artillery_vcam.m_Priority = 0;
                drone_vcam.m_Priority = 0;

                break;

            case ModeType.ARTILLERY:

                car_vcam.m_Priority = 0;
                artillery_vcam.m_Priority = 1;
                drone_vcam.m_Priority = 0;

                break;

            case ModeType.DRONE:

                car_vcam.m_Priority = 0;
                artillery_vcam.m_Priority = 0;
                drone_vcam.m_Priority = 1;

                break;

            default:
                break;
        }
    }
}
