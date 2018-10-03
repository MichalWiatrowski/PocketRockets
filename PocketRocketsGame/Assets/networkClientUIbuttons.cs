using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class networkClientUIbuttons : MonoBehaviour {
    NetworkClient client;

    bool debug1 = false;
    bool debug2 = false;
    void OnGUI()
    {
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), "Debug Info");
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Status:" + client.isConnected);

        if (!client.isConnected)
        {
            if (GUI.Button(new Rect(10,10, 100, 70), "Connect"))
            {
                Connect();
            }
        }
        else
        {
            if (GUI.Button(new Rect((Screen.width / 2) - 205, (Screen.height / 2) - 10, 200, 100), "Button 1"))
            {
                debug1 = !debug1;
                sendButton1Info();

            }
            if (GUI.Button(new Rect((Screen.width / 2) + 205, (Screen.height / 2) - 10, 200, 100), "Button 2"))
            {
                debug2 = !debug2;
                sendButton2Info();
            }
        }
    }
	// Use this for initialization
	void Start () {
        client = new NetworkClient();
	}
	void Connect()
    {
        client.Connect("192.168.0.10", 25000);
    }

   void sendButton1Info()
    {
        if (client.isConnected)
        {
            IntegerMessage msg = new IntegerMessage();
            msg.value = System.Convert.ToInt32(debug1);
            client.Send(112, msg);
        }
    }
    void sendButton2Info()
    {
        if (client.isConnected)
        {
            IntegerMessage msg = new IntegerMessage();
            msg.value = System.Convert.ToInt32(debug2);
            client.Send(113, msg);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
