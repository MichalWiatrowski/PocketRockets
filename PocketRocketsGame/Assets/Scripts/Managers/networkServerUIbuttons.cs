using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
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
        NetworkServer.RegisterHandler(130, serverReceiveGateNumber);
        NetworkServer.RegisterHandler(131, serverReceivePlayerID);

        if (!gameStart)
        {
            networkServer = this;
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

            gameStart = true;

        }

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

    void OnGUI()
    {
        if (sceneIndex == 1)
        {
            if (!NetworkServer.active)
            {
                //when the button is clicked, make a server with the port that has been inputted in the inputfield
                if (GUI.Button(new Rect(10, 10, 100, 70), "Connect"))
                {
                    //start listening on the inputted port number
                    NetworkServer.Listen(System.Convert.ToInt32(GameObject.Find("Canvas/Port_Number").GetComponent<InputField>().text));
        
                    //hide the input field
                    GameObject.Find("Canvas/Port_Number").SetActive(false);
                }
            }
            else
            {
                //displays the amount of players connected
                GameObject.Find("Canvas/PlayersConnected").GetComponent<Text>().text = "Players connected:" + System.Convert.ToString(NetworkServer.connections.Count - 1);

                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 35, 100, 70), "Start Game"))
                {
                    sceneIndex = 2;
                    SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
                    UnloadScene(1);
                    sendStartGame(); // send a message to all cients which will change their scene
                }
            }
        }


        

        //Debug information
        GUI.Box(new Rect(10, Screen.height - 100, 200, 100), "Debug Info");
        GUI.Label(new Rect(20, Screen.height - 85, 100, 20), "Status:" + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 60, 100, 20), "Connected:" + (NetworkServer.connections.Count));
    }


    void sendStartGame()
    {
        if (NetworkServer.active)
        {
            IntegerMessage msg = new IntegerMessage();
            msg.value = 2;
            NetworkServer.SendToAll(120, msg);
        }
    }

    
    private void serverReceiveGateNumber(NetworkMessage message)
    {
        IntegerMessage msg = new IntegerMessage();
        msg = message.ReadMessage<IntegerMessage>();
        activateTrap(msg.value);
    }

    private void serverReceivePlayerID(NetworkMessage message)
    {
        IntegerMessage msg = new IntegerMessage();
        msg = message.ReadMessage<IntegerMessage>();
        playerID = msg.value;
    }

    void activateTrap(int trap)
    {
        GameObject.Find("Gate" + trap + "/WallTrap" + playerID).GetComponent<BoxCollider>().enabled = true;
        playerID = 0;
        
    }
    // Update is called once per frame
    void Update () {
       
    }
}
