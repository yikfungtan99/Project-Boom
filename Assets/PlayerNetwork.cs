using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject dronePrefab;
    [SerializeField] private Player player;

    public GameObject car;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        CmdSpawnCar();
    }

    [Command]
    public void CmdSpawnCar()
    {
        GameObject spawnedCar = Instantiate(carPrefab, new Vector3(0,5,0), Quaternion.identity);
        GameObject spawnedDrone = Instantiate(dronePrefab, new Vector3(0,8,0), Quaternion.identity);

        NetworkServer.Spawn(spawnedCar, netIdentity.connectionToClient);
        NetworkServer.Spawn(spawnedDrone, netIdentity.connectionToClient);

        player.RpcSetUp(spawnedCar, spawnedDrone);
    }
}
