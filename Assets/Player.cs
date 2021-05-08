using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    DRIVE,
    FIRE
}

public class Player : MonoBehaviour
{
    public PlayerMode currentMode;

    public GameObject Car;

    private CarMovement car_control;
    private ArtilleryControl art_control;

    // Start is called before the first frame update
    void Start()
    {
        car_control = Car.GetComponent<CarMovement>();
        art_control = Car.GetComponent<ArtilleryControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentMode == PlayerMode.DRIVE)
            {
                currentMode = PlayerMode.FIRE;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentMode == PlayerMode.FIRE)
            {
                currentMode = PlayerMode.DRIVE;
            }
        }

        UpdateMode();
    }

    private void UpdateMode()
    {
        switch (currentMode)
        {
            case PlayerMode.DRIVE:

                art_control.ExitMode();
                car_control.EnterMode();
                
                break;

            case PlayerMode.FIRE:

                car_control.ExitMode();
                art_control.EnterMode();

                break;

            default:
                break;
        }
    }
}
