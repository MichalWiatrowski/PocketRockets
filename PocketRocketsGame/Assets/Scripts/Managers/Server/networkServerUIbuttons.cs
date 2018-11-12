using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.SceneManagement;

public class networkServerUIbuttons : MonoBehaviour {

    public static networkServerUIbuttons networkServer;

    int sceneIndex = 1;
    bool gameStart = false;

    private int playerID = 0;

    List<bool> readyClients = new List<bool>(); //for storing client ready states

    // Use this for initialization
    void Start()
    {
       

    }

    //this will load the first scene of the game "Menu"
    void Awake()
    {
        //register any necessary handlers
        NetworkServer.RegisterHandler(130, serverReceiveActivateTrap);
        NetworkServer.RegisterHandler(131, serverReceiveActivatePowerUP);
        //NetworkServer.RegisterHandler(131, serverReceivePlayerID);
        NetworkServer.RegisterHandler(132, serverReceiveRequestID);
        NetworkServer.RegisterHandler(133, serverReceiveReadyUp);


        if (!gameStart)
        {
            networkServer = this;
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

            gameStart = true;

        }
    }

    public void hostServer()
    {
        ConnectionConfig config = new ConnectionConfig();
        config.AddChannel(QosType.ReliableSequenced);
        config.AddChannel(QosType.Unreliable);
        NetworkServer.Configure(config, 4);
        //start listening on the inputted port number
        NetworkServer.Listen(System.Convert.ToInt32(GameObject.Find("Canvas/mainMenuPanel/portNumber").GetComponent<InputField>().text));
      
       
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
        if (sceneIndex == 1)
        {
            if (!NetworkServer.active)
            {
                ////when the button is clicked, make a server with the port that has been inputted in the inputfield
                //if (GUI.Button(new Rect(10, 10, 100, 70), "Connect"))
                //{
                //   // hostGame();
                //}
            }
            else
            {
                //displays the amount of players connected
               // GameObject.Find("Canvas/PlayersConnected").GetComponent<Text>().text = "Players connected:" + System.Convert.ToString(NetworkServer.connections.Count - 1);

                //if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 35, 100, 70), "Start Game"))
                //{
                //    sceneIndex = 2;
                //    SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
                //    UnloadScene(1);
                //    sendStartGame(); // send a message to all cients which will change their scene
                //}
            }
        }


        

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

    void activatePowerUP(int player, int powerUP)
    {
        if (powerUP == 1)
        GameObject.Find("Player " + player).GetComponent<PortalPowerUp>().CreatePortals();
        else if (powerUP == 2)
        GameObject.Find("Player " + player).GetComponent<Immunity>().activateImmunity();

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

    private void serverReceiveRequestID(NetworkMessage message)
    {
        sendPlayerID();
    }
    private void sendPlayerID()
    {
        playerID++;
        IntegerMessage msg = new IntegerMessage();
            msg.value = playerID;
            
            NetworkServer.SendToClient(playerID, 121, msg);
        readyClients.Add(false);

        if (playerID > 4)
            playerID = 4;
    }

    public void sendPoints()
    {
        int[] playerPoints = new int[4];

        StringMessage msg = new StringMessage();

        // get the points for each player
        for (int x = 0; x < 4; x++)
        {
            playerPoints[x] = GameObject.Find("Player " + x + 1).GetComponent<PlayerStats>().points;
        }

        // send all the players points to all the players
        msg.value = playerPoints[0] + "|" + playerPoints[1] + "|" + playerPoints[2] + "|" + playerPoints[3];

        NetworkServer.SendToAll(122, msg);
    }


    private void serverReceiveActivatePowerUP(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] playerID_power = msg.value.Split('|');
        activatePowerUP(System.Convert.ToInt16(playerID_power[0]), System.Convert.ToInt16(playerID_power[1]));
    }

    private void serverReceiveReadyUp(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] ready_playerID = msg.value.Split('|');
        readyClients[System.Convert.ToInt16(ready_playerID[1]) - 1] = System.Convert.ToBoolean(System.Convert.ToInt16(ready_playerID[0]));
    }

    private void serverReceiveActivateTrap(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] playerID_gateNO_trap = msg.value.Split('|');

        activateTrap(System.Convert.ToInt16(playerID_gateNO_trap[0]), System.Convert.ToInt16(playerID_gateNO_trap[1]), System.Convert.ToInt16(playerID_gateNO_trap[2]));
    }

   

    // Update is called once per frame
    void Update () {
    }
}
