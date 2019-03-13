using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
public class serverNetworkDiscovery : NetworkDiscovery {

	// Use this for initialization
	void Start () {
       // Init the network discovery
        Initialize();

        
    }
	public void Init()
    {
        //Start this script as a server
        StartAsServer();
    }
}
