using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class clientGameManager : MonoBehaviour {
    public GameObject mainSelectionPanel;
    public GameObject playerSelectionPanel4;
    public GameObject playerSelectionPanel3;
    public GameObject gateSelectionPanel;

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

    public Text gateText;
    public int nextGate;
    int amountOfPlayers = 0;
    int playerID = 0;
    int playerPos = 1;
    int playerIDchoice = 0;
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
    void Start()
    {
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
        playerSelectionPanel4.SetActive(false);
        playerSelectionPanel3.SetActive(false);
        gateSelectionPanel.SetActive(false);

        setUpGateButtons();
      
        numberOfPlayersSetUp();   
    }

    // Update is called once per frame
    void Update()
    {
        points = networkClientUIbuttons.networkClient.getPoints();
        nextGate = networkClientUIbuttons.networkClient.getNextGate();
        networkClientUIbuttons.networkClient.sendName();
        setUpGateButtons();
    }
    
    void playerChoice()
    {
        if (playerID == 1)
        {
            playerIDchoice = 2;
        }
        else if (playerID == 2)
        {
            playerIDchoice = 1;
        }
    }
    void numberOfPlayersSetUp()
    {
        //set up buttons for the screen
        if (amountOfPlayers == 2)
        {
            playerID = networkClientUIbuttons.networkClient.getPlayerID();
            //call function for 2 players
            playerChoice();
        }
        else if (amountOfPlayers == 3)
        {
            //call function for 3 players
            GetComponent<players3>().setUpButtons(networkClientUIbuttons.networkClient.getPlayerID());
        }
        else if (amountOfPlayers == 4)
        {
            //call function for 4 players
            GetComponent<players4>().setUpButtons(networkClientUIbuttons.networkClient.getPlayerID());

        }
    }
    // gate buttons, so far fixed support for 4 gates
    //TODO add dynamic buttons e.g. will only display gate numbers in front not behind
    void setUpGateButtons()
    {
        gateButton1.GetComponent<Text>().text = "Gate " + networkClientUIbuttons.networkClient.getNextGate().ToString();
        gateButton2.GetComponent<Text>().text = "Gate " + (networkClientUIbuttons.networkClient.getNextGate() + 1).ToString();
        gateButton3.GetComponent<Text>().text = "Gate " + (networkClientUIbuttons.networkClient.getNextGate() + 2).ToString();
        gateButton4.GetComponent<Text>().text = "Gate " + (networkClientUIbuttons.networkClient.getNextGate() + 3).ToString();
    }
    public void moveToGateSelection()
    {
        playerSelectionPanel3.SetActive(false);
        playerSelectionPanel4.SetActive(false);
        //for (int x = 1; x < 11; x++)
        //{
        //    if (x >= networkClientUIbuttons.networkClient.getNextGate())
        //    {
        //        GameObject.Find("gatePick" + x).SetActive(true);
        //    }
        //}
        gateSelectionPanel.SetActive(true);
    }

    public void gateButton1Pressed()
    {
        string[] buttonData = gateButton1.GetComponent<Text>().text.Split(' ');

        if (abiltyType == 2) 
        {
            // activate nessie ability code here
            if (amountOfPlayers == 2)
            {
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), playerIDchoice, networkClientUIbuttons.networkClient.vehicleChoice);
            }
            else if (amountOfPlayers == 3)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players3>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            else if (amountOfPlayers == 4)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players4>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);
        }
        else if (abiltyType == 1)
        {
            // activate nessie ability code here
            if (amountOfPlayers == 2)
            {
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), playerIDchoice, networkClientUIbuttons.networkClient.vehicleChoice);
            }
            else if (amountOfPlayers == 3)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players3>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            else if (amountOfPlayers == 4)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players4>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);

        }
        else if (abiltyType == 5)
        {
            if (amountOfPlayers == 2)
            {
                finishActivation(playerIDchoice, System.Convert.ToInt16(buttonData[1]));
            }
            else if (amountOfPlayers == 3)
                finishActivation(GetComponent<players3>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
            else if (amountOfPlayers == 4)
                finishActivation(GetComponent<players4>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
        }
        abiltyType = 0;
    }

    public void gateButton2Pressed()
    {
        string[] buttonData = gateButton2.GetComponent<Text>().text.Split(' ');

        if (abiltyType == 2) 
        {
            // activate nessie ability code here
            if (amountOfPlayers == 2)
            {
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), playerIDchoice, networkClientUIbuttons.networkClient.vehicleChoice);
            }
            else if (amountOfPlayers == 3)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players3>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            else if (amountOfPlayers == 4)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players4>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);
        }
        else if (abiltyType == 1)
        {
            // activate nessie ability code here
            if (amountOfPlayers == 2)
            {
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), playerIDchoice, networkClientUIbuttons.networkClient.vehicleChoice);
            }
            else if (amountOfPlayers == 3)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players3>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            else if (amountOfPlayers == 4)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players4>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);

        }
        else if (abiltyType == 5)
        {
            if (amountOfPlayers == 2)
            {
                finishActivation(playerIDchoice, System.Convert.ToInt16(buttonData[1]));
            }
            else if (amountOfPlayers == 3)
                finishActivation(GetComponent<players3>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
            else if (amountOfPlayers == 4)
                finishActivation(GetComponent<players4>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
        }
        abiltyType = 0;
    }

    public void gateButton3Pressed()
    {
        string[] buttonData = gateButton3.GetComponent<Text>().text.Split(' ');


        if (abiltyType == 2)
        {
            // activate nessie ability code here
            if (amountOfPlayers == 2)
            {
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), playerIDchoice, networkClientUIbuttons.networkClient.vehicleChoice);
            }
            else if (amountOfPlayers == 3)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players3>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            else if (amountOfPlayers == 4)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players4>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);
        }
        else if (abiltyType == 1)
        {
            // activate cupCake ability code here
            if (amountOfPlayers == 2)
            {
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), playerIDchoice, networkClientUIbuttons.networkClient.vehicleChoice);
            }
            else if (amountOfPlayers == 3)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players3>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            else if (amountOfPlayers == 4)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players4>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);

        }
        else if (abiltyType == 5)
        {
            if (amountOfPlayers == 2)
            {
                finishActivation(playerIDchoice, System.Convert.ToInt16(buttonData[1]));
            }
            else if (amountOfPlayers == 3)
                finishActivation(GetComponent<players3>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
            else if (amountOfPlayers == 4)
                finishActivation(GetComponent<players4>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
        }
        abiltyType = 0;
    }

    public void gateButton4Pressed()
    {
        string[] buttonData = gateButton4.GetComponent<Text>().text.Split(' ');

        if (abiltyType == 2)
        {
            // activate nessie ability code here
            if (amountOfPlayers == 2)
            {
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), playerIDchoice, networkClientUIbuttons.networkClient.vehicleChoice);
            }
            else if (amountOfPlayers == 3)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players3>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            else if (amountOfPlayers == 4)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players4>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);
        }
        else if (abiltyType == 1)
        {
            // activate nessie ability code here
            if (amountOfPlayers == 2)
            {
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), playerIDchoice, networkClientUIbuttons.networkClient.vehicleChoice);
            }
            else if (amountOfPlayers == 3)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players3>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            else if (amountOfPlayers == 4)
                networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(System.Convert.ToInt16(buttonData[1]), GetComponent<players4>().getPlayerChoice(), networkClientUIbuttons.networkClient.vehicleChoice);
            gateSelectionPanel.SetActive(false);
            mainSelectionPanel.SetActive(true);

        }
        else if (abiltyType == 5)
        {
            if (amountOfPlayers == 2)
            {
                finishActivation(playerIDchoice, System.Convert.ToInt16(buttonData[1]));
            }
            else if (amountOfPlayers == 3)
                finishActivation(GetComponent<players3>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
            else if (amountOfPlayers == 4)
                finishActivation(GetComponent<players4>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
        }
        abiltyType = 0;
    }


    

    public void activateTrap()
    {
       // if (points >= trapCost[trapChoice - 1])
       // {
                switch (amountOfPlayers)
                {
                    case 2:
                        moveToGateSelection();
                        break;
                    case 3:
                        playerSelectionPanel3.SetActive(true);
                        break;
                    case 4:
                        playerSelectionPanel4.SetActive(true);
                        break;
                }
                mainSelectionPanel.SetActive(false);
                abiltyType = 5;
       // }
    }

    public void activateJump()
    {
        if (!onJumpCooldown)
        {
            networkClientUIbuttons.networkClient.sendJump();
            StartCoroutine(StartJumpCooldown());
        }
    }

    public void switchLeft()
    {
        // send switch lane to network if there is no cooldown
        if (!onSwitchCooldown)
        {
            networkClientUIbuttons.networkClient.sendLaneSwitch("Left");
            StartCoroutine(StartSwitchCooldown());
        }
    }

    public void switchRight()
    {
        // send switch lane to network if there is no cooldown
        if (!onSwitchCooldown)
        {
            networkClientUIbuttons.networkClient.sendLaneSwitch("Right");
            StartCoroutine(StartSwitchCooldown());
        }
    }

    IEnumerator StartAbilityCooldown()
    {
        onAbilityCooldown = true;

        // loops through for each second in the cooldown
        for (int x = 0; x < abilityCooldown; x++)
        {
            int text = abilityCooldown - x;
            // sets the UI text to show cooldown time
            abilityButton.GetComponent<Text>().text = text.ToString();
            yield return new WaitForSeconds(1);
        }

        abilityButton.GetComponent<Text>().text = "Vehicle Ability!";

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
            powerupButton.GetComponent<Text>().text = text.ToString();
            yield return new WaitForSeconds(1);
        }

        powerupButton.GetComponent<Text>().text = "Power-Up!";

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
            trapButton.GetComponent<Text>().text = text.ToString();
            yield return new WaitForSeconds(1);
        }

        trapButton.GetComponent<Text>().text = "Activate Trap!";

        onTrapCooldown = false;
    }

    IEnumerator StartJumpCooldown()
    {
        onJumpCooldown = true;

        // runs cooldown for first position
        if (playerPos == 1)
        {
            // loops through for each second in the cooldown
            for (int x = 0; x < jumpCooldown1st; x++)
            {
                int text = jumpCooldown1st - x;
                // sets the UI text to show cooldown time
                jumpButton.GetComponent<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        // runs cooldown for second position
        if (playerPos == 2)
        {
            for (int x = 0; x < jumpCooldown2nd; x++)
            {
                int text = jumpCooldown2nd - x;
                jumpButton.GetComponent<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        // runs cooldown for third position
        if (playerPos == 3)
        {
            for (int x = 0; x < jumpCooldown3rd; x++)
            {
                int text = jumpCooldown3rd - x;
                jumpButton.GetComponent<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        // runs cooldown for fourth position
        if (playerPos == 4)
        {
            for (int x = 0; x < jumpCooldown4th; x++)
            {
                int text = jumpCooldown4th - x;
                jumpButton.GetComponent<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        jumpButton.GetComponent<Text>().text = "Jump!";

        onJumpCooldown = false;
    }

    IEnumerator StartSwitchCooldown()
    {
        onSwitchCooldown = true;

        // runs cooldown for first position
        if (playerPos == 1)
        {
            // loops through for each second in the cooldown
            for (int x = 0; x < switchCooldown1st; x++)
            {
                int text = switchCooldown1st - x;
                // sets the UI text to show cooldown time
                switchButtonL.GetComponent<Text>().text = text.ToString();
                switchButtonR.GetComponent<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        // runs cooldown for second position
        if (playerPos == 2)
        {
            for (int x = 0; x < switchCooldown2nd; x++)
            {
                int text = switchCooldown2nd - x;
                switchButtonL.GetComponent<Text>().text = text.ToString();
                switchButtonR.GetComponent<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        // runs cooldown for third position
        if (playerPos == 3)
        {
            for (int x = 0; x < switchCooldown3rd; x++)
            {
                int text = switchCooldown3rd - x;
                switchButtonL.GetComponent<Text>().text = text.ToString();
                switchButtonR.GetComponent<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        // runs cooldown for fourth position
        if (playerPos == 4)
        {
            for (int x = 0; x < switchCooldown4th; x++)
            {
                int text = switchCooldown4th - x;
                switchButtonL.GetComponent<Text>().text = text.ToString();
                switchButtonR.GetComponent<Text>().text = text.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        
        // reset the cooldown bool and the UI text when cooldown is complete
        switchButtonL.GetComponent<Text>().text = "Left";
        switchButtonR.GetComponent<Text>().text = "Right";

        onSwitchCooldown = false;
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
        abiltyType = networkClientUIbuttons.networkClient.getVehicleChoice();
        isAbility = true;

            if (abiltyType == 2)
            {
                switch (amountOfPlayers)
                {
                    case 2:
                        moveToGateSelection();
                        break;
                    case 3:
                        playerSelectionPanel3.SetActive(true);
                        break;
                    case 4:
                        playerSelectionPanel4.SetActive(true);
                        break;
                }
                mainSelectionPanel.SetActive(false);
            }
            else if (abiltyType == 1)
            {
                switch (amountOfPlayers)
                {
                    case 2:
                        moveToGateSelection();
                        break;
                    case 3:
                        playerSelectionPanel3.SetActive(true);
                        break;
                    case 4:
                        playerSelectionPanel4.SetActive(true);
                        break;
                }
                mainSelectionPanel.SetActive(false);

            }
            else if (abiltyType == 3)
            {
                finishActivation(playerIDchoice, 0);
            }
      
       
    }
   

    public void finishActivation(int playerIDchoice, int gateChoice)
    {
        if (isAbility == false)
        {
            if (!onTrapCooldown)
            {
                networkClientUIbuttons.networkClient.sendActivateTrap(gateChoice, playerIDchoice, trapChoice);
                // remove points for trap
                networkClientUIbuttons.networkClient.removePoints(trapCost[trapChoice - 1]);
                gateSelectionPanel.SetActive(false);
                mainSelectionPanel.SetActive(true);
                StartCoroutine(StartTrapCooldown());
            }
        }
        else {
            if (!onAbilityCooldown)
            {
                if (vehicleAbility == 3)
                {
                    networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(0, playerID, vehicleAbility);
                    StartCoroutine(StartAbilityCooldown());
                }
                else
                {
                    networkClientUIbuttons.networkClient.sendActivateVehicleAbiltiy(gateChoice, playerIDchoice, vehicleAbility);
                    StartCoroutine(StartAbilityCooldown());
                    isAbility = false;
                    gateSelectionPanel.SetActive(false);
                    mainSelectionPanel.SetActive(true);
                }
            }
        }
    }

    
}
