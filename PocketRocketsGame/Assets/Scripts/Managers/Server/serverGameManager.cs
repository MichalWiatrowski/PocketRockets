using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class serverGameManager : MonoBehaviour {

    private int amountOfPlayers = 0;
    public GameObject mainCamera;
    public AudioClip raceStartClip;
    public bool gameStarted = false;

    public GameObject winnerScreen;
    public GameObject timerText;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private AudioSource gameSource;

    float gameTimer = 0.0f;
    void Awake() {
    gameSource= GetComponent<AudioSource>();

    }
    // Use this for initialization
    void Start()
    {
        //gameSource.PlayOneShot(raceStartClip, 0.1f);
        amountOfPlayers = networkServerUIbuttons.networkServer.getPlayerAmount();

        //make the winner screen not visable by reducing its scale to 0
        winnerScreen.transform.localScale = new Vector3(0, 0, 0);


        for (int i = 0; i < 4; i++)
        {
            if (networkServerUIbuttons.networkServer.playersConnected[i] == 1)
            {
                GameObject temp = GameObject.Find("Player " + (i + 1));
                temp.transform.GetChild(networkServerUIbuttons.networkServer.playerVehiclesTest[i] - 1).gameObject.SetActive(true);
                temp.GetComponent<PlayerStats>().setUpPlayerStats(networkServerUIbuttons.networkServer.playerVehiclesTest[i] - 1);
            }
            else
            {
                GameObject temp = GameObject.Find("Player " + (i + 1));
                temp.SetActive(false);
            }
        }


        Debug.Log("count " + mainCamera.GetComponent<Follow>().cars.Count);
        Debug.Log("amount " + amountOfPlayers);

        for (int i = 4; i > amountOfPlayers; i--)
        {
            winnerScreen.transform.GetChild(0).GetChild(i - 1).gameObject.SetActive(false);
        }

        //setup camera

        //mainCamera.GetComponent<Follow>().cars.RemoveRange(amountOfPlayers, mainCamera.GetComponent<Follow>().cars.Count - amountOfPlayers);
        mainCamera.GetComponent<Follow>().cars.RemoveRange(0, 4);
        for (int i = 0; i < 4; i++)
        {
            if (networkServerUIbuttons.networkServer.playersConnected[i] == 1)
            {
                mainCamera.GetComponent<Follow>().cars.Add(GameObject.Find("Player " + (i + 1)).transform);
            }

        }

        for (int i = 0; i < 4; i++)
        {
            if (networkServerUIbuttons.networkServer.playersConnected[i] == 1)
            {
                GameObject.Find("Player " + (i + 1)).GetComponent<PlayerStats>().setPlayerName(networkServerUIbuttons.networkServer.playerNames[i]);
              
            }
        }
    }

    public void restartGame()
    {
        networkServerUIbuttons.networkServer.restartGame();
    }
    // Update is called once per frame
    void Update () {

        if (gameStarted == true)
        {
            for (int i = 0; i < 4; i++)
            {
                if (networkServerUIbuttons.networkServer.playersConnected[i] == 1)
                {
                    GameObject.Find("Player " + (i + 1)).GetComponent<PlayerStats>().setPlayerTime(Time.deltaTime);

                }
            }
            gameTimer += Time.deltaTime;
            timerText.GetComponent<Text>().text = "Time: " + gameTimer;
        }
        
    }
}
