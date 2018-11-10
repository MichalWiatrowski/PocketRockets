using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Winner : MonoBehaviour {

    public List<Transform> players;
    PlayerStats stats;
    public Text winner;

    // get the players stats
    void Start()
    {
        gameObject.SetActive(false);
    }

    // draw players position on screen
    void Update()
    {
        for (int x = 0; x < players.Count; x++)
        {
            stats = players[x].GetComponent<PlayerStats>();
            if (stats.winner == true)
            {
                winner.text = stats.playerName;
            }
        }
    }
}
