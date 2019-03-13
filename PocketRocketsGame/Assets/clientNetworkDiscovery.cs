using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
public class clientNetworkDiscovery : NetworkDiscovery {

	// Use this for initialization
	void Start () {
        //Init network discovery
        Initialize();
        //Start network discovery as client to search for local servers
        StartAsClient();
    }
	

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("Server Found with IP: " + fromAddress + " and port " + data);
        //string[] lol = fromAddress.Split(':');
        // discoveryIP = lol[1];
        networkClientUIbuttons.networkClient.discoveryIP = fromAddress;

        networkClientUIbuttons.networkClient.discoveryIP.Remove(0, 7);
        networkClientUIbuttons.networkClient.discoveryPort = System.Convert.ToInt32(data);
    }
}
