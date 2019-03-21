using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class serverGameManager : MonoBehaviour {

    private int amountOfPlayers = 0;
    public GameObject mainCamera;
    public AudioClip raceStartClip;
    public List<GameObject> player1Vehicles = new List<GameObject>();
    public List<GameObject> player2Vehicles = new List<GameObject>();
    public List<GameObject> player3Vehicles = new List<GameObject>();
    public List<GameObject> player4Vehicles = new List<GameObject>();


    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private AudioSource gameSource;

    void Awake() {
    gameSource= GetComponent<AudioSource>();

    }
    // Use this for initialization
    void Start()
    {
        gameSource.PlayOneShot(raceStartClip, 0.1f);
        amountOfPlayers = networkServerUIbuttons.networkServer.getPlayerAmount();
        

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


    }

    
    // Update is called once per frame
    void Update () {

     
    }
}
