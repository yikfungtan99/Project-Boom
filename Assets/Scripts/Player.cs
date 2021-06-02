using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance { get => _instance; }

    public PlayerMode currentMode;

    public GameObject Car;

    [SerializeField] private CarControl car_control;
    [SerializeField] private ArtilleryControl art_control;
    [SerializeField] private DroneControl drone_control;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        SwitchMode(car_control);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentMode == car_control)
        {
            SwitchMode(art_control);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && currentMode == art_control)
        {
            SwitchMode(car_control);
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
