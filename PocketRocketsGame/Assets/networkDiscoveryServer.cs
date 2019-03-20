using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
public class networkDiscoveryServer : NetworkDiscovery{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void init()
    {
        //Init network discovery
        Initialize();
        //Start network discovery as client to search for local servers
        StartAsServer();
    }
}
