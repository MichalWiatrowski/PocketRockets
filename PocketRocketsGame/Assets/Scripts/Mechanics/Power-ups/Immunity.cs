﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity : MonoBehaviour {

    public float immunityTime =  5.0f;

    void OnTriggerEnter (Collider collided) {

        //Check to see if power up is colliding with a car.
        if (collided.CompareTag("Car")) {
            //run the pick function when collided with vehicle.
            StartCoroutine(  Pickup(collided));
        }
    }
    public void activateImmunity()
    {
        StartCoroutine(activate());
    }
    IEnumerator activate()
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        stats.setImmune(true);

        yield return new WaitForSeconds(immunityTime);

        stats.setImmune(false);

       
    }


    IEnumerator Pickup(Collider player) {
        //Get Car Immunity information from immune script and apply immunity.
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.setImmune(true);

        // "removes" the power up before the timer
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(immunityTime);

        //Return immunity to normal.
        stats.setImmune(false);

        // Clean up
        Destroy(gameObject);

    }
}
