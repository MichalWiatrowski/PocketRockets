using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clientGameScreen : MonoBehaviour {

    int menuIndex = 1;
    int choiceIndex = 1;
	// Use this for initialization
	void Start () {
		
	}
    void OnGUI()
    {
        if (menuIndex == 1)
        {
            if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 10, 200, 100), "Activate Power-Up"))
            {


            }
            if (GUI.Button(new Rect((Screen.width / 2) + 200, (Screen.height / 2) - 10, 200, 100), "Activate Trap"))
            {
                menuIndex = 2;
            }
        }
        else if (menuIndex == 2)
        {

            if (choiceIndex == 1)
            {
                if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 200, 200, 100), "Player 1"))
                {
                    networkClientUIbuttons.networkClient.sendPlayerID(1);
                    choiceIndex = 2;
                }

                if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 100, 200, 100), "Player 2"))
                {
                    networkClientUIbuttons.networkClient.sendPlayerID(2);
                    choiceIndex = 2;
                }

                if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2), 200, 100), "Player 3"))
                {
                    networkClientUIbuttons.networkClient.sendPlayerID(3);
                    choiceIndex = 2;
                }

                if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) + 100, 200, 100), "Player 4"))
                {
                    networkClientUIbuttons.networkClient.sendPlayerID(4);
                    choiceIndex = 2;
                }
            }
            else if (choiceIndex == 2)
            {
                if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 200, 200, 100), "Gate 1"))
                {
                    networkClientUIbuttons.networkClient.sendGateNumber(1);
                    choiceIndex = 1;
                    menuIndex = 1;
                }

                if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 100, 200, 100), "Gate 2"))
                {
                    networkClientUIbuttons.networkClient.sendGateNumber(2);
                    choiceIndex = 1;
                    menuIndex = 1;
                }

                if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2), 200, 100), "Gate 3"))
                {
                    networkClientUIbuttons.networkClient.sendGateNumber(3);
                    choiceIndex = 1;
                    menuIndex = 1;
                }

                if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) + 100, 200, 100), "Gate 4"))
                {
                    networkClientUIbuttons.networkClient.sendGateNumber(4);
                    choiceIndex = 1;
                    menuIndex = 1;
                }
            }
           
        }
        
    }

        // Update is called once per frame
    void Update () {
		
	}
}
