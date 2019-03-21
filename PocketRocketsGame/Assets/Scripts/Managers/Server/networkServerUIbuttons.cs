using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.SceneManagement;

public class networkServerUIbuttons : NetworkManager {

    public static networkServerUIbuttons networkServer;
    public AudioClip tankFireClip;

    
    private AudioSource networkSource;

    public int sceneIndex = 1;
    bool gameStart = false;

    public int playersAmount = 0;

    public List<int> playerConnectionID = new List<int> { -1, -1, -1, -1 };
    // int[] playerConnectionID = new int[4] { -1, -1, -1, -1 };
    public List<int> playerIDTest = new List<int> { 1, 2, 3, 4 };
    public List<int> playersConnected = new List<int> { 0, 0, 0, 0 };

    public List<int>  readyClientsTest = new List<int>{ -1, -1, -1, -1 };
    //public List<bool>  readyClientsTest = new List<bool>(); //for storing client ready states
  
    public List<int> playerVehiclesTest = new List<int> { 9, 9, 9, 9 };
    

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

        maxDelay = 0.4f;

    
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


        NetworkManager.singleton.networkPort = System.Convert.ToInt32(GameObject.Find("Canvas/mainMenuPanel/portNumber").GetComponent<InputField>().text);


        //start listening on the inputted port number
        //NetworkServer.Listen(System.Convert.ToInt32(GameObject.Find("Canvas/mainMenuPanel/portNumber").GetComponent<InputField>().text));

        //The data that will be broadcasted to other network discvovery scripts
        GetComponent<networkDiscoveryServer>().broadcastData = GameObject.Find("Canvas/mainMenuPanel/portNumber").GetComponent<InputField>().text;

        GetComponent<networkDiscoveryServer>().init();


        NetworkManager.singleton.StartServer(config, 4);
      
    }


    public override void OnServerConnect(NetworkConnection conn)
    {
        //Debug.Log("someone connected and id is " + conn.connectionId);
        sendPlayerID(conn);
        base.OnServerConnect(conn);
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Debug.Log("player with id " + conn.connectionId + " has disconnected");

        for (int i =0; i < 4; i++)
        {
            if (playerConnectionID[i] == conn.connectionId)
            {
                playerConnectionID[i] = -1;
                readyClientsTest[i] = -1;
                playerVehiclesTest[i] = 1;
                playersConnected[i] = 0;
            }
        }




        base.OnServerDisconnect(conn);
    }
    public void hostGameRemake()
    {

    }
    public List<int> getReadyClient()
    {
        return  readyClientsTest;
    }
    public void hostGame()
    {
        //if ( readyClientsTest.Contains())
        //{
        //    // say clients not ready code
        //}
        //else
        //{
           // if (playerID > 1)
           // {
                sceneIndex = 2;
                SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
                UnloadScene(1);

        GetComponent<networkDiscoveryServer>().StopBroadcast();
        singleton.maxConnections = getPlayerAmount();
        sendStartGame(); // send a message to all cients which will change their scene
           // }
   
       // }   
    }

    public void restartGame()
    {
        sceneIndex = 1;
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        UnloadScene(2);

        for (int i = 0; i < 4; i++)
        {
            if (playersConnected[i] == 1)
            {
                readyClientsTest[i] = 0;
            }
        }

        GetComponent<networkDiscoveryServer>().StartAsServer();
        sendRestartGame(); // send a message to all cients which will change their scene
    }

    public int getPlayerAmount()
    {
        int playerAmount = 0;
        for (int i = 0; i < 4; i++)
        {
            if (playersConnected[i] == 1)
                playerAmount++;
        }
        return playerAmount;
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
        GUI.Box(new Rect(30, Screen.height - 100, 400, 150), "Debug Info");
        GUI.Label(new Rect(20, Screen.height - 85, 100, 20), "Status:" + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 60, 100, 20), "player cars" + ( playerVehiclesTest.Count ));

        GUI.Label(new Rect(20, Screen.height - 45, 300, 20), "Player 1 Speed Stack: " + GameObject.Find("Player " + 1).GetComponent<PlayerStats>().getStackingSpeedBuff());
        GUI.Label(new Rect(20, Screen.height - 30, 300, 20), "Player 2 Speed Stack: " + GameObject.Find("Player " + 2).GetComponent<PlayerStats>().getStackingSpeedBuff());
        GUI.Label(new Rect(20, Screen.height - 15, 300, 20), "Player 3 Speed Stack: " + GameObject.Find("Player " + 3).GetComponent<PlayerStats>().getStackingSpeedBuff());
        GUI.Label(new Rect(20, Screen.height - 0, 300, 20), "Player 4 Speed Stack: " + GameObject.Find("Player " + 4).GetComponent<PlayerStats>().getStackingSpeedBuff());

        //GameObject.Find("Player " + playerID).GetComponent<Immunity>()
        //GUI.Label(new Rect(20, Screen.height - 45, 100, 20), "PlayerID:" + playerID);
        //GUI.Label(new Rect(20, Screen.height - 85, 100, 20), "player 1" + playerVehiclesTest[0]);
        //GUI.Label(new Rect(20, Screen.height - 60, 100, 20), "player 2" + playerVehiclesTest[1]);
        //GUI.Label(new Rect(20, Screen.height - 45, 100, 20), "player 3" + playerVehiclesTest[1]);
        //GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Ready:" +  readyClientsTest[0]);
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
                GameObject.Find("Gate" + gateNo + "/JellyTotTrap" + playerTarget).GetComponent<MeshRenderer>().enabled = false;
                networkSource.PlayOneShot(tankFireClip);
                Debug.Log("Fire Tank!!");

                break;
            case 2://Bubble
                //GameObject.Find("Player " + playerTarget).GetComponentInChildren<NessieBubble>().CreateBubble(GameObject.Find("Gate" + gateNo), playerTarget);
                GameObject.Find("Gate" + gateNo + "/NessieBubbleTrap" + playerTarget).GetComponent<SphereCollider>().enabled = true;
                GameObject.Find("Gate" + gateNo + "/NessieBubbleTrap" + playerTarget).GetComponent<MeshRenderer>().enabled = false;
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
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                if (playersConnected[i] == 1)
                {
                    index++;
                }
            }
            msg.value = index;
            
            NetworkServer.SendToAll(120, msg);
        }
    }

    void sendRestartGame()
    {
        //if (NetworkServer.active)
        //{
            IntegerMessage msg = new IntegerMessage();
            msg.value = playersAmount;
            NetworkServer.SendToAll(125, msg);
       // }
    }

    private void serverReceiveRequestID(NetworkMessage message)
    {
        Debug.Log(message.conn.connectionId);
        //sendPlayerID(message.conn);
    }

    public void sendSwitchStateL()
    {
        string[] switchStates = new string[4];
        StringMessage msg = new StringMessage();

        //for (int x = 1; x < playerID + 1; x++)
        //{
        //    switchStates[x - 1] = GameObject.Find("Player " + x).GetComponent<PlayerStats>().getSwitchStateL().ToString();
        //    msg.value += switchStates[x - 1] + "|";
        //}
        for (int i = 0; i < 4; i++)
        {
            if (playersConnected[i] == 1)
            {
                switchStates[i] = GameObject.Find("Player " + (i + 1)).GetComponent<PlayerStats>().getSwitchStateL().ToString();
                msg.value += switchStates[i] + "|";
            }
        }
        NetworkServer.SendToAll(126, msg);
    }

    public void sendSwitchStateR()
    {
        string[] switchStates = new string[4];
        StringMessage msg = new StringMessage();

        for (int i = 0; i < 4; i++)
        {
            if (playersConnected[i] == 1)
            {
                switchStates[i] = GameObject.Find("Player " + (i + 1)).GetComponent<PlayerStats>().getSwitchStateR().ToString();
                msg.value += switchStates[i] + "|";
            }
        }

        NetworkServer.SendToAll(127, msg);
    }

    private void sendPlayerID(NetworkConnection conn)
    {
        IntegerMessage msg = new IntegerMessage();
     
        if (!playersConnected.Contains(0))
        {
            conn.Disconnect();
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (playersConnected[i] == 0)
                {

                    msg.value = playerIDTest[i];
                    NetworkServer.SendToClient(conn.connectionId, 121, msg);

                     readyClientsTest[i] = 0;
                    // readyClientsTest.Add(false);
                    //playerVehiclesTest.Add(1);
                    playerConnectionID[i] = conn.connectionId;
                    playersConnected[i] = 1;
                    break;

                }

            }
        }
        
        
      
        


    }

    public void sendPosition()
    {
        int[] playerPositions = new int[4];
        StringMessage msg = new StringMessage();

        //for (int x = 1; x < playerID + 1; x++)
        //{
        //    playerPositions[x - 1] = GameObject.Find("Player " + x).GetComponent<PlayerStats>().getPosition();
        //    msg.value += playerPositions[x - 1] + "|";
        //}
        for (int i = 0; i < 4; i++)
        {
            if (playersConnected[i] == 1)
            {
                playerPositions[i] = GameObject.Find("Player " + (i + 1)).GetComponent<PlayerStats>().getPosition();
                msg.value += playerPositions[i] + "|";
            }
        }
        NetworkServer.SendToAll(124, msg);
    }

    public void sendPoints_Gate()
    {
        
        int[] playerPoints = new int[4];
        int[] nextGate = new int[4];

        StringMessage msg = new StringMessage();
        StringMessage gateMsg = new StringMessage();
        // get the points for each player
    
        //for (int x = 1; x < playerID + 1; x++)
        //{
        //    nextGate[x - 1] = GameObject.Find("Player " + x).GetComponent<PlayerStats>().getNextGate();
        //    gateMsg.value += nextGate[x - 1] + "|";
        //}
        for (int i = 0; i < 4; i++)
        {
            if (playersConnected[i] == 1)
            {
                nextGate[i] = GameObject.Find("Player " + (i + 1)).GetComponent<PlayerStats>().getNextGate();
                gateMsg.value += nextGate[i] + "|";
            }
        }

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
         readyClientsTest[System.Convert.ToInt16(ready_playerID_vehicle_PlayerName[1]) - 1] = System.Convert.ToInt16(ready_playerID_vehicle_PlayerName[0]);
        playerVehiclesTest[System.Convert.ToInt16(ready_playerID_vehicle_PlayerName[1]) - 1] = System.Convert.ToInt16(ready_playerID_vehicle_PlayerName[2]);


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
