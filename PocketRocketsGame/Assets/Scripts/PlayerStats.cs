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

    public int points = 600;
    public int position = 1;
    public int nextGate = 1;

    public bool winner = false;
    public bool fallingThroughTeleport = false;
    public bool trappedInBubble = false;
    public bool inTheAir = false;
    public bool jumping = false;
    public bool switchLeft = true;
    public bool switchRight = true;
    public bool immune = false;

    void Start()
    {
        //defaultSpeed = speed;
    }
    
    //Falling Through Teleport
    public bool getFallingThroughTeleport(){ return fallingThroughTeleport;}
    public void setFallingThroughTeleport(bool flag) { fallingThroughTeleport = flag;}
    //In The Air
    public bool getInTheAir(){ return inTheAir;}
    public void setInTheAir(bool flag) { inTheAir = flag;}
    //Trapped in Bubble
    public bool getTrappedInBubble() { return trappedInBubble; }
    public void setTrappedInBubble(bool flag) { trappedInBubble = flag; }
    //Is Winner
    public bool isWinner() { return winner; }
    public void getIsWinner(bool flag) { winner = flag; }
    //Jumping
    public bool getJumping() { return jumping; }
    public void setJumping(bool flag) { jumping = flag; }
    //Switch Left
    public bool getSwitchLeft() { return switchLeft; }
    public void setSwitchLeft(bool flag) { switchLeft = flag; }
    //Switch Right
    public bool getSwitchRight() { return switchRight; }
    public void setSwitchRight(bool flag) { switchRight = flag; }
    //Immune
    public bool getImmune() { return immune; }
    public void setImmune(bool flag) { immune = flag; }
    //Default Speed
    public float getDefaultSpeed() { return defaultSpeed; }
    public void setDefaultSpeed(float spd) { defaultSpeed = spd; }
    //Slow down Factor
    public float getSlowDownFactor() { return slowDownFactor; }
    public void setSlowDownFactor(float slowDown){ slowDownFactor = slowDown; }
    //Name
    public string getPlayerName() {  return playerName; }
    public void setPlayerName(string name) { playerName = name; }
    //Points
    public int getPoints() { return points; }
    public void setPoints(int pointValue) { points = pointValue; }
    //Position
    public int getPosition() { return position; }
    public void setPosition(int positionValue) { position = positionValue; }
    //Next Gate
    public int getNextGate() { return nextGate; }
    public void setNextGate(int gateNum) { nextGate = gateNum; }
    //Current Lane
    public int getCurrentLane() { return currentLane; }
    public void setCurrentLane(int currLane) { currentLane = currLane; }

 
    //Set up default values based on vehicles
    public void setUpPlayerStats(int vehicleID)
    {
        if (vehicleID == 0)
        {
            defaultSpeed = 35;
        }
        else if (vehicleID == 1)
        {
            defaultSpeed = 35;
        }
        else if (vehicleID == 2)
        {
            defaultSpeed = 35;
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

    //Speed
    public float getSpeed() { return speed; }
    public void setSpeed(float spd) { speed = spd; }

    public void incrementSpeed(float incrementValue)
    {

        speed += incrementValue;
    }

    public void decrementSpeed(float decrementValue)
    {

        speed -= decrementValue;
    }
}
