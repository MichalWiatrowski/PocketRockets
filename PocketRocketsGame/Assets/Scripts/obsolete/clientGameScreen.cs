using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clientGameScreen : MonoBehaviour {

    int menuIndex = 1;
    int choiceIndex = 1;
    int removePlayerIDchoice = 0;

    int playerIDchoice = 0;
	// Use this for initialization
	void Start () {
        removePlayerIDchoice = networkClientUIbuttons.networkClient.getPlayerID();
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
            if (removePlayerIDchoice == 1)
            {
                if (choiceIndex == 1)
                {
                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 100, 200, 100), "Player 2"))
                    {

                        playerIDchoice = 2;
                        choiceIndex = 2;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2), 200, 100), "Player 3"))
                    {

                        playerIDchoice = 3;
                        choiceIndex = 2;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) + 100, 200, 100), "Player 4"))
                    {

                        playerIDchoice = 4;
                        choiceIndex = 2;
                    }
                }
                else if (choiceIndex == 2)
                {
                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 200, 200, 100), "Gate 1"))
                    {

                        networkClientUIbuttons.networkClient.sendActivateTrap(1, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 100, 200, 100), "Gate 2"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(2, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2), 200, 100), "Gate 3"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(3, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) + 100, 200, 100), "Gate 4"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(4, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }
                }
            }
            else if (removePlayerIDchoice == 2)
            {
                if (choiceIndex == 1)
                {
                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 100, 200, 100), "Player 1"))
                    {
                        playerIDchoice = 1;
                        choiceIndex = 2;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2), 200, 100), "Player 3"))
                    {

                        playerIDchoice = 3;
                        choiceIndex = 2;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) + 100, 200, 100), "Player 4"))
                    {

                        playerIDchoice = 4;
                        choiceIndex = 2;
                    }
                }
                else if (choiceIndex == 2)
                {
                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 200, 200, 100), "Gate 1"))
                    {

                        networkClientUIbuttons.networkClient.sendActivateTrap(1, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 100, 200, 100), "Gate 2"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(2, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2), 200, 100), "Gate 3"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(3, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) + 100, 200, 100), "Gate 4"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(4, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }
                }
            }
            else if (removePlayerIDchoice == 3)
            {
                if (choiceIndex == 1)
                {
                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 100, 200, 100), "Player 1"))
                    {

                        playerIDchoice = 1;
                        choiceIndex = 2;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2), 200, 100), "Player 2"))
                    {

                        playerIDchoice = 2;
                        choiceIndex = 2;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) + 100, 200, 100), "Player 4"))
                    {

                        playerIDchoice = 4;
                        choiceIndex = 2;
                    }
                }
                else if (choiceIndex == 2)
                {
                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 200, 200, 100), "Gate 1"))
                    {

                        networkClientUIbuttons.networkClient.sendActivateTrap(1, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 100, 200, 100), "Gate 2"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(2, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2), 200, 100), "Gate 3"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(3, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) + 100, 200, 100), "Gate 4"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(4, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }
                }
            }
            else if (removePlayerIDchoice == 4)
            {
                if (choiceIndex == 1)
                {
                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 100, 200, 100), "Player 1"))
                    {

                        playerIDchoice = 1;
                        choiceIndex = 2;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2), 200, 100), "Player 2"))
                    {

                        playerIDchoice = 2;
                        choiceIndex = 2;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) + 100, 200, 100), "Player 3"))
                    {

                        playerIDchoice = 3;
                        choiceIndex = 2;
                    }
                }
                else if (choiceIndex == 2)
                {
                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 200, 200, 100), "Gate 1"))
                    {

                        networkClientUIbuttons.networkClient.sendActivateTrap(1, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 100, 200, 100), "Gate 2"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(2, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2), 200, 100), "Gate 3"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(3, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }

                    if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) + 100, 200, 100), "Gate 4"))
                    {
                        networkClientUIbuttons.networkClient.sendActivateTrap(4, playerIDchoice);
                        choiceIndex = 1;
                        menuIndex = 1;
                        playerIDchoice = 0;
                    }
                }
            }
        }   
    }

        // Update is called once per frame
    void Update () {
		if (removePlayerIDchoice == 0)
        {
            removePlayerIDchoice = networkClientUIbuttons.networkClient.getPlayerID();
        }
	}
}
//switch (menuIndex)
//        {
//            case 1:
//                if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 10, 200, 100), "Activate Power-Up"))
//                {
//                }
//                if (GUI.Button(new Rect((Screen.width / 2) + 200, (Screen.height / 2) - 10, 200, 100), "Activate Trap"))
//                {
//                    menuIndex = 2;
//                }
//                break;
//            case 2:
//                switch (removeCurrentPlayerIDchoice)
//                {
//                    case 1:
//                        switch (choiceIndex)
//                        {
//                            case 1:
//                                if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 100, 200, 100), "Player 2"))
//                                {
//                                    playerIDchoice = 2;
//                                    choiceIndex = 2;
//                                }

//                                if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2), 200, 100), "Player 3"))
//                                {
//                                    playerIDchoice = 3;
//                                    choiceIndex = 2;
//                                }

//                                if (GUI.Button(new Rect((Screen.width / 2) - 400, (Screen.height / 2) + 100, 200, 100), "Player 4"))
//                                {
//                                    playerIDchoice = 4;
//                                    choiceIndex = 2;
//                                }
//                                break;
//                            case 2:
//                                if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 200, 200, 100), "Gate 1"))
//                                {
//                                    networkClientUIbuttons.networkClient.sendActivateTrap(1, playerIDchoice);
//                                    choiceIndex = 1;
//                                    menuIndex = 1;
//                                    playerIDchoice = 0;
//                                }

//                                if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) - 100, 200, 100), "Gate 2"))
//                                {
//                                    networkClientUIbuttons.networkClient.sendActivateTrap(2, playerIDchoice);
//                                    choiceIndex = 1;
//                                    menuIndex = 1;
//                                    playerIDchoice = 0;
//                                }

//                                if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2), 200, 100), "Gate 3"))
//                                {
//                                    networkClientUIbuttons.networkClient.sendActivateTrap(3, playerIDchoice);
//                                    choiceIndex = 1;
//                                    menuIndex = 1;
//                                    playerIDchoice = 0;
//                                }

//                                if (GUI.Button(new Rect((Screen.width / 2) + 400, (Screen.height / 2) + 100, 200, 100), "Gate 4"))
//                                {
//                                    networkClientUIbuttons.networkClient.sendActivateTrap(4, playerIDchoice);
//                                    choiceIndex = 1;
//                                    menuIndex = 1;
//                                    playerIDchoice = 0;
//                                }
//                                break;
//                        }
//                        break;
//                    case 2:
//                        break;
//                    case 3:
//                        break;
//                    case 4:
//                        break;
//                }
//                 break;
//        }

//    }