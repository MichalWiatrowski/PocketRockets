using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class clientMenuManager : MonoBehaviour {

    public AudioClip buttonClip;
    public AudioClip ready1;
    public AudioClip ready2;

    public GameObject joinGamePanel;
    public GameObject vehicleSelectionPanel;
    public GameObject powerUpSelectionPanel;
    public GameObject trapSelectionPanel;
    public GameObject readyButton;

    public Text text;

    public AudioSource buttonSource;
    private int powerUP = 0;
    private int trap = 0;

    private int vehicleNo = 1;
    private bool playerReady = false;

    private void Awake()
    {

        buttonSource = GetComponent<AudioSource>();

    }
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

    public void Next()
    {
        buttonSource.PlayOneShot(buttonClip);
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
    public void Previous()
    {
        buttonSource.PlayOneShot(buttonClip);
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
    public void Ready()
    {
        buttonSource.PlayOneShot(buttonClip);
        int readyNum;
        readyNum = Random.Range(1, 11);

        //set client ready code
        playerReady = !playerReady;

        if (playerReady) {

            if (readyNum < 6)
            {
                buttonSource.PlayOneShot(ready1);
            }
            else
            {
                buttonSource.PlayOneShot(ready2);
            }
        }
        //networkClientUIbuttons.networkClient.toggleReady();
        networkClientUIbuttons.networkClient.vehicleChoice = vehicleNo;
        networkClientUIbuttons.networkClient.SendReadyUp(System.Convert.ToInt16(playerReady));

       if(playerReady)
            readyButton.GetComponent<Image>().color = Color.green;
        else
            readyButton.GetComponent<Image>().color = Color.red;

    }

    public void Join()
    {
        buttonSource.PlayOneShot(buttonClip);
        networkClientUIbuttons.networkClient.SetName();
        networkClientUIbuttons.networkClient.JoinGame();
    }

    public void VoidPower()
    {
        buttonSource.PlayOneShot(buttonClip);
        powerUP = 1;
        powerUpSelectionPanel.SetActive(false);
        trapSelectionPanel.SetActive(true);
    }
    
    public void ImmunityPower()
    {
        buttonSource.PlayOneShot(buttonClip);
        powerUP = 2;
        powerUpSelectionPanel.SetActive(false);
        trapSelectionPanel.SetActive(true);
    }

    public void FreezeTrap()
    {
        buttonSource.PlayOneShot(buttonClip);
        trap = 1;
        networkClientUIbuttons.networkClient.SetPowers(powerUP, trap);
        trapSelectionPanel.SetActive(false);
        vehicleSelectionPanel.SetActive(true);
    }

    public void WallTrap()
    {
        buttonSource.PlayOneShot(buttonClip);
        trap = 2;
        networkClientUIbuttons.networkClient.SetPowers(powerUP, trap);
        trapSelectionPanel.SetActive(false);
        vehicleSelectionPanel.SetActive(true);
    }




}
