using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    // player stat variables
    public string playerName;
    public float speed = 4f;
    public bool immune = false;
    public int points = 600;
    public int position = 1;
    public bool winner = false;
    public bool fallingThroughTeleport = false;

    public bool getFallingThroughTeleport()
    {

        return fallingThroughTeleport;
    }
}
