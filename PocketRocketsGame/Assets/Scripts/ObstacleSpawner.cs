using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    List<int> lanes = new List<int>();
    int randLane;
    int randZ;

    // Use this for initialization
    void Start () {
        lanes.Add(-7);
        lanes.Add(-10);
        lanes.Add(-13);
        lanes.Add(-16);
        SetRandPos();
	}

    void SetRandPos()
    {
        randLane = Random.Range(0, 4);
        randZ = Random.Range(-70, 2100);
        transform.Translate(lanes[randLane], 0f, randZ);
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

        stats.speed = stats.speed * 0.9f;
    }

    void IncreaseSpeed(Collider player)
    {
        PlayerStats stats = player.GetComponentInParent<PlayerStats>();

        stats.speed = stats.defaultSpeed;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
