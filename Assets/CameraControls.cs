using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private CinemachineVirtualCamera drive_vcam;
    [SerializeField] private CinemachineVirtualCamera artillery_vcam;

    // Start is called before the first frame update
    void Start()
    {     
    }

    // Update is called once per frame
    void Update()
    {
        switch (player.currentMode)
        {
            case PlayerMode.DRIVE:

                drive_vcam.m_Priority = 1;
                artillery_vcam.m_Priority = 0;
                break;

            case PlayerMode.FIRE:

                drive_vcam.m_Priority = 0;
                artillery_vcam.m_Priority = 1;
                break;

            default:
                break;
        }
    }
}
