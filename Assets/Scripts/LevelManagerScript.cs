using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour {

   // LobbyDiscovery discovery;

    void Awake()
    {

       // discovery = FindObjectOfType<LobbyDiscovery>();
    }

    void Start()
    {
      //  StopDiscovery();
    }


    private void StopDiscovery()
    {

        //if (discovery.running)
       // {
           // discovery.StopBroadcast();
       // }
    }

    public void StartOffline()
    {
        SceneManager.LoadScene("CompleteMainScene");
    }

    public void StartLAN()
    {
        SceneManager.LoadScene("Main");
    }



    public void CreateGame()
    {

       // StopDiscovery();
       // discovery.StartAsServer();
       // NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {

       // StopDiscovery();
        //discovery.StartAsClient();
       // NetworkManager.singleton.StartClient();
    }



    private void AddressData()
    {
        Debug.Log("Network ip: " + Network.natFacilitatorIP);
        Debug.Log("Network port: " + Network.natFacilitatorPort);

        Debug.Log("Networkplayer ip: " + Network.player.ipAddress);
        Debug.Log("Networkplayer port: " + Network.player.port);

        Debug.Log("Networkplayer external ip: " + Network.player.externalIP);
        Debug.Log("Networkplayer external port: " + Network.player.externalPort);


        Debug.Log("NetworkManager ip: " + NetworkManager.singleton.networkAddress);
        Debug.Log("NetworkManager port: " + NetworkManager.singleton.networkPort);
        Debug.Log("//////////////");
    }
}


