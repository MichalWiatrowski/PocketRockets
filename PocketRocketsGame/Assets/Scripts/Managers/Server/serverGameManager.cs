using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serverGameManager : MonoBehaviour {

    private int amountOfPlayers = 0;
    public GameObject mainCamera;

    // Use this for initialization
    void Start()
    {
        amountOfPlayers = networkServerUIbuttons.networkServer.getPlayerAmount();
        for (int i = 4; i > amountOfPlayers; i--)
        {
            GameObject.Find("Player " + i).SetActive(false);
        }
        mainCamera.GetComponent<Follow>().cars.RemoveRange(amountOfPlayers, mainCamera.GetComponent<Follow>().cars.Count - amountOfPlayers);

    }

    
    // Update is called once per frame
    void Update () {
    }
}
