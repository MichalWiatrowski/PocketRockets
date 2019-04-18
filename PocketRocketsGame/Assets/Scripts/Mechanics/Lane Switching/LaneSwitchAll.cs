using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSwitchAll : MonoBehaviour {

    public List<Transform> players;

    List<PlayerStats> stats = new List<PlayerStats>();
    List<Move> move = new List<Move>();
    List<int> lanes = new List<int>();
    int randLane;

    void Start()
    {
        for (int x = 0; x < 4; x++)
        {
            stats.Insert(x, players[x].GetComponent<PlayerStats>());
            move.Insert(x, players[x].GetComponent<Move>());
        }
    }

    public void JumbleLanes()
    {
        for (int x = 0; x < 4; x++)
        {
            lanes.Add(x);
        }

        for (int x = 0; x < 4; x++)
        {
            randLane = Random.Range(0, 4 - x);
            move[x].newPos = new Vector3(stats[x].LANE[lanes[randLane]], players[x].position.y, players[x].position.z);
            move[x].lerp = true;
            stats[x].setCurrentLane(lanes[randLane]);
            lanes.RemoveAt(randLane);
            players[x].GetComponent<BoxCollider>().enabled = false;
            players[x].GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void Update()
    {

    }
}
