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
        if (stats.switchLeft && stats.currentLane != 3)
        {
            newPos = new Vector3(stats.LANE[stats.currentLane + 1], transform.position.y, transform.position.z);
            lerp = true;
            stats.currentLane++;
        }
    }

    public void MoveRight()
    {
        if (stats.switchRight && stats.currentLane != 0)
        {
            newPos = new Vector3(stats.LANE[stats.currentLane - 1], transform.position.y, transform.position.z);
            lerp = true;
            stats.currentLane--;
        }
    }

    // Update is called once per frame
    void Update () {

        // movement
        if (stats.fallingThroughTeleport == false && stats.trappedInBubble == false && stats.inTheAir == false)
        {

            transform.Translate(0f, 0f, (stats.speed / stats.slowDownFactor) * Time.deltaTime);
        }

        if (lerp)
        {
            Vector3 tempVec = new Vector3(0f, transform.position.y, transform.position.z);
            tempVec.x = Mathf.Lerp(transform.position.x, newPos.x, 0.1f);
            transform.position = tempVec;

            if (transform.position.x == newPos.x)
            {
                transform.GetComponentInChildren<BoxCollider>().enabled = true;
                transform.GetComponentInChildren<Rigidbody>().useGravity = true;
                lerp = false;
            }
        }
	}
}
