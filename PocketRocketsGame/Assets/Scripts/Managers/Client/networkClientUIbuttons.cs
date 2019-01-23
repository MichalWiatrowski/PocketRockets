﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.SceneManagement;


public class networkClientUIbuttons : MonoBehaviour {

    NetworkClient client;
    public static networkClientUIbuttons networkClient;


    int sceneIndex = 1;
    int menuIndex = 1;

    bool gameStart = false; //keeps track of when to start the game

    bool testing = true;

    string testIP = "193.60.172.113";
    int testPortNum = 52047;

   
    public int vehicleChoice = 0;
    

    int powerUp = 0;
    int trap = 0;

    private bool clientReady = false;
    private string name;
    private int playerID = 0; //keeps the ID of the clients player ID on the server e.g. Player 1 or 2 etc (Set to two for testing nessies ability)
    private int amountOfPlayers = 0; //keeps the track of how many players there are in the game
    private int points = 0;
    private int nextGate = 1;

    //this will load the first scene of the mobile side of the game "Menu"
    void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {
        client = new NetworkClient();

        //Register handlers
        client.RegisterHandler(120, clientReceiveStartGame);
        client.RegisterHandler(121, clientReceivePlayerID);
        client.RegisterHandler(122, clientReceivePoints);
        client.RegisterHandler(123, clientReceiveNextGate);

        if (!gameStart)
        {
            networkClient = this;
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            gameStart = true;
        }
    }

    public int getPlayerID()
    {
        return playerID;
    }
    public int getAmountOfPlayers()
    {
        return amountOfPlayers;
    }
    public int getPoints()
    {
        return points;
    }

    public int getNextGate()
    {
        return nextGate;
    }

    public void joinGame()
    {
        //create a config, add qos channels, then configure the client to have 1 max connection; the server
        ConnectionConfig config = new ConnectionConfig();
        config.AddChannel(QosType.ReliableSequenced);
        config.AddChannel(QosType.Unreliable);
        client.Configure(config, 1);

        //connect to the server
        if (testing == false)
        {
            Connect(GameObject.Find("Canvas/joinGamePanel/IPaddress").GetComponent<InputField>().text, System.Convert.ToInt32(GameObject.Find("Canvas/joinGamePanel/portNumber").GetComponent<InputField>().text));
        }
        else {
            Connect(testIP, testPortNum);
        }

      
    }
   


    void OnGUI()
    {
        GUI.Box(new Rect(10, Screen.height - 100, 100, 160), "Debug Info");

        GUI.Label(new Rect(20, Screen.height - 80, 100, 20), "Status:" + client.isConnected);
        GUI.Label(new Rect(20, Screen.height - 60, 100, 20), "PlayerID:" + playerID);
        GUI.Label(new Rect(20, Screen.height - 40, 100, 20), "ready:" + System.Convert.ToInt16(clientReady));
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "points:" + points);
    }

    void Connect(string IP, int portNumber)
    {
        client.Connect(IP, portNumber);
        StartCoroutine(askPlayerID());
    }

    public void setPowers(int p, int t)
    {
        powerUp = p;
        trap = t;
    }
    public int getTrap()
    {
        return trap;
    }
    public int getPowerUp()
    {
        return powerUp;
    }

    public void setName()
    {
        name = GameObject.Find("Canvas/joinGamePanel/Name").GetComponent<InputField>().text;
    }

    public string getName()
    {
        return name;
    }

    public void toggleReady()
    {
        clientReady = !clientReady;
    }

    public bool getReady()
    {
        return clientReady;
    }

    public int getVehicleChoice()
    {
        return vehicleChoice;
    }

    public void sendActivateTrap(int gate, int playerIDchoice, int trapChoice)
    {

        StringMessage msg = new StringMessage();
        msg.value = playerIDchoice + "|" + gate + "|" + trapChoice;
        client.Send(130, msg);
    }
    public void sendActivatePowerUP(int powerUP, int playerID)
    {
        StringMessage msg = new StringMessage();
        msg.value = playerID + "|" + powerUP;
        client.Send(131, msg);
    }
    public void sendActivateVehicleAbiltiy(int gate, int playerIDchoice, int abilityChoice)
    {

        StringMessage msg = new StringMessage();
        msg.value = playerID + "|" + playerIDchoice + "|" + gate + "|" + abilityChoice;
        client.Send(136, msg);
    }

    public void removePoints(int ammount)
    {
        points -= ammount;
        StringMessage msg = new StringMessage();
        msg.value = playerID + "|" + ammount;
        client.Send(134, msg);
    }

   

    public void sendReadyUp(int ready)
    {
        StringMessage msg = new StringMessage();
        msg.value = ready + "|" + playerID + '|' + vehicleChoice + '|' + name;
        client.Send(133, msg);
        sendName();
    }

    public void sendName()
    {
        StringMessage msg = new StringMessage();
        msg.value = playerID + "|" + name;
        client.Send(135, msg);
    }

    private void clientReceivePlayerID(NetworkMessage message)
    {
        IntegerMessage msg = new IntegerMessage();
        msg = message.ReadMessage<IntegerMessage>();
        
        playerID = msg.value;

        if (playerID <= 4)
        {
            GameObject.Find("sceneManager").GetComponent<clientMenuManager>().joinGamePanel.SetActive(false);
            GameObject.Find("sceneManager").GetComponent<clientMenuManager>().powerUpSelectionPanel.SetActive(true);
        }
        else
        {
            client.Disconnect();
        }
    }
    
    private void clientReceiveStartGame(NetworkMessage message)
    {
        IntegerMessage msg = new IntegerMessage();
        msg = message.ReadMessage<IntegerMessage>();
        amountOfPlayers = msg.value;

        sceneIndex = 2;

        //menuIndex = 2;

        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        UnloadScene(1);
    }

    private void clientReceivePoints(NetworkMessage message)
    {
        // get points from server
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        // split into an array of each players
        string[] points1_points2_points3_points4_nextGate1_2_3_4 = msg.value.Split('|');

        points = System.Convert.ToInt16(points1_points2_points3_points4_nextGate1_2_3_4[playerID - 1]);
        //nextGate = System.Convert.ToInt16(points1_points2_points3_points4_nextGate1_2_3_4[(playerID - 1 - amountOfPlayers) + 3]);

    }
    private void clientReceiveNextGate(NetworkMessage message)
    {
        // get points from server
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        // split into an array of each players
        string[] points1_points2_points3_points4_nextGate1_2_3_4 = msg.value.Split('|');

        //points = System.Convert.ToInt16(points1_points2_points3_points4_nextGate1_2_3_4[playerID - 1]);
        nextGate = System.Convert.ToInt16(points1_points2_points3_points4_nextGate1_2_3_4[playerID - 1]);

    }
    //ask the server for the player ID after 2 seconds, will add 'loading icon' lets the client to connect to the server first
    private IEnumerator askPlayerID()
    {
        yield return new WaitForSeconds(2.0f);
        IntegerMessage msg = new IntegerMessage();
        client.Send(132, msg);
    }

    public void UnloadScene(int scene)
    {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(int scene)
    {
        yield return null;

        SceneManager.UnloadSceneAsync(scene);
    }

    // Update is called once per frame
    void Update () {
    }
}
