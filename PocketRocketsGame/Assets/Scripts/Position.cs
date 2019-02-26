using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Position : MonoBehaviour {

    public Transform player;
    PlayerStats stats;
    public Text position;

    // get the players stats
    void Start()
    {
       // position.font = Resources.Load<Font>("Fonts/JazzCreateBubble");
        stats = player.GetComponent<PlayerStats>();
    }

    // draw players position on screen
    void Update()
    {
        if (player.gameObject.activeSelf)
        {
            transform.gameObject.SetActive(true);
            position.text = stats.getPosition().ToString();
        }
        else
        {
            transform.gameObject.SetActive(false);
        }
    }
}
