using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrap : MonoBehaviour {

    public float iceTime = 3f;
    public float carDelay = 0.8f;

    //public GameObject iceEffect;
    //Vector3 effectPos;

    void Start()
    {
        // hides the ice block untill it is collided with
        GetComponent<MeshRenderer>().enabled = false;

        //// offset the effect slightly
        //effectPos = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);

        //// creates a particle affect where the ice block is
        //Instantiate(iceEffect, effectPos, transform.rotation);
    }

    void OnTriggerEnter(Collider collided)
    {
        // check to see if trap is colliding with a vehicle
       if (collided.CompareTag("Car") && collided.GetComponent<CarValues>().immune == false)
        {
            // run the trap function when collided with vehicle
            StartCoroutine(Trap(collided));
        }
    }

    IEnumerator Trap(Collider player)
    {

        // delay for when the car needs to stop 
        yield return new WaitForSeconds(carDelay);

        // get information from move script on the vehicles and apply the freeze and reveal the ice
        GetComponent<MeshRenderer>().enabled = true;
        Move movement = player.GetComponent<Move>();
        movement.speed = 0f;

        // how long the player is stoped for
        yield return new WaitForSeconds(iceTime);

        // return the player to normal speed
        movement.speed = 4f;

        // clean up
        Destroy(gameObject);
    }
}
