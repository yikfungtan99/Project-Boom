using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Player player;
    private Transform car;
    private bool impacted = false;

    // Start is called before the first frame update
    void Start()
    {
        car = player.Car.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!impacted)
        {
            float distance = Vector3.Distance(transform.position, car.position);
            print(distance);
            impacted = true;
        }
    }
}
