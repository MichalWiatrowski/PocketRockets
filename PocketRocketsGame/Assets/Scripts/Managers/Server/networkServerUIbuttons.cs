using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.SceneManagement;

public class networkServerUIbuttons : NetworkDiscovery {

    public static networkServerUIbuttons networkServer;
    public AudioClip tankFireClip;

    
    private AudioSource networkSource;

    public int sceneIndex = 1;
    bool gameStart = false;

    public int playerID = 0;

    List<bool> readyClients = new List<bool>(); //for storing client ready states
  
    public List<int> playerVehicles = new List<int>();


    // Use this for initialization
    void Start()
    {
        //Allows the app to run while in the background/minimised
        Application.runInBackground = true;

        networkSource = GetComponent<AudioSource>();
        //register any necessary handlers
        NetworkServer.RegisterHandler(130, serverReceiveActivateTrap);
        NetworkServer.RegisterHandler(131, serverReceiveActivatePowerUP);
        //NetworkServer.RegisterHandler(131, serverReceivePlayerID);
        NetworkServer.RegisterHandler(132, serverReceiveRequestID);
        NetworkServer.RegisterHandler(133, serverReceiveReadyUp);
        NetworkServer.RegisterHandler(134, serverReceiveRemovePoints);
        NetworkServer.RegisterHandler(135, serverReceiveName);
        NetworkServer.RegisterHandler(136, serverReceiveVehicleAbility);
        NetworkServer.RegisterHandler(137, serverReceiveJump);
        NetworkServer.RegisterHandler(138, serverReceiveLaneSwitch);

        if (!gameStart)
        {
            networkServer = this;
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

            gameStart = true;

        }

    }

    public void hostServer()
    {
        //Create config file for the network server
        ConnectionConfig config = new ConnectionConfig();
        config.AddChannel(QosType.ReliableSequenced);
        config.AddChannel(QosType.Unreliable);
        NetworkServer.Configure(config, 4);




        

        //start listening on the inputted port number
        NetworkServer.Listen(System.Convert.ToInt32(GameObject.Find("Canvas/mainMenuPanel/portNumber").GetComponent<InputField>().text));

        //The data that will be broadcasted to other network discvovery scripts
        broadcastData = GameObject.Find("Canvas/mainMenuPanel/portNumber").GetComponent<InputField>().text;
        //Init the network discovery
        Initialize();

        //Start this script as a server
        StartAsServer();

    }
    public void hostGameRemake()
    {

    }
    public List<bool> getReadyClient()
    {
        return readyClients;
    }
    public void hostGame()
    {
        if (readyClients.Contains(false))
        {
            // say clients not ready code
        }
        else
        {
            if (playerID > 1)
            {
                sceneIndex = 2;
                SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
                UnloadScene(1);

                sendStartGame(); // send a message to all cients which will change their scene
            }
   
        }   
    }

    public void restartGame()
    {
        sceneIndex = 1;
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        UnloadScene(2);

        sendRestartGame(); // send a message to all cients which will change their scene
    }

    public int getPlayerAmount()
    {
        return playerID;
    }
    
 
    //function and ienumerator for unloading scenes
    public void UnloadScene(int scene)
    {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(int scene)
    {
        yield return null;

        SceneManager.UnloadSceneAsync(scene);
    }

    void OnGUI()
    {
        //Debug information
        GUI.Box(new Rect(10, Screen.height - 100, 200, 100), "Debug Info");
        GUI.Label(new Rect(20, Screen.height - 85, 100, 20), "Status:" + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 60, 100, 20), "Connected:" + (NetworkServer.connections.Count));
        GUI.Label(new Rect(20, Screen.height - 45, 100, 20), "PlayerID:" + playerID);
        //GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Ready:" + readyClients[0]);
    }

    void activateTrap(int playerIDchoice, int gateNo, int trapChoice)
    {
        if (trapChoice == 1)
        GameObject.Find("Gate" + gateNo + "/FreezeTrap" + playerIDchoice).GetComponent<BoxCollider>().enabled = true;
        else if (trapChoice == 2)
        GameObject.Find("Gate" + gateNo + "/WallTrap" + playerIDchoice).GetComponent<BoxCollider>().enabled = true;
    }


    void activatePowerUP(int player, int powerUP, int vehicleID)
    {
        string vehicle = "";

        if (vehicleID == 1)
            vehicle = "CupcakeTank";
        else if (vehicleID == 2)
            vehicle = "DinoCar";
        else if (vehicleID == 3)
            vehicle = "Bathtub1";

            if (powerUP == 1)
                GameObject.Find("Player " + player).GetComponent<PortalPowerUp>().CreatePortals();

            else if (powerUP == 2)
                GameObject.Find("Player " + player).GetComponent<Immunity>().activateImmunity();

    }

    void activateJump(int playerID)
    {
        Debug.Log(playerID);
        GameObject.Find("Player " + playerID).GetComponent<VehicleJump>().Jump();
    }

    void activateLaneSwitch(int playerID, string direction)
    {
        if (direction == "Left")
        {
            GameObject.Find("Player " + playerID).GetComponent<Move>().MoveLeft();
        }
        else if (direction == "Right")
        {
            GameObject.Find("Player " + playerID).GetComponent<Move>().MoveRight();
        }
    }

    void activateVehicleAbility(int playerID, int playerTarget, int gateNo, int abilityChoice) {

        switch (abilityChoice) {
            case 1://Jelly Tot
                //Vehicle ability acvivation goes here for Cup Cake Tank
                GameObject.Find("Gate" + gateNo + "/JellyTotTrap" + playerTarget).GetComponent<BoxCollider>().enabled = true;
                GameObject.Find("Gate" + gateNo + "/JellyTotTrap" + playerTarget).GetComponent<MeshRenderer>().enabled = true;
                networkSource.PlayOneShot(tankFireClip);
                Debug.Log("Fire Tank!!");

                break;
            case 2://Bubble
                //GameObject.Find("Player " + playerTarget).GetComponentInChildren<NessieBubble>().CreateBubble(GameObject.Find("Gate" + gateNo), playerTarget);
                GameObject.Find("Gate" + gateNo + "/NessieBubbleTrap" + playerTarget).GetComponent<SphereCollider>().enabled = true;
                GameObject.Find("Gate" + gateNo + "/NessieBubbleTrap" + playerTarget).GetComponent<MeshRenderer>().enabled = true;
                Debug.Log("Create Bubble");
                break;
            case 3://Speed Boost
                //Vehicle ability acvivation goes here for Bath Tub
                GameObject.Find("Player " + playerID).GetComponent<SpeedBoost>().activateSpeedBoost();
                break;
            case 4://Immunity
                //Vehicle ability acvivation goes here for Crown
                GameObject.Find("Player " + playerID).GetComponent<Immunity>().activateImmunity();
                break;

        }
    }





    ////////////////////////////////////Network Messages

    void sendStartGame()
    {
        if (NetworkServer.active)
        {
            IntegerMessage msg = new IntegerMessage();
            msg.value = playerID;
            NetworkServer.SendToAll(120, msg);
        }
    }

    void sendRestartGame()
    {
        //if (NetworkServer.active)
        //{
            IntegerMessage msg = new IntegerMessage();
            msg.value = playerID;
            NetworkServer.SendToAll(125, msg);
       // }
    }

    private void serverReceiveRequestID(NetworkMessage message)
    {
        sendPlayerID();
    }

    public void sendSwitchStateL()
    {
        string[] switchStates = new string[playerID];
        StringMessage msg = new StringMessage();

        for (int x = 1; x < playerID + 1; x++)
        {
            switchStates[x - 1] = GameObject.Find("Player " + x).GetComponent<PlayerStats>().getSwitchStateL().ToString();
            msg.value += switchStates[x - 1] + "|";
        }

        NetworkServer.SendToAll(126, msg);
    }

    public void sendSwitchStateR()
    {
        string[] switchStates = new string[playerID];
        StringMessage msg = new StringMessage();

        for (int x = 1; x < playerID + 1; x++)
        {
            switchStates[x - 1] = GameObject.Find("Player " + x).GetComponent<PlayerStats>().getSwitchStateR().ToString();
            msg.value += switchStates[x - 1] + "|";
        }

        NetworkServer.SendToAll(127, msg);
    }

    private void sendPlayerID()
    {
        playerID++;
        IntegerMessage msg = new IntegerMessage();
            msg.value = playerID;
            
            NetworkServer.SendToClient(playerID, 121, msg);
        readyClients.Add(false);
        playerVehicles.Add(1);

        if (playerID > 4)
            playerID = 4;
    }

    public void sendPosition()
    {
        int[] playerPositions = new int[playerID];
        StringMessage msg = new StringMessage();

        for (int x = 1; x < playerID + 1; x++)
        {
            playerPositions[x - 1] = GameObject.Find("Player " + x).GetComponent<PlayerStats>().getPosition();
            msg.value += playerPositions[x - 1] + "|";
        }

        NetworkServer.SendToAll(124, msg);
    }

    public void sendPoints_Gate()
    {
        
        int[] playerPoints = new int[playerID];
        int[] nextGate = new int[playerID];

        StringMessage msg = new StringMessage();
        StringMessage gateMsg = new StringMessage();
        // get the points for each player
        for (int x = 1; x < playerID + 1; x++)
        {
            //playerPoints[x - 1] = GameObject.Find("Player " + x).GetComponent<PlayerStats>().points;
           // msg.value += playerPoints[x - 1] + "|";
        }

        for (int x = 1; x < playerID + 1; x++)
        {
            nextGate[x - 1] = GameObject.Find("Player " + x).GetComponent<PlayerStats>().getNextGate();
            gateMsg.value += nextGate[x - 1] + "|";
        }

            //NetworkServer.SendToAll(122, msg);
        NetworkServer.SendToAll(123, gateMsg);
    }

    private void serverReceiveActivateTrap(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] playerID_gateNO_trap = msg.value.Split('|');

        activateTrap(System.Convert.ToInt16(playerID_gateNO_trap[0]), System.Convert.ToInt16(playerID_gateNO_trap[1]), System.Convert.ToInt16(playerID_gateNO_trap[2]));
    }

    private void serverReceiveActivatePowerUP(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] playerID_power = msg.value.Split('|');
        activatePowerUP(System.Convert.ToInt16(playerID_power[0]), System.Convert.ToInt16(playerID_power[1]), System.Convert.ToInt16(playerID_power[2]));
    }

    private void serverReceiveVehicleAbility(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] vehicleAbility_ = msg.value.Split('|');
        activateVehicleAbility(System.Convert.ToInt16(vehicleAbility_[0]), System.Convert.ToInt16(vehicleAbility_[1]), System.Convert.ToInt16(vehicleAbility_[2]), System.Convert.ToInt16(vehicleAbility_[3]));
    }

    private void serverReceiveJump(NetworkMessage message)
    {
        activateJump(message.ReadMessage<IntegerMessage>().value);
    }

    private void serverReceiveLaneSwitch(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] laneSwitch = msg.value.Split('|');
        activateLaneSwitch(System.Convert.ToInt16(laneSwitch[0]), laneSwitch[1]);
    }

    private void serverReceiveReadyUp(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] ready_playerID_vehicle_PlayerName = msg.value.Split('|');
        readyClients[System.Convert.ToInt16(ready_playerID_vehicle_PlayerName[1]) - 1] = System.Convert.ToBoolean(System.Convert.ToInt16(ready_playerID_vehicle_PlayerName[0]));
        playerVehicles[System.Convert.ToInt16(ready_playerID_vehicle_PlayerName[1]) - 1] = System.Convert.ToInt16(ready_playerID_vehicle_PlayerName[2]);


    }

    private void serverReceiveName(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] playerID_name = msg.value.Split('|');
        PlayerStats stats = GameObject.Find("Player " + System.Convert.ToInt16(playerID_name[0])).GetComponent<PlayerStats>();
        stats.setPlayerName(playerID_name[1]);
    }

  

    private void serverReceiveRemovePoints(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] playerID_ammount = msg.value.Split('|');

        PlayerStats stats = GameObject.Find("Player " + System.Convert.ToInt16(playerID_ammount[0])).GetComponent<PlayerStats>();
        //stats.points -= System.Convert.ToInt16(playerID_ammount[1]);
    }

   

    // Update is called once per frame
    void Update () {
       
    }
}
