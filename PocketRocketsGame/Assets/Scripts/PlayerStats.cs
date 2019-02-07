using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public readonly int[] LANE = { -7, -10, -13, -16 };
    public int currentLane;

    // player stat variables
    public string playerName;
    public float speed = 4f;
    private float defaultSpeed = 30.0f;
    public float slowDownFactor = 1.0f;
    public bool immune = false;
    public int points = 600;
    public int position = 1;
    public bool winner = false;
    public bool fallingThroughTeleport = false;
    public bool trappedInBubble = false;
    public bool inTheAir = false;
    public int nextGate = 1;
    public bool switchLeft = true;
    public bool switchRight = true;
    public bool jumping = false;

    void Start()
    {
        //defaultSpeed = speed;
    }
    private void Update()
    {

    }

    public bool getFallingThroughTeleport()
    {
        return fallingThroughTeleport;
    }

    public void setSlowDownFactor(float slowDown)
    {
        slowDownFactor = slowDown;
    }

    public float getDefaultSpeed()
    {
        return defaultSpeed;
    }

    //Set up default values based on vehicles
    public void setUpPlayerStats(int vehicleID)
    {
        if (vehicleID == 0)
        {
            defaultSpeed = 30;
        }
        else if (vehicleID == 1)
        {
            defaultSpeed = 35;
        }
        else if (vehicleID == 2)
        {
            defaultSpeed = 40;
        }
    }


    /// Colidders
 
    void OnTriggerEnter(Collider collided)
    {
        if (collided.CompareTag("Plane"))
        {
            Debug.Log("Collided with Plane");



           inTheAir = false;
           GetComponent<Rigidbody>().useGravity = true;
           GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

           jumping = false;
        }
    }
   
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Plane"))
        {
            Debug.Log("Collided with Plane");
            jumping = false;
        }

    }
 }
