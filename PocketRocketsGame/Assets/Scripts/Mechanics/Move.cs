using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private PlayerStats stats;
    private Vector3 trackValues;
    private float timer = 0;

    public Vector3 newPos;
    public bool lerp = false;


    void Start()
    {
        stats = gameObject.GetComponent<PlayerStats>();
        GameObject tempPlane = GameObject.Find("Plane");
        trackValues = new Vector3(tempPlane.transform.position.x, tempPlane.transform.position.y, tempPlane.transform.position.z);
    }

    public void MoveLeft()
    {
        if (stats.getSwitchLeft())
        {
            newPos = new Vector3(stats.LANE[stats.getCurrentLane() - 1], transform.position.y, transform.position.z);
            lerp = true;
            stats.setCurrentLane(stats.getCurrentLane() - 1);
            if (stats.getCurrentLane() == 2)
            {
                stats.setSendSwitchState(true);
            }
        }
    }

    public void MoveRight()
    {
        if (stats.getSwitchRight())
        {
            newPos = new Vector3(stats.LANE[stats.getCurrentLane() + 1], transform.position.y, transform.position.z);
            lerp = true;
            stats.setCurrentLane(stats.getCurrentLane() + 1);
            if (stats.getCurrentLane() == 1)
            {
                stats.setSendSwitchState(true);
            }
        }
    }

    // Update is called once per frame
    void Update () {

        // movement
        if (stats.getFallingThroughTeleport() == false && stats.getTrappedInBubble() == false && stats.getInTheAir() == false)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, ((stats.getSpeed() + stats.getStackingSpeedBuff()) * stats.getSlowDownFactor()));
            //transform.Translate(0f, 0f, (stats.speed / stats.slowDownFactor) * Time.deltaTime);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
        }

        if (lerp)
        {
            Vector3 tempVec = new Vector3(0f, transform.position.y, transform.position.z);
            tempVec.x = Mathf.Lerp(transform.position.x, newPos.x, 0.1f);
            transform.position = tempVec;

            if (transform.position.x - newPos.x <= 0.1 && transform.position.x - newPos.x >= -0.1)
            {
                transform.GetComponentInParent<BoxCollider>().enabled = true;
                transform.GetComponentInParent<Rigidbody>().useGravity = true;
                lerp = false;
            }
        }

        if (stats.getCurrentLane() == 0)
        {
            timer += Time.deltaTime;

            if (timer > 1)
            {
                stats.setSwitchLeft(false);
                stats.setSwitchStateL(0);
                networkServerUIbuttons.networkServer.sendSwitchStateL();
            }
        }
        else if (stats.getCurrentLane() == 3)
        {
            timer += Time.deltaTime;

            if (timer > 1)
            {
                stats.setSwitchRight(false);
                stats.setSwitchStateR(0);
                networkServerUIbuttons.networkServer.sendSwitchStateR();
            }
        }
        else
        {
            if (stats.getSendSwitchState())
            { 
                stats.setSwitchLeft(true);
                stats.setSwitchStateL(1);
                networkServerUIbuttons.networkServer.sendSwitchStateL();
                stats.setSwitchRight(true);
                stats.setSwitchStateR(1);
                networkServerUIbuttons.networkServer.sendSwitchStateR();
                stats.setSendSwitchState(false);
            }
        }
    }
}
