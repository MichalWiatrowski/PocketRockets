﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    List<int> lanes = new List<int>();
    int randLane;
    int randZ;

    // Use this for initialization
    void Start () {
        lanes.Add(-5);
        lanes.Add(-9);
        lanes.Add(-13);
        lanes.Add(-17);
        SetRandPos();
	}

    void SetRandPos()
    {
        randLane = Random.Range(0, 4);
        randZ = Random.Range(-70, 2100);
       // transform.Translate(lanes[randLane], 0f, randZ);
        transform.SetPositionAndRotation(new Vector3 (lanes[randLane], -0.5f, randZ), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void OnTriggerEnter(Collider collided)
    {
        // check to see if gate is colliding with a vehicle
        if (collided.CompareTag("Player"))
        {
            ReduceSpeed(collided);
        }
    }

    void OnTriggerExit(Collider collided)
    {
        // check to see if gate is colliding with a vehicle
        if (collided.CompareTag("Player"))
        {
            IncreaseSpeed(collided);
        }
    }

    void ReduceSpeed(Collider player)
    {
        PlayerStats stats = player.GetComponentInParent<PlayerStats>();

        //stats.setSpeed(stats.getSpeed() * 0.9f);
        stats.setSlowDownFactor(0.9f);
    }

    void IncreaseSpeed(Collider player)
    {
        PlayerStats stats = player.GetComponentInParent<PlayerStats>();

        //stats.setSpeed(stats.getDefaultSpeed());
       // stats.setSlowDownFactor(1.0f);
        stats.resetSlowDownFactor();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
