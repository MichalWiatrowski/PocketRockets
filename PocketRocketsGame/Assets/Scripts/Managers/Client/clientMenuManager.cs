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

    public Text text;


    private int powerUP = 0;
    private int trap = 0;

    private int vehicleNo = 1;
    private bool playerReady = false;
    // Use this for initialization
    void Start () {
        joinGamePanel.SetActive(true);
        powerUpSelectionPanel.SetActive(false);
        vehicleSelectionPanel.SetActive(false);
        trapSelectionPanel.SetActive(false);
        text.text = "Cupcake";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void next()
    {
        //next vehicle code
        if (playerReady != true)
        {
            vehicleNo++;
            if (vehicleNo > 3)
                vehicleNo = 1;
            switch (vehicleNo)
            {
                case 1:
                    text.text = "Cupcake";
                    break;
                case 2:
                    text.text = "Nessie";
                    break;
                case 3:
                    text.text = "Bathtub";
                    break;
                case 4:
                    text.text = "Crown";
                    break;
            }
        } 
    }
    public void previous()
    { 
        //previous vehicle code
        if (playerReady != true)
        {
            vehicleNo--;
            if (vehicleNo < 1)
                vehicleNo = 3;
            switch (vehicleNo)
            {
                case 1:
                    text.text = "Cupcake";
                    break;
                case 2:
                    text.text = "Nessie";
                    break;
                case 3:
                    text.text = "Bathtub";
                    break;
                case 4:
                    text.text = "Crown";
                    break;
            }
        }  
    }
    public void ready()
    {
        //set client ready code
        playerReady = !playerReady;
        //networkClientUIbuttons.networkClient.toggleReady();
        networkClientUIbuttons.networkClient.vehicleChoice = vehicleNo;
        networkClientUIbuttons.networkClient.sendReadyUp(System.Convert.ToInt16(playerReady));

       if(playerReady)
            readyButton.GetComponent<Image>().color = Color.green;
        else
            readyButton.GetComponent<Image>().color = Color.red;

    }

    public void join()
    {
        networkClientUIbuttons.networkClient.setName();
        networkClientUIbuttons.networkClient.joinGame();
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
