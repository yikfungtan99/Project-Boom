using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : NetworkBehaviour
{
    public Player player;
    private Transform car;
    private bool impacted = false;

    [SerializeField] private TrailRenderer trail;

    public float explosionForce;
    public float explosionRadius;

    [SerializeField] private GameObject explosionEffect;

    // Start is called before the first frame update
    public void SetOwner(Player player)
    {
        this.player = player;
        car = player.car.transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!impacted)
        {
            print(collision.gameObject);

            float distance = Vector3.Distance(transform.position, car.position);

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
