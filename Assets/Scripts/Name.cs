using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Name : NetworkBehaviour
{
    [SyncVar(hook = "OnChangeName")]
    string pname = "Player";

    private void OnGUI()
    {

        if (isLocalPlayer)
        {
            pname = GUI.TextField(new Rect(25, Screen.height - 40, 100, 30), pname);
            if (GUI.Button(new Rect(125, Screen.height - 40, 80, 30), "Change"))
                CmdChangeName(pname);
        }
    }

    public void OnChangeName(string name)
    {
        this.GetComponentInChildren<TextMesh>().text = name;

    }

    [Command]
    public void CmdChangeName(string newName)
    {
        pname = newName;
    }

    public override void OnStartClient()
    {
        this.GetComponentInChildren<TextMesh>().text = pname;
    }
}