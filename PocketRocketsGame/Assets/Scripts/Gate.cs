﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    int count = 1;

    void OnTriggerEnter(Collider collided)
    {
        // check to see if gate is colliding with a vehicle
        if (collided.CompareTag("Car"))
        {
            StartCoroutine(GatePoints(collided));
        }
    }

    IEnumerator GatePoints(Collider player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        // assign vehicle positions based on how many vehicles have passed through gate
        // then assign points
        if (count == 1)
        {
            stats.points += 300;
            stats.position = 1;
            yield return new WaitForSeconds(0.5f);
        }
        else if (count == 2)
        {
            stats.points += 225;
            stats.position = 2;
            yield return new WaitForSeconds(0.5f);
        }
        else if (count == 3)
        {
            stats.points += 150;
            stats.position = 3;
            yield return new WaitForSeconds(0.5f);
        }
        else if (count == 4)
        {
            stats.position += 100;
            stats.position = 4;
            yield return new WaitForSeconds(0.5f);
        }
        count++;
        networkServerUIbuttons.networkServer.sendPoints();
    }
}