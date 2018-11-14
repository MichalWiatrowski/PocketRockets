using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : MonoBehaviour {

    public float wallTime = 0.4f;
    public float wallSpeed = 1f;
    public float carDelay = 1f;
    public bool moving = false;

    void OnTriggerEnter(Collider collided)
    {
        // check to see if trap is colliding with a vehicle
        if (collided.CompareTag("Car") && collided.GetComponent<PlayerStats>().immune == false)
        {
            // run the trap function when collided with vehicle
            StartCoroutine( Trap(collided));
        }
    }

    IEnumerator Trap(Collider player)
    {
        // start moving the wall up
        moving = true;

        // delay for when the car needs to stop 
        yield return new WaitForSeconds(carDelay); 

        // get information from move script on the vehicles and apply boost
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.speed = 0f;

        // how long the player is stoped for
        yield return new WaitForSeconds(wallTime);

        // return the player to normal speed
        stats.speed = 4f;

        // clean up
        Destroy(gameObject);
    }

    void Update()
    {
        // if the trap has been triggered move the wall up untill it reaches a certain point
        if (moving)
        {
            if (transform.position.y <= -1)
            {
                transform.Translate(0f, wallSpeed * Time.deltaTime, 0f);
            }
        }
    }
}
