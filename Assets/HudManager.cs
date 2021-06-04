using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    private static HudManager _instance;
    public static HudManager Instance { get => _instance; }

    [SerializeField] private TextMeshProUGUI txtAngle;
    [SerializeField] private TextMeshProUGUI txtForce;

    [Header("Shell")]
    [SerializeField] private TextMeshProUGUI txtShellInfo;
    [SerializeField] private TextMeshProUGUI txtReloading;
    [SerializeField] private TextMeshProUGUI txtReloadTime;

    private Player player;

    private CarControl car;
    private ArtilleryControl art;
    private DroneControl drone;

    private void Awake()
    {
        _instance = this;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;

        car = player.car.GetComponent<CarControl>();
        art = player.car.GetComponent<ArtilleryControl>();
        drone = player.car.GetComponent<DroneControl>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHud();
    }

    void UpdateHud()
    {
        if(art != null)
        {
            txtAngle.text = "Angle: " + (int) art.curAngle;
            txtForce.text = "Force: " + (int) art.force;

            txtShellInfo.text = art.currentShellInfo;

            if(player.currentMode.mode == ModeType.ARTILLERY)
            {
                txtShellInfo.gameObject.SetActive(true);

                if (art.isReloading)
                {
                    txtReloadTime.text = art.currentReloadTime.ToString("0.00");
                    txtReloading.gameObject.SetActive(true);
                }
                else
                {
                    txtReloading.gameObject.SetActive(false);
                }
            }
            else
            {
                txtReloading.gameObject.SetActive(false);
                txtShellInfo.gameObject.SetActive(false);
            }
        }
    }
}
