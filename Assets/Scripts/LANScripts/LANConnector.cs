using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LANConnector : NetworkManager
{
    LanBehaviour lanBehaviour;

    private void Start()
    {
        lanBehaviour = FindObjectOfType<LanBehaviour>();
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if(lanBehaviour == null)
            {
                lanBehaviour = FindObjectOfType<LanBehaviour>();
            }

        lanBehaviour.setPlayerNumber();
        var player = (GameObject)GameObject.Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log("hola");

       if (lanBehaviour.getPlayerNumber() == 1) { player.tag = "Blue"; }
       if (lanBehaviour.getPlayerNumber() == 2) { player.tag = "Red"; }
       if (lanBehaviour.getPlayerNumber() == 3) { player.tag = "Blue"; }
       if (lanBehaviour.getPlayerNumber() == 4) { player.tag = "Red"; }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

}