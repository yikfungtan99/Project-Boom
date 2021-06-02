using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Player player;
    private Transform car;
    private bool impacted = false;

    [SerializeField] private TrailRenderer trail;

    [SerializeField] private float currentInterval;
    private float trailInterval;

    public float explosionForce;
    public float explosionRadius;

    [SerializeField] private GameObject explosionEffect;

    private Rigidbody rb; 

    // Start is called before the first frame update
    void Start()
    {
        car = player.Car.transform;
        rb = GetComponent<Rigidbody>();
        //trail.AddPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //if(currentInterval >= trailInterval)
        //{
        //    trail.AddPosition(transform.position);
        //    currentInterval = 0;
        //}
        //else
        //{
        //    currentInterval += Time.deltaTime;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!impacted)
        {
            float distance = Vector3.Distance(transform.position, car.position);
            print(distance);
            impacted = true;

            ExplosionForce();

            GameObject explodeEffect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explodeEffect, 3.0f);
            gameObject.SetActive(false);
        }
    }

    private void ExplosionForce()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, 1.0f);
            }
        }
    }
}
