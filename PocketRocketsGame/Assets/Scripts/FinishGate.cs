using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGate : MonoBehaviour {

    int count = 1;
    public List<Transform> winText;

    void OnTriggerEnter(Collider collided)
    {
        // check to see if trap is colliding with a vehicle
        if (collided.CompareTag("Car"))
        {
            FindWinner(collided);
        }
    }

    void FindWinner(Collider player)
    {
        PlayerStats stats = player.GetComponentInParent<PlayerStats>();

        if (count == 1)
        {
            stats.winner = true;

            for (int x = 0; x < winText.Count; x++)
            {
                winText[x].gameObject.SetActive(true);
            }
        }
        count++;
    }
}
