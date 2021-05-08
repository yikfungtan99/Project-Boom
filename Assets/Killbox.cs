using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = spawnPos.position;
    }
}
