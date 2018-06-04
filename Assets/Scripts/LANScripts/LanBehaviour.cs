using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LanBehaviour : NetworkBehaviour { 

    [SyncVar]
    internal int playerNumber;

    LobbyDiscovery discovery;


    public int getPlayerNumber()
    {
        return playerNumber;
    }
    public void setPlayerNumber()
    {
        playerNumber++;
    }

    void Awake()
    {
        discovery = FindObjectOfType<LobbyDiscovery>();
    }


    public void Start()
    {
        playerNumber = 0;
    }


    public void Update()
    {
        if(!GameObject.FindGameObjectWithTag("Blue") && playerNumber > 1)
        {
            //red wins
        }else if(!GameObject.FindGameObjectWithTag("Red") && playerNumber > 1)
        {
            //blue wins
        }
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
}
