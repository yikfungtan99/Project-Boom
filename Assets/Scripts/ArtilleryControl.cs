using Cinemachine;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtilleryControl : PlayerMode
{
    [SerializeField] private CarControl car;
    public CinemachineVirtualCamera artilleryCam;

    [Header("Movement")]
    [SerializeField] private Transform artilleryHorizontal;
    [SerializeField] private Transform artilleryVertical;

    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private float liftSpeed;

    [SerializeField] private float rotateSpeed;

    public float curAngle;

    [Header("Fire")]
    [SerializeField] private Transform artilleryFirePoint;
    

    [Header("Force")]
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float forceRate;

    [Header("Reload")]
    [SerializeField] private float reloadTime;
    [HideInInspector] public float currentReloadTime;
    [HideInInspector] public bool isReloading = false;

    private int chamberCount;

    [Header("Shell")]
    [SerializeField] private GameObject currentShell;

    [Header("Inventory")]
    [SerializeField] private ShellInventory selectedShellIvt;
    [SerializeField] private List<ShellInventory> shellInventory = new List<ShellInventory>();
    [SerializeField] private ShellObject debugShell;

    [HideInInspector] public string currentShellInfo;

    [SyncVar] private int shellSelect = 0;

    [Header("Effect")]
    [SerializeField] private ParticleSystem smoke;

    public float force { get; private set; }

    private void Start()
    {
        shellInventory.Add(new ShellInventory(debugShell, 999));
        curAngle = 0;
        selectedShellIvt = shellInventory[shellSelect];
        currentReloadTime = reloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) return;
        if (!modeActive) return;

        RotateHorizontal();
        RotateVertical();
        ForceInput();
        FireInput();
        ReloadInput();
        ShellSelectInput();
        UpdateShellInfo();
        ReloadTimer();
    }

    private void UpdateShellInfo()
    {
        currentShellInfo = chamberCount +  "/" + selectedShellIvt.count;
    }

    private void ShellSelectInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            shellSelect -= 1;
            if (shellSelect < 0)
            {
                shellSelect = shellInventory.Count;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            shellSelect += 1;
            if (shellSelect > shellInventory.Count - 1)
            {
                shellSelect = 0;
            }
        }

        selectedShellIvt = shellInventory[shellSelect];
    }

    private void ReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (selectedShellIvt == null) return;
            if (selectedShellIvt.count > 0 && chamberCount < selectedShellIvt.shellObject.maxLoadable)
            {
                Reload();
            }
        }
    }

    private void Reload()
    {
        isReloading = true;
    }

    private void ReloadTimer()
    {
        if (isReloading)
        {
            if (currentReloadTime <= 0)
            {
                currentReloadTime = 0;
                isReloading = false;
                LoadShell();
                currentReloadTime = reloadTime;
            }
            else
            {
                currentReloadTime -= Time.deltaTime;
            }
        }
    }

    private void LoadShell()
    {
        selectedShellIvt.count -= 1;
        CmdLoadShell();
        chamberCount += 1;
    }

    [Command]
    private void CmdLoadShell()
    {
        RpcLoadShell();
    }

    [ClientRpc]
    private void RpcLoadShell()
    {
        currentShell = selectedShellIvt.shellObject.shell;
    }

    private void ForceInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            force += forceRate * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            force -= forceRate * Time.deltaTime;
        }

        force = Mathf.Clamp(force, minForce, maxForce);
    }

    private void FireInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (chamberCount <= 0) return;
            Fire();
            if (isReloading)
            {
                isReloading = false;
                currentReloadTime = reloadTime;
            }
        }
    }

    private void Fire()
    {
        CmdFire(force, car.owner);
        chamberCount -= 1;
        if (chamberCount <= 0)
        {
            currentShell = null;
        }
    }

    [Command]
    private void CmdFire(float force, Player player)
    {
        GameObject firedProjectile = Instantiate(currentShell, artilleryFirePoint.position, artilleryFirePoint.rotation);
        NetworkServer.Spawn(firedProjectile);
        RpcFire(firedProjectile, force, player, player.car.transform);
    }

    [ClientRpc]
    private void RpcFire(GameObject projectile, float force, Player owner, Transform car)
    {
        projectile.GetComponent<Shell>().SetOwner(owner, car);
        projectile.GetComponent<Rigidbody>().AddForce(artilleryFirePoint.forward * force, ForceMode.Impulse);
        smoke.Play();
    }

    private void RotateVertical()
    {
        if (Input.GetAxisRaw("Vertical") > 0 && curAngle < maxAngle)
        {
            artilleryVertical.Rotate(Vector3.right, -liftSpeed * Time.deltaTime);
            curAngle += liftSpeed * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Vertical") < 0 && curAngle > minAngle)
        {
            artilleryVertical.Rotate(Vector3.right, liftSpeed * Time.deltaTime);
            curAngle -= liftSpeed * Time.deltaTime;
        }
    }

    private void RotateHorizontal()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            artilleryHorizontal.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            artilleryHorizontal.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
    }

    public override void ExitMode()
    {
        currentShell = null;
        chamberCount = 0;

        base.ExitMode();
    }
}
