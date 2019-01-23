using System.Collections;
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
        PlayerStats stats = player.GetComponentInParent<PlayerStats>();

        player.GetComponentInParent<PlayerStats>().nextGate = player.GetComponentInParent<PlayerStats>().nextGate + 1;
        // assign vehicle positions based on how many vehicles have passed through gate
        // then assign points
        if (count == 1)
        {
            stats.points += 100;
            stats.position = 1;
            yield return new WaitForSeconds(0.1f);
        }
        else if (count == 2)
        {
            stats.points += 150;
            stats.position = 2;
            yield return new WaitForSeconds(0.1f);
        }
        else if (count == 3)
        {
            stats.points += 225;
            stats.position = 3;
            yield return new WaitForSeconds(0.1f);
        }
        else if (count == 4)
        {
            stats.position += 300;
            stats.position = 4;
            yield return new WaitForSeconds(0.1f);
        }
        count++;
        networkServerUIbuttons.networkServer.SendPoints_Gate();
    }
}
