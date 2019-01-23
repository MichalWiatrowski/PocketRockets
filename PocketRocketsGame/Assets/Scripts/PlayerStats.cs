using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    // player stat variables
    public string playerName;
    public float speed = 4f;
    public float defaultSpeed = 0.0f;
    public float slowDownFactor = 1.0f;
    public bool immune = false;
    public int points = 600;
    public int position = 1;
    public bool winner = false;
    public bool fallingThroughTeleport = false;
    public bool trappedInBubble = false;
    public bool inTheAir = false;
    public int nextGate = 1;

    void Start()
    {
        defaultSpeed = speed;
    }
    

    public bool getFallingThroughTeleport()
    {

        return fallingThroughTeleport;
    }

    public void setSlowDownFactor(float slowDown)
    {
        slowDownFactor = slowDown;
    }

    private void Update()
    {
        
    }

    void OnTriggerEnter(Collider collided)
    {
        if (collided.CompareTag("Plane"))
        {
            Debug.Log("Collided with Plane");

            inTheAir = false;
        }
    }
   }
