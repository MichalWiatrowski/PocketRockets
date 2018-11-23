using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private PlayerStats stats;
    private Vector3 trackValues;
    void Start()
    {
        stats = gameObject.GetComponent<PlayerStats>();
        GameObject tempPlane = GameObject.Find("Plane");
        trackValues = new Vector3(tempPlane.transform.position.x, tempPlane.transform.position.y, tempPlane.transform.position.z);

    }
    // Update is called once per frame
    void Update () {

        // movement
        if (stats.fallingThroughTeleport == false && stats.trappedInBubble == false && stats.inTheAir == false)
        {

            transform.Translate(0f, 0f, (stats.speed / stats.slowDownFactor) * Time.deltaTime);
        }
	}
}
