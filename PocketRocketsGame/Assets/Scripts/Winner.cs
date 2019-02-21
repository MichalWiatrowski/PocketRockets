using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Winner : MonoBehaviour {

    public List<Transform> players;
    public AudioClip winningClip;
    PlayerStats stats;
    public Text winner;

    private AudioSource winningSource;

    void Awake()
    {
        winningSource = GetComponent<AudioSource>();
    }
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
            if (stats.isWinner() == true)

            {
                
                winner.fontSize = 70;
                winner.text = stats.getPlayerName() + " Wins!";
            }
        }
    }
}
