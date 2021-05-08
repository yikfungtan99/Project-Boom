using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtilleryControl : MonoBehaviour
{
    [SerializeField] private CarProperties car;

    [Header("Movement")]
    [SerializeField] private Transform artilleryHorizontal;
    [SerializeField] private Transform artilleryVertical;

    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;
    [SerializeField] private float liftSpeed;

    [SerializeField] private float rotateSpeed;

    private float curAngle;

    [Header("Fire")]
    [SerializeField] private Transform artilleryFirePoint;
    [SerializeField] private GameObject artilleryFireProjectile;

    [Header("Force")]
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;

    [SerializeField] private float forceRate;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI txtAngle;
    [SerializeField] private TextMeshProUGUI txtForce;

    private float force;

    [Header("")]
    [SerializeField] private bool modeActive = false;

    private void Start()
    {
        curAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!modeActive) return;

        RotateHorizontal();
        RotateVertical();
        ForceInput();
        FireInput();
        UpdateUI();
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
            GameObject firedProjectile = Instantiate(artilleryFireProjectile, artilleryFirePoint.position, artilleryFirePoint.rotation);
            //firedProjectile.GetComponent<Rigidbody>().velocity = artilleryFirePoint.forward * force;
            firedProjectile.GetComponent<Shell>().player = car.owner;
            firedProjectile.GetComponent<Rigidbody>().AddForce(artilleryFirePoint.forward * force, ForceMode.Impulse);
        }
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

    private void UpdateUI()
    {
        txtAngle.text = "V: " + (int)curAngle;
        txtForce.text = "Force: " + (int)force;
    }

    public void EnterMode()
    {
        modeActive = true;
    }

    public void ExitMode()
    {
        modeActive = false;
    }
}
