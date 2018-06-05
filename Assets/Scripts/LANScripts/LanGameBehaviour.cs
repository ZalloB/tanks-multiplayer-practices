using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LanGameBehaviour : NetworkBehaviour{

    [SyncVar]
    public GameObject text;

    public void Start()
    {
        text = GameObject.FindGameObjectWithTag("text");
    }


    public void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Blue") && GameObject.Find("GameBehaviour").GetComponent<LanBehaviour>().getPlayerNumber() > 1)
        {
            //red wins
            Cmdwin("Team Red Wins!");

        }
        else if (!GameObject.FindGameObjectWithTag("Red") && GameObject.Find("GameBehaviour").GetComponent<LanBehaviour>().getPlayerNumber() > 1)
        {
            //blue wins
            Cmdwin("Team Blue Wins!");
        }
    }

    [Command]
    void Cmdwin(string message)
    {
        text.GetComponent<Text>().text = message;

    }
    
}
