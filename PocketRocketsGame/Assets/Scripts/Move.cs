using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private PlayerStats stats;
    void Start()
    {
        stats = gameObject.GetComponent<PlayerStats>();

    }
    // Update is called once per frame
    void Update () {

        // movement
        if (stats.fallingThroughTeleport == false)
        {
            transform.Translate(0f, 0f, stats.speed * Time.deltaTime);
        }
	}
}
