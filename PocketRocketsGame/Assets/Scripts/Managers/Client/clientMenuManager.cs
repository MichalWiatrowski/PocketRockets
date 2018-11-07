using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clientMenuManager : MonoBehaviour {

    public GameObject joinGamePanel;
    public GameObject vehicleSelectionPanel;
    // Use this for initialization
    void Start () {
        joinGamePanel.SetActive(true);
        vehicleSelectionPanel.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void next()
    {
        //next vehicle code
    }
    public void previous()
    {
        //previous vehicle code
    }
    public void ready()
    {
        //set client ready code
    }
    public void join()
    {
        networkClientUIbuttons.networkClient.joinGame();
        joinGamePanel.SetActive(false);
        vehicleSelectionPanel.SetActive(true);   
    }
}
