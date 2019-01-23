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
    public void ActivateImmunity()
    {
        StartCoroutine(Activate());
    }
    IEnumerator Activate()
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        stats.immune = true;

        yield return new WaitForSeconds(immunityTime);

        stats.immune = false;

       
    }


    IEnumerator Pickup(Collider player) {
        //Get Car Immunity information from immune script and apply immunity.
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.immune = true;

        // "removes" the power up before the timer
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(immunityTime);

        //Return immunity to normal.
        stats.immune = false;

        // Clean up
        Destroy(gameObject);

    }
}
