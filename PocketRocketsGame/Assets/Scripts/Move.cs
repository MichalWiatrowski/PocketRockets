using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private PlayerStats stats;
    private Vector3 trackValues;

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
        //if (stats.getSwitchLeft() && stats.getCurrentLane() != 3)
        {
            newPos = new Vector3(stats.LANE[stats.getCurrentLane() + 1], transform.position.y, transform.position.z);
            lerp = true;
            stats.setCurrentLane(stats.getCurrentLane() + 1);
        }
    }

    public void MoveRight()
    {
        if (stats.getSwitchRight() && stats.getCurrentLane() != 0)
        {
            newPos = new Vector3(stats.LANE[stats.getCurrentLane() - 1], transform.position.y, transform.position.z);
            lerp = true;
            stats.setCurrentLane(stats.getCurrentLane() - 1);
        }
    }

    // Update is called once per frame
    void Update () {

        // movement
        if (stats.getFallingThroughTeleport() == false && stats.getTrappedInBubble() == false && stats.getInTheAir() == false)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, ((stats.getSpeed() / stats.getSlowDownFactor())));
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
	}
}
