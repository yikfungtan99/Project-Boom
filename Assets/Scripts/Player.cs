using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public PlayerMode currentMode;

    public GameObject car;
    public GameObject drone;

    [SerializeField] private CarControl car_control;
    [SerializeField] private ArtilleryControl art_control;
    [SerializeField] private DroneControl drone_control;

    [ClientRpc]
    public void RpcSetUp(GameObject car, GameObject drone)
    {
        if (!hasAuthority) return;

        this.car = car;
        this.drone = drone;

        car_control = car.GetComponent<CarControl>() == null ? null : car.GetComponent<CarControl>() ;
        art_control = car.GetComponent<ArtilleryControl>() == null ? null : car.GetComponent<ArtilleryControl>();
        drone_control = drone.GetComponent<DroneControl>() == null ? null : drone.GetComponent<DroneControl>();

        car_control.owner = this;

        CameraControls.Instance.SetUp(this, car_control, art_control, drone_control);
        HudManager.Instance.SetPlayer(this);

        if(car_control != null) SwitchMode(car_control);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) return;

        if (Input.GetKeyDown(KeyCode.Space) && currentMode == car_control)
        {
            SwitchMode(art_control);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(currentMode.mode == ModeType.DRONE)
            {
                SwitchMode(car_control);
                return;
            }

            if(currentMode.mode == ModeType.DRIVE)
            {
                SwitchMode(drone_control);
                return;
            }

            if (currentMode.mode == ModeType.ARTILLERY)
            {
                SwitchMode(car_control);
                return;
            }
        }
    }

    void SwitchMode(PlayerMode mode)
    {
        if (mode == null) return;
        if(currentMode != null) currentMode.ExitMode();
        currentMode = mode;
        currentMode.EnterMode();
    }
}
