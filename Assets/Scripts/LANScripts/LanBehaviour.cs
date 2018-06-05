using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LanBehaviour : NetworkBehaviour {

    [SyncVar]
    internal int playerNumber;

    LobbyDiscovery discovery;

    void Awake()
    {
        discovery = FindObjectOfType<LobbyDiscovery>();
    }


    public void CreateGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("Canvas"));
        StopDiscovery();
        discovery.StartAsServer();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("Canvas"));
        StopDiscovery();
        //discovery.StartAsClient();
        NetworkManager.singleton.StartClient();
    }

    private void StopDiscovery()
    {

        if (discovery.running)
        {
            discovery.StopBroadcast();
        }
    }


    public int getPlayerNumber()
    {
        return playerNumber;
    }

    public void setPlayerNumber()
    {
        playerNumber++;
    }

    public override void OnStartClient()
    {
        GameObject.Find("GameBehaviour").GetComponent<LanBehaviour>().setPlayerNumber();
    }
}
