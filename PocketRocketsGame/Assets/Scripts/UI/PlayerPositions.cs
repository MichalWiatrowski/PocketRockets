using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerPositions : MonoBehaviour {

    public List<GameObject> players;
    public List<Text> positionTextList;
    public List<GameObject> positionImages;

    private bool[] textAssigned;
    private int playerCount = 0;

    // Use this for initialization
    void Start () {
        for (int j = 0; j < players.Count; j++)
        {
            if (players[j].gameObject.activeSelf)
            {
                playerCount++;
            }
        }

        Debug.Log("Player count is: " + playerCount);

        textAssigned = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            textAssigned[i] = false;
        }

        if (playerCount == 3)
        {
            positionTextList[3].text = "";
            textAssigned[3] = true;
            positionImages[3].SetActive(false);

        }
        else if (playerCount == 2)
        {

            positionTextList[3].text = "";
            textAssigned[3] = true;
            positionImages[3].SetActive(false);

            positionTextList[2].text = "";
            textAssigned[2] = true;
            positionImages[2].SetActive(false);

        }

    }
    private void Awake()
    {
    }
    // Update is called once per frame
    void Update () {

        for (int x = 0; x < players.Count; x++)
        {
            switch (players[x].GetComponent<PlayerStats>().getPosition())
            {
                case 1:
                    textAssignment(x, 0);
                break;
                case 2:
                    textAssignment(x, 1);
                    break;
                case 3:
                    textAssignment(x, 2);
                    break;
                case 4:
                    textAssignment(x, 3);
                    break;
            }

        }


        for (int i = 0; i < playerCount; i++)
        {
            textAssigned[i] = false;
        }
    }

    private void textAssignment(int iPos, int textboxPosition) {

        if (textAssigned[textboxPosition] == false)
        {
            positionTextList[textboxPosition].text = players[iPos].GetComponent<PlayerStats>().getPlayerName();
            textAssigned[textboxPosition] = true;
        }
        else if (textAssigned[textboxPosition + 1] == false)
        {
            positionTextList[textboxPosition + 1].text = players[iPos].GetComponent<PlayerStats>().getPlayerName();
            textAssigned[textboxPosition + 1] = true;
        }
        else if (textAssigned[textboxPosition + 2] == false)
        {
            positionTextList[textboxPosition + 2].text = players[iPos].GetComponent<PlayerStats>().getPlayerName();
            textAssigned[textboxPosition + 2] = true;
        }
        else {
            positionTextList[textboxPosition + 3].text = players[iPos].GetComponent<PlayerStats>().getPlayerName();
            textAssigned[textboxPosition + 3] = true;

        }

    } 
}
