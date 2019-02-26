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


    public GameObject nessie;
    public GameObject bathtub;
    public GameObject tank;



    public Text text;

    public AudioSource buttonSource;
    private int powerUP = 0;
    private int trap = 0;

    private int vehicleNo = 1;
    private bool playerReady = false;

    private int menuIndex = 1;

    private void Awake()
    {

        buttonSource = GetComponent<AudioSource>();

    }
    // Use this for initialization
    void Start() {
        if (networkClientUIbuttons.networkClient.client.isConnected == true)
        {
            joinGamePanel.SetActive(false);
            vehicleSelectionPanel.SetActive(true);
        }
        else
        {
            joinGamePanel.SetActive(true);
            vehicleSelectionPanel.SetActive(false);
        }
        
        powerUpSelectionPanel.SetActive(false);
        //vehicleSelectionPanel.SetActive(false);
        trapSelectionPanel.SetActive(false);
        text.text = "Cupcake";



        //new model rotation
        if (vehicleNo == 1)
        {
            nessie.SetActive(false);
            bathtub.SetActive(false);
            tank.SetActive(true);
        }
        else if (vehicleNo == 2)
        {
            nessie.SetActive(true);
            bathtub.SetActive(false);
            tank.SetActive(false);
        }
        else if (vehicleNo == 3)
        {
            nessie.SetActive(false);
            bathtub.SetActive(true);
            tank.SetActive(false);
        }

        
       

    }

    // Update is called once per frame
    void Update() {

    }

    public void next()
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
                    nessie.SetActive(false);
                    bathtub.SetActive(false);
                    tank.SetActive(true);
                    break;
                case 2:
                    text.text = "Nessie";
                    nessie.SetActive(true);
                    bathtub.SetActive(false);
                    tank.SetActive(false);
                    break;
                case 3:
                    text.text = "Bathtub";
                    nessie.SetActive(false);
                    bathtub.SetActive(true);
                    tank.SetActive(false);
                    break;
                case 4:
                    text.text = "Crown";
                    break;
            }
        }
    }
    public void previous()
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
                    nessie.SetActive(false);
                    bathtub.SetActive(false);
                    tank.SetActive(true);
                    break;
                case 2:
                    text.text = "Nessie";
                    nessie.SetActive(true);
                    bathtub.SetActive(false);
                    tank.SetActive(false);
                    break;
                case 3:
                    text.text = "Bathtub";
                    nessie.SetActive(false);
                    bathtub.SetActive(true);
                    tank.SetActive(false);
                    break;
                case 4:
                    text.text = "Crown";
                    break;
            }
        }
    }
    public void ready()
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
        networkClientUIbuttons.networkClient.sendReadyUp(System.Convert.ToInt16(playerReady));
        networkClientUIbuttons.networkClient.sendName();

        if (playerReady)
            readyButton.GetComponent<Image>().color = Color.green;
        else
            readyButton.GetComponent<Image>().color = Color.red;

    }

    public void join()
    {
        buttonSource.PlayOneShot(buttonClip);
        menuIndex = 2;
        networkClientUIbuttons.networkClient.setName();
        networkClientUIbuttons.networkClient.joinGame();
    }

    public void voidPower()
    {
        buttonSource.PlayOneShot(buttonClip);
        powerUP = 1;
        menuIndex = 3;
        powerUpSelectionPanel.SetActive(false);
        trapSelectionPanel.SetActive(true);
    }

    public void immunityPower()
    {
        buttonSource.PlayOneShot(buttonClip);
        powerUP = 2;
        menuIndex = 3;
        powerUpSelectionPanel.SetActive(false);
        trapSelectionPanel.SetActive(true);
    }

    public void freezeTrap()
    {
        buttonSource.PlayOneShot(buttonClip);
        trap = 1;
        networkClientUIbuttons.networkClient.setPowers(powerUP, trap);
        menuIndex = 4;
        trapSelectionPanel.SetActive(false);
        vehicleSelectionPanel.SetActive(true);
    }

    public void wallTrap()
    {
        buttonSource.PlayOneShot(buttonClip);
        trap = 2;
        networkClientUIbuttons.networkClient.setPowers(powerUP, trap);
        menuIndex = 4;
        trapSelectionPanel.SetActive(false);
        vehicleSelectionPanel.SetActive(true);
    }

    public void back()
    {
        if (menuIndex == 2)
        {
            joinGamePanel.SetActive(true);
            powerUpSelectionPanel.SetActive(false);
            menuIndex = 1;
        }
        else if (menuIndex == 3)
        {
            trapSelectionPanel.SetActive(false);
            powerUpSelectionPanel.SetActive(true);
            menuIndex = 2;
        }
        else if (menuIndex == 4)
        {
            trapSelectionPanel.SetActive(true);
            vehicleSelectionPanel.SetActive(false);
            menuIndex = 3;
        }
    }


}
