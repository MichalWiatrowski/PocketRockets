using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class clientGameManager : MonoBehaviour {
    public GameObject mainSelectionPanel;
    public GameObject playerSelectionPanel4;
    public GameObject playerSelectionPanel3;
    public GameObject gateSelectionPanel;


    public GameObject gateButton1;
    public GameObject gateButton2;
    public GameObject gateButton3;
    public GameObject gateButton4;

    int amountOfPlayers = 0;
    int playerID = 0;
    int playerIDchoice = 0;
    // Use this for initialization
    void Start()
    {
        amountOfPlayers = networkClientUIbuttons.networkClient.getAmountOfPlayers();

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

    }
    
    //code for 2 players, no need to create another script

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
    // gate buttons, so far fixed support for 4 gates
    //TODO add dynamic buttons e.g. will only display gate numbers in front not behind
    void setUpGateButtons()
    {
        gateButton1.GetComponent<Text>().text = "Gate 1";
        gateButton2.GetComponent<Text>().text = "Gate 2";
        gateButton3.GetComponent<Text>().text = "Gate 3";
        gateButton4.GetComponent<Text>().text = "Gate 4";
    }
 
    public void gateButton1Pressed()
    {
        string[] buttonData = gateButton1.GetComponent<Text>().text.Split(' ');

        if (amountOfPlayers == 2)
        {
            finishActivation(playerIDchoice, System.Convert.ToInt16(buttonData[1]));
        }
        else if (amountOfPlayers == 3)
            finishActivation(GetComponent<players3>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
        else if (amountOfPlayers == 4)
            finishActivation(GetComponent<players4>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
    }

    public void gateButton2Pressed()
    {
        string[] buttonData = gateButton2.GetComponent<Text>().text.Split(' ');

        if (amountOfPlayers == 2)
        {
            finishActivation(playerIDchoice, System.Convert.ToInt16(buttonData[1]));  
        }     
        else if (amountOfPlayers == 3)
            finishActivation(GetComponent<players3>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
        else if (amountOfPlayers == 4)
            finishActivation(GetComponent<players4>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
    }

    public void gateButton3Pressed()
    {
        string[] buttonData = gateButton3.GetComponent<Text>().text.Split(' ');

        if (amountOfPlayers == 2)
        {
            finishActivation(playerIDchoice, System.Convert.ToInt16(buttonData[1]));
        }
        else if (amountOfPlayers == 3)
            finishActivation(GetComponent<players3>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
        else if (amountOfPlayers == 4)
            finishActivation(GetComponent<players4>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
    }

    public void gateButton4Pressed()
    {
        string[] buttonData = gateButton4.GetComponent<Text>().text.Split(' ');

        if (amountOfPlayers == 2)
        {
            finishActivation(playerIDchoice, System.Convert.ToInt16(buttonData[1]));
        }
        else if (amountOfPlayers == 3)
            finishActivation(GetComponent<players3>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
        else if (amountOfPlayers == 4)
            finishActivation(GetComponent<players4>().getPlayerChoice(), System.Convert.ToInt16(buttonData[1]));
    }
/// <summary>
/// /////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>

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

    public void activateTrap()
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
    public void activatePowerUp()
    {
        // activate power up code here
    }
    public void activateVehicleAbility()
    {
        // activate ability code here
    }
    public void moveToGateSelection()
    {
        playerSelectionPanel3.SetActive(false);
        playerSelectionPanel4.SetActive(false);
        gateSelectionPanel.SetActive(true);
    }

    public void finishActivation(int playerIDchoice, int gateChoice)
    {
        networkClientUIbuttons.networkClient.sendActivateTrap(gateChoice, playerIDchoice);
        gateSelectionPanel.SetActive(false);
        mainSelectionPanel.SetActive(true);
    }

    
}
