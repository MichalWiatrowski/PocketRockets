using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class networkDiscoveryClient : NetworkDiscovery {

	// Use this for initialization
	void Start () {
		
	}
	

    public void init()
    {
        //Init network discovery
        Initialize();
        //Start network discovery as client to search for local servers
        StartAsClient();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        //Debug.Log("Server Found with IP: " + fromAddress + " and port " + data);
        //string[] lol = fromAddress.Split(':');
        // discoveryIP = lol[1];
        GetComponent<networkClientUIbuttons>().discoveryIP = fromAddress;

        GetComponent<networkClientUIbuttons>().discoveryIP.Remove(0, 7);
        GetComponent<networkClientUIbuttons>().discoveryPort = System.Convert.ToInt32(data);
    }
}
