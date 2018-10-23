using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class networkServerUIbuttons : MonoBehaviour {


    //BasicTrap referencedScript = GetComponent<BasicTrap>();
    public BasicTrap basicTrap;
    public GameObject car1;


    bool debug1;
    bool debug2;
    // Use this for initialization
    void Start()
    {
        //start listening on port 25000
        NetworkServer.Listen(63192);
        NetworkServer.RegisterHandler(112, serverReceiveButton1);
        NetworkServer.RegisterHandler(113, serverReceiveButton2);
        basicTrap = GameObject.FindObjectOfType(typeof(BasicTrap)) as BasicTrap;
        

    }
    void OnGUI()
    {
        GUI.Box(new Rect(10, Screen.height - 100, 200, 100), "Debug Info");
        GUI.Label(new Rect(20, Screen.height - 85, 100, 20), "Status:" + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 60, 100, 20), "Connected:" + (NetworkServer.connections.Count));
        GUI.Label(new Rect(20, Screen.height - 45, 100, 20), "Button_1" + basicTrap.getTrigger());
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Button_2:" + debug2);
    }

    


	//2 functions responsible for receiving button information
    private void serverReceiveButton1(NetworkMessage message)
    {
        IntegerMessage msg = new IntegerMessage();
        if (basicTrap.getTrigger() == false)
        {
            basicTrap.setTrigger();
        }
        debug1 = System.Convert.ToBoolean(message.ReadMessage<IntegerMessage>().value);
    }
    private void serverReceiveButton2(NetworkMessage message)
    {
        IntegerMessage msg = new IntegerMessage();
        debug2 = System.Convert.ToBoolean(message.ReadMessage<IntegerMessage>().value);
    }
    // Update is called once per frame
    void Update () {
       
    }
}
