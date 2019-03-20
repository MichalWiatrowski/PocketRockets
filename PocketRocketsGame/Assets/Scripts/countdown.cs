using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class countdown : MonoBehaviour {

    public List<Transform> players;
    public Text countText;
    PlayerStats[] stats = new PlayerStats[4];

    void Start()
    {
        StartCoroutine(CountdownStart());
    }

    IEnumerator CountdownStart()
    {
        //for (int x = 0; x < networkServerUIbuttons.networkServer.getPlayerAmount(); x++)
        //{
        //    stats[x] = players[x].GetComponent<PlayerStats>();
        //    stats[x].setSpeed(0);
        //}

        //for (int x = 1; x < networkServerUIbuttons.networkServer.getPlayerAmount() + 1; x++)
        //{
        //    GameObject.Find("Player " + x).GetComponent<PlayerStats>().setCurrentLane(x - 1);
        //}

        for (int i = 0; i < 4; i++)
        {
            if (networkServerUIbuttons.networkServer.playersConnected[i] == 1)
            {
                stats[i] = players[i].GetComponent<PlayerStats>();
                stats[i].setSpeed(0);
                stats[i].setCurrentLane(i);
                
            }
        }


            countText.gameObject.SetActive(true);
        countText.text = "3";

        yield return new WaitForSeconds(1);

        countText.text = "2";

        yield return new WaitForSeconds(1);

        countText.text = "1";

        yield return new WaitForSeconds(1);

        countText.text = "Go!";

        yield return new WaitForSeconds(1);

        countText.gameObject.SetActive(false);

        //for (int x = 0; x < networkServerUIbuttons.networkServer.getPlayerAmount(); x++)
        //{
        //    stats[x].resetSpeed();
        //}


        for (int i = 0; i < 4; i++)
        {
            if (networkServerUIbuttons.networkServer.playersConnected[i] == 1)
            {
                stats[i].resetSpeed();
            }
        }

    }
}
