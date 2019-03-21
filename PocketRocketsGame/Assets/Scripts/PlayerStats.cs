using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public readonly int[] LANE = { -5, -9, -13, -17 };
    private int currentLane;

    // player stat variables
    private string playerName;
    [SerializeField]
    private float speed = 30f;
    private float defaultSpeed = 30.0f;
    private float slowDownFactor = 1.0f;
    private float speedStack = 0.0f;

    private int points = 600;
    private int position = 1;
    private int nextGate = 1;

    private bool winner = false;
    private bool fallingThroughTeleport = false;
    private bool trappedInBubble = false;
    private bool inTheAir = false;
    private bool jumping = false;
    private bool switchLeft = true;
    private bool switchRight = true;
    private bool immune = false;
    private int switchStateL = 1;
    private int switchStateR = 1;

    float timer = 0;

    void Start()
    {
        //defaultSpeed = speed;
        //currentLane = networkServerUIbuttons.networkServer.playerID - 1;
    }

    //Falling Through Teleport
    public bool getFallingThroughTeleport() { return fallingThroughTeleport; }
    public void setFallingThroughTeleport(bool flag) { fallingThroughTeleport = flag; }
    //In The Air
    public bool getInTheAir() { return inTheAir; }
    public void setInTheAir(bool flag) { inTheAir = flag; }
    //Trapped in Bubble
    public bool getTrappedInBubble() { return trappedInBubble; }
    public void setTrappedInBubble(bool flag) { trappedInBubble = flag; }
    //Is Winner
    public bool isWinner() { return winner; }
    public void setWinner(bool flag) { winner = flag; }
    //Jumping
    public bool getJumping() { return jumping; }
    public void setJumping(bool flag) { jumping = flag; }
    //Switch Left
    public bool getSwitchLeft() { return switchLeft; }
    public void setSwitchLeft(bool flag) { switchLeft = flag; }
    //Switch Right
    public bool getSwitchRight() { return switchRight; }
    public void setSwitchRight(bool flag) { switchRight = flag; }
    //Switch State L
    public int getSwitchStateL() { return switchStateL; }
    public void setSwitchStateL(int flag) { switchStateL = flag; }
    //Switch State R
    public int getSwitchStateR() { return switchStateR; }
    public void setSwitchStateR(int flag) { switchStateR = flag; }
    //Immune
    public bool getImmune() { return immune; }
    public void setImmune(bool flag) { immune = flag; }
    //Default Speed
    public float getDefaultSpeed() { return defaultSpeed; }
    public void setDefaultSpeed(float spd) { defaultSpeed = spd; }
    //Slow down Factor
    public float getSlowDownFactor() { return slowDownFactor; }
    public void setSlowDownFactor(float slowDown) { slowDownFactor = slowDown; }
    public void resetSlowDownFactor() { slowDownFactor = 1.0f; }
    //Name
    public string getPlayerName() { return playerName; }
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

    public void resetSpeed() { speed = defaultSpeed; }

    public void incrementSpeedStack(float incrementValue)
    {
        if (speedStack < 0)
        {
            speedStack = 0;
        }
        speedStack += incrementValue;
        Debug.Log("Speed stack has been increased : " + getStackingSpeedBuff());
    }

    public void decrementSpeedStack(float decrementValue)
    {
        if (speedStack > 0)
        {
            speedStack = 0;
        }
        speedStack -= decrementValue;
        Debug.Log("Speed stack has been reduced : " + getStackingSpeedBuff());
    }

    public int getStackingSpeedBuff()
    {


        return System.Convert.ToInt16(speedStack);
    }
}