﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clientGameManager2 : MonoBehaviour {

    public GameObject mainSelectionPanel;
    public GameObject gateSelectionPanel;
    public GameObject laneSelectionPanel;

    public GameObject switchButtonL;
    public GameObject switchButtonR;
    public GameObject abilityButton;
    public GameObject powerupButton;
    public GameObject trapButton;
    public GameObject jumpButton;

    public int switchCooldown1st = 7;
    public int switchCooldown2nd = 5;
    public int switchCooldown3rd = 4;
    public int switchCooldown4th = 3;

    public int jumpCooldown1st = 7;
    public int jumpCooldown2nd = 5;
    public int jumpCooldown3rd = 4;
    public int jumpCooldown4th = 3;

    public int abilityCooldown = 4;
    public int trapCooldown = 10;
    public int powerupCooldown = 6;

    public GameObject gateButton1;
    public GameObject gateButton2;
    public GameObject gateButton3;
    public GameObject gateButton4;

    public GameObject laneButton1;
    public GameObject laneButton2;
    public GameObject laneButton3;
    public GameObject laneButton4;

    public Text gateText;
    public int nextGate;


    int laneChoice = 0;
    int amountOfPlayers = 0;
    int playerID = 0;

    int position = 1;
    int points = 0;
    string name;
    private bool isAbility = false;
    private bool onSwitchCooldown = false;
    private bool onJumpCooldown = false;
    private bool onAbilityCooldown = false;
    private bool onTrapCooldown = false;
    private bool onPowerupCooldown = false;



    private int abiltyType = 1;
    int powerUPchoice = 0; // will store the selected powerup ID; 1 for void, 2 for immunity
    int[] powerUPcost = new int[2]; // stores the cost for each powerup
    int trapChoice = 0; // will store the selected trap; 1 for freeze, 2 for barrier
    int[] trapCost = new int[2]; // stores the cost for each trap
    int vehicleAbility = 0;// will store the selected vehicle ability ID; 1 for nessieBubble
    int[] abilityCost = new int[1]; // stores the cost for each vehicle (will be increased in size when more abilitys have been implemented)
    // Use this for initialization

    void Start () {
        amountOfPlayers = networkClientUIbuttons.networkClient.getAmountOfPlayers();
        playerID = networkClientUIbuttons.networkClient.getPlayerID();

        powerUPchoice = networkClientUIbuttons.networkClient.getPowerUp();
        trapChoice = networkClientUIbuttons.networkClient.getTrap();


        powerUPcost[0] = 200;
        powerUPcost[1] = 200;
        trapCost[0] = 200;
        trapCost[1] = 200;

        //For Testing first vehicle ability will be set when vehicle is chosen client side
        vehicleAbility = networkClientUIbuttons.networkClient.getVehicleChoice();
        abilityCost[0] = 200;

        //init panels
        mainSelectionPanel.SetActive(true);
        laneSelectionPanel.SetActive(false);
        gateSelectionPanel.SetActive(false);

        setUpGateButtons();
    }
	
	// Update is called once per frame
	void Update () {
        points = networkClientUIbuttons.networkClient.getPoints();
        nextGate = networkClientUIbuttons.networkClient.getNextGate();
        position = networkClientUIbuttons.networkClient.getPosition();
       // networkClientUIbuttons.networkClient.sendName();
        setUpGateButtons();


        if (Input.acceleration.x > 0.5)
        {
            switchRight();
        }
        else if (Input.acceleration.x < -0.5)
        {
            switchLeft();
        }








    }

    void setUpGateButtons()
    {
        gateButton1.GetComponentInChildren<Text>().text = "Gate " + networkClientUIbuttons.networkClient.getNextGate().ToString();
        gateButton2.GetComponentInChildren<Text>().text = "Gate " + (networkClientUIbuttons.networkClient.getNextGate() + 1).ToString();
        gateButton3.GetComponentInChildren<Text>().text = "Gate " + (networkClientUIbuttons.networkClient.getNextGate() + 2).ToString();
        gateButton4.GetComponentInChildren<Text>().text = "Gate " + (networkClientUIbuttons.networkClient.getNextGate() + 3).ToString();
    }

    public void moveToGateSelection()
    {
        laneSelectionPanel.SetActive(false);

        gateSelectionPanel.SetActive(true);
    }

    public void gateButton1Pressed()
    {
        string[] buttonData = gateButton1.GetComponentInChildren<Text>().text.Split(' ');

        if (abiltyType == 2)
        {

            networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), laneChoice, networkClientUIbuttons.networkClient.vehicleChoice);
            StartCoroutine(StartAbilityCooldown());

            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);
        }
        else if (abiltyType == 1)
        {

            networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), laneChoice, networkClientUIbuttons.networkClient.vehicleChoice);
            StartCoroutine(StartAbilityCooldown());

            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);

        }
        else if (abiltyType == 5)
        {
            finishActivation(System.Convert.ToInt16(buttonData[1]));
        }
        abiltyType = 0;
    }
    public void gateButton2Pressed()
    {
        string[] buttonData = gateButton2.GetComponentInChildren<Text>().text.Split(' ');

        if (abiltyType == 2)
        {

            networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), laneChoice, networkClientUIbuttons.networkClient.vehicleChoice);
            StartCoroutine(StartAbilityCooldown());

            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);
        }
        else if (abiltyType == 1)
        {

            networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), laneChoice, networkClientUIbuttons.networkClient.vehicleChoice);
            StartCoroutine(StartAbilityCooldown());

            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);

        }
        else if (abiltyType == 5)
        {
            finishActivation(System.Convert.ToInt16(buttonData[1]));
        }
        abiltyType = 0;
    }
    public void gateButton3Pressed()
    {
        string[] buttonData = gateButton3.GetComponentInChildren<Text>().text.Split(' ');

        if (abiltyType == 2)
        {

            networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), laneChoice, networkClientUIbuttons.networkClient.vehicleChoice);
            StartCoroutine(StartAbilityCooldown());

            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);
        }
        else if (abiltyType == 1)
        {

            networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), laneChoice, networkClientUIbuttons.networkClient.vehicleChoice);
            StartCoroutine(StartAbilityCooldown());

            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);

        }
        else if (abiltyType == 5)
        {
            finishActivation(System.Convert.ToInt16(buttonData[1]));
        }
        abiltyType = 0;
    }
    public void gateButton4Pressed()
    {
        string[] buttonData = gateButton4.GetComponentInChildren<Text>().text.Split(' ');

        if (abiltyType == 2)
        {

            networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), laneChoice, networkClientUIbuttons.networkClient.vehicleChoice);
            StartCoroutine(StartAbilityCooldown());

            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);
        }
        else if (abiltyType == 1)
        {

            networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), laneChoice, networkClientUIbuttons.networkClient.vehicleChoice);
            StartCoroutine(StartAbilityCooldown());

            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);

        }
        else if (abiltyType == 5)
        {
            finishActivation(System.Convert.ToInt16(buttonData[1]));
        }
        abiltyType = 0;
    }

    public void laneButton1Pressed()
    {
        string[] buttonData = laneButton1.GetComponentInChildren<Text>().text.Split(' ');
        laneChoice = System.Convert.ToInt16(buttonData[1]);
        moveToGateSelection();
    }
    public void laneButton2Pressed()
    {
        string[] buttonData = laneButton2.GetComponentInChildren<Text>().text.Split(' ');
        laneChoice = System.Convert.ToInt16(buttonData[1]);
        moveToGateSelection();
    }
    public void laneButton3Pressed()
    {
        string[] buttonData = laneButton3.GetComponentInChildren<Text>().text.Split(' ');
        laneChoice = System.Convert.ToInt16(buttonData[1]);
        moveToGateSelection();
    }
    public void laneButton4Pressed()
    {
        string[] buttonData = laneButton4.GetComponentInChildren<Text>().text.Split(' ');
        laneChoice = System.Convert.ToInt16(buttonData[1]);
        moveToGateSelection();
    }
   

    public void activateJump()
    {
        if (!onJumpCooldown)
        {
            networkClientUIbuttons.networkClient.sendJump();
            StartCoroutine(StartJumpCooldown());
            StartCoroutine(StartJumpSwitchCooldown());
        }
    }

    public void switchLeft()
    {
        // send switch lane to network if there is no cooldown
        if (!onSwitchCooldown && networkClientUIbuttons.networkClient.getSwitchStateL() == 1)
        {
            networkClientUIbuttons.networkClient.sendLaneSwitch("Left");
            StartCoroutine(StartSwitchCooldown());
        }
    }

    public void switchRight()
    {
        // send switch lane to network if there is no cooldown
        if (!onSwitchCooldown && networkClientUIbuttons.networkClient.getSwitchStateR() == 1)
        {
            networkClientUIbuttons.networkClient.sendLaneSwitch("Right");
            StartCoroutine(StartSwitchCooldown());
        }
    }

    public IEnumerator StartAbilityCooldown()
    {
        onAbilityCooldown = true;

        // loops through for each second in the cooldown
        for (int x = 0; x < abilityCooldown; x++)
        {
            int text = abilityCooldown - x;
            // sets the UI text to show cooldown time
            abilityButton.GetComponentInChildren<Text>().text = text.ToString();
            yield return new WaitForSeconds(1);
        }

        abilityButton.GetComponentInChildren<Text>().text = "";

        onAbilityCooldown = false;
    }

    IEnumerator StartPowerupCooldown()
    {
        onPowerupCooldown = true;

        // loops through for each second in the cooldown
        for (int x = 0; x < powerupCooldown; x++)
        {
            int text = powerupCooldown - x;
            // sets the UI text to show cooldown time
            powerupButton.GetComponentInChildren<Text>().text = text.ToString();
            yield return new WaitForSeconds(1);
        }

        powerupButton.GetComponentInChildren<Text>().text = "";

        onPowerupCooldown = false;
    }

    IEnumerator StartTrapCooldown()
    {
        onTrapCooldown = true;

        // loops through for each second in the cooldown
        for (int x = 0; x < trapCooldown; x++)
        {
            int text = trapCooldown - x;
            // sets the UI text to show cooldown time
            trapButton.GetComponentInChildren<Text>().text = text.ToString();
            yield return new WaitForSeconds(1);
        }

        trapButton.GetComponentInChildren<Text>().text = "";

        onTrapCooldown = false;
    }

    IEnumerator StartJumpSwitchCooldown()
    {
        networkClientUIbuttons.networkClient.setSwitchStateL(0);
        networkClientUIbuttons.networkClient.setSwitchStateR(0);

        yield return new WaitForSeconds(2);

        networkClientUIbuttons.networkClient.setSwitchStateL(1);
        networkClientUIbuttons.networkClient.setSwitchStateR(1);
    }

    IEnumerator StartJumpCooldown()
    {
        onJumpCooldown = true;

        // runs cooldown for first position
        if (position == 1)
        {
            // loops through for each second in the cooldown
            for (int x = 0; x < jumpCooldown1st; x++)
            {
                int text = jumpCooldown1st - x;
                // sets the UI text to show cooldown time
                jumpButton.GetComponentInChildren<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        // runs cooldown for second position
        else if (position == 2)
        {
            for (int x = 0; x < jumpCooldown2nd; x++)
            {
                int text = jumpCooldown2nd - x;
                jumpButton.GetComponentInChildren<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        // runs cooldown for third position
        else if (position == 3)
        {
            for (int x = 0; x < jumpCooldown3rd; x++)
            {
                int text = jumpCooldown3rd - x;
                jumpButton.GetComponentInChildren<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        // runs cooldown for fourth position
        else if (position == 4)
        {
            for (int x = 0; x < jumpCooldown4th; x++)
            {
                int text = jumpCooldown4th - x;
                jumpButton.GetComponentInChildren<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        jumpButton.GetComponentInChildren<Text>().text = "";

        onJumpCooldown = false;
    }

    IEnumerator StartSwitchCooldown()
    {
        onSwitchCooldown = true;

        // runs cooldown for first position
        if (position == 1)
        {
            // loops through for each second in the cooldown
            for (int x = 0; x < switchCooldown1st; x++)
            {
                int text = switchCooldown1st - x;
                // sets the UI text to show cooldown time
                switchButtonL.GetComponentInChildren<Text>().text = text.ToString();
                switchButtonR.GetComponentInChildren<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        // runs cooldown for second position
        else if (position == 2)
        {
            for (int x = 0; x < switchCooldown2nd; x++)
            {
                int text = switchCooldown2nd - x;
                switchButtonL.GetComponentInChildren<Text>().text = text.ToString();
                switchButtonR.GetComponentInChildren<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        // runs cooldown for third position
        else if (position == 3)
        {
            for (int x = 0; x < switchCooldown3rd; x++)
            {
                int text = switchCooldown3rd - x;
                switchButtonL.GetComponentInChildren<Text>().text = text.ToString();
                switchButtonR.GetComponentInChildren<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        // runs cooldown for fourth position
        else if (position == 4)
        {
            for (int x = 0; x < switchCooldown4th; x++)
            {
                int text = switchCooldown4th - x;
                switchButtonL.GetComponentInChildren<Text>().text = text.ToString();
                switchButtonR.GetComponentInChildren<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        // reset the cooldown bool and the UI text when cooldown is complete
        switchButtonL.GetComponentInChildren<Text>().text = "";
        switchButtonR.GetComponentInChildren<Text>().text = "";

        onSwitchCooldown = false;
    }

    public void activateTrap()
    {
        if (!onTrapCooldown)
        {
            isAbility = false;

            laneSelectionPanel.SetActive(true);
            mainSelectionPanel.SetActive(false);
            abiltyType = 5;
        }
    }

    public void activatePowerUp()
    {
        // if (points >= powerUPcost[powerUPchoice - 1])
        // {
        if (!onPowerupCooldown)
        {
            // activate power up code here
            networkClientUIbuttons.networkClient.sendActivatePowerUP(powerUPchoice, playerID, vehicleAbility);
            // remove points for trap
            //networkClientUIbuttons.networkClient.removePoints(powerUPcost[powerUPchoice - 1]);
            StartCoroutine(StartPowerupCooldown());
        }
        // }
    }
    public void activateVehicleAbility()
    {
        if (!onAbilityCooldown)
        {
            abiltyType = networkClientUIbuttons.networkClient.getVehicleChoice();
            isAbility = true;
            if (abiltyType == 2)
            {
                laneSelectionPanel.SetActive(true);
                mainSelectionPanel.SetActive(false);
            }
            else if (abiltyType == 1)
            {
                laneSelectionPanel.SetActive(true);
                mainSelectionPanel.SetActive(false);

            }
            else if (abiltyType == 3)
            {
                finishActivation(0);
            }
        }

    }


    public void finishActivation(int gateChoice)
    {
        
        if (isAbility == false)
        {
            networkClientUIbuttons.networkClient.sendActivateTrap(gateChoice, laneChoice, trapChoice);
            // remove points for trap
            networkClientUIbuttons.networkClient.removePoints(trapCost[trapChoice - 1]);
            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);
            StartCoroutine(StartTrapCooldown());
        }
        else
        {
            if (vehicleAbility == 3)
            {
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(0, playerID, vehicleAbility);
                isAbility = false;
                StartCoroutine(StartAbilityCooldown());
            }
            else
            {
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(gateChoice, laneChoice, vehicleAbility);
                isAbility = false;
                gateSelectionPanel.SetActive(false);
                mainSelectionPanel.SetActive(true);
                StartCoroutine(StartAbilityCooldown());
            }
        }
        laneChoice = 0;
    }
}
