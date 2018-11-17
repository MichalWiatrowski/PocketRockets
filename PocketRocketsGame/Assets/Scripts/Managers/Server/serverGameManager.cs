﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class serverGameManager : MonoBehaviour {

    private int amountOfPlayers = 0;
    public GameObject mainCamera;

    public List<GameObject> player1Vehicles = new List<GameObject>();
    public List<GameObject> player2Vehicles = new List<GameObject>();
    public List<GameObject> player3Vehicles = new List<GameObject>();
    public List<GameObject> player4Vehicles = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        amountOfPlayers = networkServerUIbuttons.networkServer.getPlayerAmount();
        

        if (amountOfPlayers == 2)
        {
            player1Vehicles[networkServerUIbuttons.networkServer.playerVehicles[0] - 1].SetActive(true);
            player2Vehicles[networkServerUIbuttons.networkServer.playerVehicles[1] - 1].SetActive(true);
        }
        else if (amountOfPlayers == 3)
        {
            player1Vehicles[networkServerUIbuttons.networkServer.playerVehicles[0] - 1].SetActive(true);
            player2Vehicles[networkServerUIbuttons.networkServer.playerVehicles[1] - 1].SetActive(true);
            player3Vehicles[networkServerUIbuttons.networkServer.playerVehicles[2] - 1].SetActive(true);
        }
        else if (amountOfPlayers == 4)
        {
            player1Vehicles[networkServerUIbuttons.networkServer.playerVehicles[0] - 1].SetActive(true);
            player2Vehicles[networkServerUIbuttons.networkServer.playerVehicles[1] - 1].SetActive(true);
            player3Vehicles[networkServerUIbuttons.networkServer.playerVehicles[2] - 1].SetActive(true);
            player4Vehicles[networkServerUIbuttons.networkServer.playerVehicles[3] - 1].SetActive(true);
        }
        for (int i = 4; i > amountOfPlayers; i--)
        {
            //GameObject.Find("Player " + i).SetActive(false);
            GameObject.Find("Player_" + i).SetActive(false);
            GameObject.Find("Position_" + i).SetActive(false);
        }


        mainCamera.GetComponent<Follow>().cars.RemoveRange(amountOfPlayers, mainCamera.GetComponent<Follow>().cars.Count - amountOfPlayers);

    }

    
    // Update is called once per frame
    void Update () {
        
    }
}
