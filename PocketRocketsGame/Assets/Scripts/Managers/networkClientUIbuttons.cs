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
    bool gameStart = false;


    // Use this for initialization
    void Start()
    {
        client = new NetworkClient();

        client.RegisterHandler(120, clientReceiveStartGame);

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
                if (GUI.Button(new Rect(10, 10, 100, 70), "Connect"))
                {
                    Connect(GameObject.Find("Canvas/IPaddress").GetComponent<InputField>().text, System.Convert.ToInt32(GameObject.Find("Canvas/Port_Number").GetComponent<InputField>().text));

                    GameObject.Find("Canvas/Port_Number").SetActive(false);
                    GameObject.Find("Canvas/IPaddress").SetActive(false);
                }
            }
            else
            {
                    if (GUI.Button(new Rect((Screen.width / 2) - 500, (Screen.height / 2) - 10, 200, 100), "Previous"))
                    {
                        //go to previous car selection

                    }
                    if (GUI.Button(new Rect((Screen.width / 2) + 300, (Screen.height / 2) - 10, 200, 100), "Next"))
                    {
                        // go to the next car selection
                    }             
            }
        }
        
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), "Debug Info");
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Status:" + client.isConnected);
    }

	void Connect(string IP, int portNumber)
    {
        client.Connect(IP, portNumber);
    }

   public void sendGateNumber(int gate)
    {
        //if (client.isConnected)
        //{
            IntegerMessage msg = new IntegerMessage();
            msg.value = gate;
            client.Send(130, msg);
           // Debug.Log(gate);
       // }
    }
    public void sendPlayerID(int playerID)
    {
        //if (client.isConnected)
        //{
        IntegerMessage msg = new IntegerMessage();
        msg.value = playerID;
        client.Send(131, msg);
        // Debug.Log(gate);
        // }
    }




    private void clientReceiveStartGame(NetworkMessage message)
    {
        sceneIndex = 2;
        //menuIndex = 2;
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        UnloadScene(1);
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
