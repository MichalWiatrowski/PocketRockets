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
        
        sceneIndex = 2;
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        UnloadScene(1);
        sendStartGame(); // send a message to all cients which will change their scene
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
    }

    void activateTrap(int playerIDchoice, int trap)
    {
        GameObject.Find("Gate" + trap + "/WallTrap" + playerIDchoice).GetComponent<BoxCollider>().enabled = true;
    }

    void activatePowerUP(int player, int powerUP)
    {
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
            
        if (playerID > 4)
            playerID = 4;
    }


    private void serverReceiveActivatePowerUP(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] playerID_gateNO = msg.value.Split('|');
        activatePowerUP(System.Convert.ToInt16(playerID_gateNO[0]), System.Convert.ToInt16(playerID_gateNO[1]));
    }




    private void serverReceiveActivateTrap(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] playerID_gateNO = msg.value.Split('|');

        activateTrap(System.Convert.ToInt16(playerID_gateNO[0]), System.Convert.ToInt16(playerID_gateNO[1]));
    }

   

    // Update is called once per frame
    void Update () {
       
    }
}
