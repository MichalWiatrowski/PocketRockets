using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class players3 : MonoBehaviour {

    public GameObject playerButton1Text;
    public GameObject playerButton2Text;

    //public GameObject gateButton1;
    //public GameObject gateButton2;
    //public GameObject gateButton3;
    //public GameObject gateButton4;


    int playerChoice = 0; //use this for storing the lane where the trap will be placed

    // Use this for initialization
    void Start () {
		
	}

    public void setUpButtons(int removePlayerIDchoice)
    {
        switch (removePlayerIDchoice)
        {
            case 1:
                playerButton1Text.GetComponent<Text>().text = "Player 2";
                playerButton2Text.GetComponent<Text>().text = "Player 3";              
                break;
            case 2:
                playerButton1Text.GetComponent<Text>().text = "Player 1";
                playerButton2Text.GetComponent<Text>().text = "Player 3";
                break;
            case 3:
                playerButton1Text.GetComponent<Text>().text = "Player 1";
                playerButton2Text.GetComponent<Text>().text = "Player 2";
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void playerButton1Pressed()
    {
        string[] buttonData = playerButton1Text.GetComponent<Text>().text.Split(' ');

        playerChoice = System.Convert.ToInt16(buttonData[1]);
    }
    public void playerButton2Pressed()
    {
        string[] buttonData = playerButton2Text.GetComponent<Text>().text.Split(' ');
        playerChoice = System.Convert.ToInt16(buttonData[1]);
    }

    public int getPlayerChoice()
    {
        return playerChoice;
    }

}
