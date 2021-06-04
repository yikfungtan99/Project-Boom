using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkHud : MonoBehaviour
{
    [SerializeField] private TMP_InputField ipInput;

    [SerializeField] private Button btnJoin;
    [SerializeField] private Button btnHost;
    [SerializeField] private Button btnDisconnect;

    private NetworkManager net;

    private void Start()
    {
        net = NetworkManager.singleton;
    }

    public void Host()
    {
        net.networkAddress = ipInput.text;
        net.StartHost();

        btnDisconnect.gameObject.SetActive(true);
        btnJoin.gameObject.SetActive(false);
        btnHost.gameObject.SetActive(false);
    }

    public void Join()
    {
        net.networkAddress = ipInput.text;
        net.StartClient();

        btnDisconnect.gameObject.SetActive(true);
        btnJoin.gameObject.SetActive(false);
        btnHost.gameObject.SetActive(false);
    }

    public void Disconnect()
    {
        net.StopServer();
        net.StopHost();
        net.StopClient();
        
        btnDisconnect.gameObject.SetActive(false);
        btnJoin.gameObject.SetActive(true);
        btnHost.gameObject.SetActive(true);
    }
}
