using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class players4 : MonoBehaviour {

    public GameObject playerButton1;
    public GameObject playerButton2;
    public GameObject playerButton3;
    public GameObject playerButton4;

    


    int playerChoice = 0; //use this for storing the lane where the trap will be placed

    // Use this for initialization
    void Start () {
        
     
    }
	
    public void setUpButtons(int removePlayerIDchoice)
    {
        //switch (removePlayerIDchoice)
        //{
        //    case 1:
        //        playerButton1.GetComponent<Text>().text = "Player 2";
        //        playerButton2.GetComponent<Text>().text = "Player 3";
        //        playerButton3.GetComponent<Text>().text = "Player 4";
        //        break;
        //    case 2:
        //        playerButton1.GetComponent<Text>().text = "Player 1";
        //        playerButton2.GetComponent<Text>().text = "Player 3";
        //        playerButton3.GetComponent<Text>().text = "Player 4";
        //        break;
        //    case 3:
        //        playerButton1.GetComponent<Text>().text = "Player 1";
        //        playerButton2.GetComponent<Text>().text = "Player 2";
        //        playerButton3.GetComponent<Text>().text = "Player 4";

        //        break;
        //    case 4:
        //        playerButton1.GetComponent<Text>().text = "Player 1";
        //        playerButton2.GetComponent<Text>().text = "Player 2";
        //        playerButton3.GetComponent<Text>().text = "Player 3";
        //        break;
        //}
    }

    public void playerButton1Pressed()
    {
        string[] buttonData = playerButton1.GetComponent<Text>().text.Split(' ');
      
        playerChoice = System.Convert.ToInt16(buttonData[1]);
    }
    public void playerButton2Pressed()
    {
        string[] buttonData = playerButton2.GetComponent<Text>().text.Split(' ');
        playerChoice = System.Convert.ToInt16(buttonData[1]);
    }
    public void playerButton3Pressed()
    {
        string[] buttonData = playerButton3.GetComponent<Text>().text.Split(' ');
        playerChoice = System.Convert.ToInt16(buttonData[1]);
    }
    public void playerButton4Pressed()
    {
        string[] buttonData = playerButton4.GetComponent<Text>().text.Split(' ');
        playerChoice = System.Convert.ToInt16(buttonData[1]);
    }

    //public void gateButton1Pressed()
    //{
    //    string[] buttonData = gateButton1.GetComponent<Text>().text.Split(' ');
    //    GetComponent<clientGameManager>().finishActivation(playerChoice, System.Convert.ToInt16(buttonData[1]));

    //    playerChoice = 0;
    //}

    //public void gateButton2Pressed()
    //{
    //    string[] buttonData = gateButton2.GetComponent<Text>().text.Split(' ');
    //    GetComponent<clientGameManager>().finishActivation(playerChoice, System.Convert.ToInt16(buttonData[1]));

    //    playerChoice = 0;
    //}

    //public void gateButton3Pressed()
    //{
    //    string[] buttonData = gateButton3.GetComponent<Text>().text.Split(' ');
    //    GetComponent<clientGameManager>().finishActivation(playerChoice, System.Convert.ToInt16(buttonData[1]));

    //    playerChoice = 0;
    //}

    //public void gateButton4Pressed()
    //{
    //    string[] buttonData = gateButton4.GetComponent<Text>().text.Split(' ');
    //    GetComponent<clientGameManager>().finishActivation(playerChoice, System.Convert.ToInt16(buttonData[1]));

    //    playerChoice = 0;
    //}

    public int getPlayerChoice()
    {
        return playerChoice;
    }


    // Update is called once per frame
    void Update () {

        //if (removePlayerIDchoice == 0) //if player doensnt have id, keep checking until they do
        //{
        //    removePlayerIDchoice = networkClientUIbuttons.networkClient.getPlayerID();
        //}
    }
}
