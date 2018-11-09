using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

    public float boost = 1.6f;
    public float boostTime = 3f;

    void OnTriggerEnter (Collider collided)
    {
        // check to see if power up is colliding with a vehicle
        if (collided.CompareTag("Car"))
        {
            // run the pickup function when collided with vehicle
          StartCoroutine(  Pickup(collided));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        // get information from move script on the vehicles and apply boost
        Move movement = player.GetComponent<Move>();
        movement.speed *= boost;

        // "removes" the power up before the timer
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(boostTime);

        // return vehicle speed to normal after boost
        movement.speed /= boost;

        // clean up
        Destroy(gameObject);
    }
}
