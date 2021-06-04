using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    public bool isShowingSettings = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsMenu();
        }
    }

    void ToggleSettingsMenu()
    {
        isShowingSettings = !isShowingSettings;
        settingsMenu.SetActive(isShowingSettings);
    }
}
