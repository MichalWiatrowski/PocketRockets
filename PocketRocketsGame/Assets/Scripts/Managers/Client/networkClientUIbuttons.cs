using System.Collections;
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

    int playerID = 0; //keeps the ID of the clients player ID on the server e.g. Player 1 or 2 etc
    int amountOfPlayers = 1; //keeps the track of how many players there are in the game

    int powerUp = 0;
    int trap = 0;

    private bool clientReady = false;
    // Use this for initialization
    void Start()
    {
        client = new NetworkClient();


        //client.RegisterHandler(121, clientReceiveMaxPlayers);
        client.RegisterHandler(120, clientReceiveStartGame);
        client.RegisterHandler(121, clientReceivePlayerID);
        
    }
    public int getPlayerID()
    {
        return playerID;
    }
    public int getAmountOfPlayers()
    {
        return amountOfPlayers;
    }
   public void joinGame()
    {
        //create a config, add qos channels, then configure the client to have 1 max connection; the server
        ConnectionConfig config = new ConnectionConfig();
        config.AddChannel(QosType.ReliableSequenced);
        config.AddChannel(QosType.Unreliable);
        client.Configure(config, 1);

        //connect to the server
        
        Connect(GameObject.Find("Canvas/joinGamePanel/IPaddress").GetComponent<InputField>().text, System.Convert.ToInt32(GameObject.Find("Canvas/joinGamePanel/portNumber").GetComponent<InputField>().text));
        //Connect("193.60.172.195", 50828); //for quick debugging
    }
    //this will load the first scene of the mobile side of the game "Menu"
    void Awake()
    {
        if (!gameStart)
        {
            networkClient = this;
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            gameStart = true;
        }
    }


    void OnGUI()
    {
       
        if (sceneIndex == 1)
        {
            if (!client.isConnected)
            {
                
                //if (GUI.Button(new Rect(10, 10, 100, 70), "Connect"))
                //{
                //    joinGame();
                //}
            }
            else
            {
                    //if (GUI.Button(new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 50, 200, 100), "Previous"))
                    //{
                    //    //go to previous car selection

                    //}
                    //if (GUI.Button(new Rect((Screen.width / 2) + 150, (Screen.height / 2) - 50, 200, 100), "Next"))
                    //{
                    //    // go to the next car selection
                    //}             
            }
        }
        
        GUI.Box(new Rect(10, Screen.height - 80, 100, 80), "Debug Info");
        GUI.Label(new Rect(20, Screen.height - 60, 100, 20), "Status:" + client.isConnected);
        GUI.Label(new Rect(20, Screen.height - 40, 100, 20), "PlayerID:" + playerID);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "ready:" + System.Convert.ToInt16(clientReady));
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
    
    public void toggleReady()
    {
        clientReady = !clientReady;
    }

    public bool getReady()
    {
        return clientReady;
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

    public void sendReadyUp()
    {
        StringMessage msg = new StringMessage();
        msg.value = System.Convert.ToInt16(clientReady) + "|" + playerID;
       // msg.value = 1 + "|" + playerID;
        client.Send(133, msg);
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


   
    //ask the server for the player ID after 2 seconds, lets the client to connect to the server first
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
