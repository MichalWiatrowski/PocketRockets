using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{

    public List<Transform> players;
    int playerCount = 4;
    float timer = 0;
    int tempVal = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            FindPositions();
            networkServerUIbuttons.networkServer.sendPosition();
            timer = 0;
        }
    }

    void FindPositions()
    {
        for (int x = 0; x < playerCount; x++)
        {
            PlayerStats stats = players[x].GetComponent<PlayerStats>();
            tempVal = 1;
            for (int y = 0; y < playerCount; y++)
            {
                if (players[x].transform.position.z < players[y].transform.position.z)
                {
                    tempVal++;
                }
            }
            stats.setPosition(tempVal);
        }
    }
}


