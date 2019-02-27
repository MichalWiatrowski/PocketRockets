using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.SceneManagement;


public class networkClientUIbuttons : NetworkDiscovery {

    public NetworkClient client;
    public static networkClientUIbuttons networkClient;


    int sceneIndex = 1;
    int menuIndex = 1;

    bool gameStart = false; //keeps track of when to start the game



    bool testing = false;

    int switchStateL = 1;
    int switchStateR = 1;


    string testIP = "192.168.0.20";
    int testPortNum = 53100;


    string discoveryIP = "";
    int discoveryPort = 0;

    public int vehicleChoice = 0;
    

    int powerUp = 0;
    int trap = 0;

    private bool clientReady = false;
    private string name;
    private int playerID = 0; //keeps the ID of the clients player ID on the server e.g. Player 1 or 2 etc (Set to two for testing nessies ability)
    private int amountOfPlayers = 0; //keeps the track of how many players there are in the game
    private int points = 0;
    private int position = 0;
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
        client.RegisterHandler(124, clientReceivePosition);
        client.RegisterHandler(125, clientReceiveRestartGame);
        client.RegisterHandler(126, clientReceiveSwitchStateL);
        client.RegisterHandler(127, clientReceiveSwitchStateR);

        if (!gameStart)
        {
            networkClient = this;
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            gameStart = true;
        }

        //Init network discovery
        Initialize();
        //Start network discovery as client to search for local servers
        StartAsClient();

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

    public int getPosition()
    {
        return position;
    }

    public int getNextGate()
    {
        return nextGate;
    }

    public int getSwitchStateL()
    {
        return switchStateL;
    }

    public int getSwitchStateR()
    {
        return switchStateR;
    }

    public void setSwitchStateL(int flag) { switchStateL = flag; }
    public void setSwitchStateR(int flag) { switchStateR = flag; }


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
            //Connect through network discovery
            Connect(discoveryIP, discoveryPort);

            //Connect by inputting ip and port into input boxes
            //Connect(GameObject.Find("Canvas/joinGamePanel/IPaddress").GetComponent<InputField>().text, System.Convert.ToInt32(GameObject.Find("Canvas/joinGamePanel/portNumber").GetComponent<InputField>().text));
        }
        else
        {
           Connect(testIP, testPortNum);
        }

      
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("Server Found with IP: " + fromAddress + " and port " + data);
        //string[] lol = fromAddress.Split(':');
       // discoveryIP = lol[1];
        discoveryIP = fromAddress;

        discoveryIP.Remove(0,7);
        discoveryPort = System.Convert.ToInt32(data);
    }


    void OnGUI()
    {
    //    GUI.Box(new Rect(10, Screen.height - 140, 100, 200), "Debug Info");

    //    GUI.Label(new Rect(20, Screen.height - 120, 600, 20), "DiscoveryIP:" + discoveryIP);
    //    GUI.Label(new Rect(20, Screen.height - 100, 300, 20), "DiscoveryPort:" + discoveryPort);

    //    GUI.Label(new Rect(20, Screen.height - 80, 100, 20), "Status:" + client.isConnected);
    //    GUI.Label(new Rect(20, Screen.height - 60, 100, 20), "PlayerID:" + playerID);
    //    GUI.Label(new Rect(20, Screen.height - 40, 100, 20), "ready:" + System.Convert.ToInt16(clientReady));
    //    GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "points:" + points);
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

    public void sendJump()
    {
        IntegerMessage msg = new IntegerMessage();
        msg.value = playerID;
        client.Send(137, msg);
    }

    public void sendActivatePowerUP(int powerUP, int playerID, int abilityChoice)
    {
        StringMessage msg = new StringMessage();
        msg.value = playerID + "|" + powerUP + "|" + abilityChoice;
        client.Send(131, msg);
    }
    public void sendActivateVehicleAbiltiy(int gate, int playerIDchoice, int abilityChoice)
    {
        StringMessage msg = new StringMessage();
        msg.value = playerID + "|" + playerIDchoice + "|" + gate + "|" + abilityChoice;
        client.Send(136, msg);
    }

    public void sendLaneSwitch(string direction)
    {
        StringMessage msg = new StringMessage();
        msg.value = playerID + "|" + direction;
        client.Send(138, msg);
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

    private void clientReceiveSwitchStateL(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        // split into an array of each players
        string[] state1_2_3_4 = msg.value.Split('|');

        switchStateL = System.Convert.ToInt16(state1_2_3_4[playerID - 1]);
    }

    private void clientReceiveSwitchStateR(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        // split into an array of each players
        string[] state1_2_3_4 = msg.value.Split('|');

        switchStateR = System.Convert.ToInt16(state1_2_3_4[playerID - 1]);
    }

    private void clientReceiveRestartGame(NetworkMessage message)
    {
        IntegerMessage msg = new IntegerMessage();
        msg = message.ReadMessage<IntegerMessage>();
        amountOfPlayers = msg.value;

        sceneIndex = 1;

        //menuIndex = 2;

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        UnloadScene(2);
    }

    private void clientReceivePosition(NetworkMessage message)
    {
        // get position from server
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        // split into an array of each players
        string[] position1_2_3_4 = msg.value.Split('|');

        position = System.Convert.ToInt16(position1_2_3_4[playerID - 1]);
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

}
