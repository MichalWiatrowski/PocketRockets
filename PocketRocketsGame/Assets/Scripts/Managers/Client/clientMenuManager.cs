using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class clientMenuManager : MonoBehaviour {

    public GameObject joinGamePanel;
    public GameObject vehicleSelectionPanel;
    public GameObject powerUpSelectionPanel;
    public GameObject trapSelectionPanel;
    public GameObject readyButton;

    private int powerUP = 0;
    private int trap = 0;



    // Use this for initialization
    void Start () {
        joinGamePanel.SetActive(true);
        powerUpSelectionPanel.SetActive(false);
        vehicleSelectionPanel.SetActive(false);
        trapSelectionPanel.SetActive(false);
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
        networkClientUIbuttons.networkClient.toggleReady();
        networkClientUIbuttons.networkClient.sendReadyUp();

        if (networkClientUIbuttons.networkClient.getReady())
            readyButton.GetComponent<Image>().color = Color.green;
        else
            readyButton.GetComponent<Image>().color = Color.red;

    }
    public void join()
    {
        networkClientUIbuttons.networkClient.joinGame();
        //joinGamePanel.SetActive(false);
        //vehicleSelectionPanel.SetActive(true);   
        //powerUpSelectionPanel.SetActive(true);
    }

    public void voidPower()
    {
        powerUP = 1;
        powerUpSelectionPanel.SetActive(false);
        trapSelectionPanel.SetActive(true);
    }
    
    public void immunityPower()
    {
        powerUP = 2;
        powerUpSelectionPanel.SetActive(false);
        trapSelectionPanel.SetActive(true);
    }

    public void freezeTrap()
    {
        trap = 1;
        networkClientUIbuttons.networkClient.setPowers(powerUP, trap);
        trapSelectionPanel.SetActive(false);
        vehicleSelectionPanel.SetActive(true);
    }

    public void wallTrap()
    {
        trap = 2;
        networkClientUIbuttons.networkClient.setPowers(powerUP, trap);
        trapSelectionPanel.SetActive(false);
        vehicleSelectionPanel.SetActive(true);
    }




}
