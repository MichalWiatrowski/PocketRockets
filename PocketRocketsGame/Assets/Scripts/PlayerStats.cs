using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public readonly int[] LANE = { -7, -10, -13, -16 };
    public int currentLane;

    // player stat variables
    public string playerName;
    public float speed = 4f;
    public float defaultSpeed = 30.0f;
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

    public void incrementSpeed(float incrementValue)
    {

        speed += incrementValue;
    }

    public void decrementSpeed(float decrementValue)
    {

        speed -= decrementValue;
    }
}
