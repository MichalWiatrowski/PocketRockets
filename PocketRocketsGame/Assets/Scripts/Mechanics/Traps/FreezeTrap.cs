﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrap : MonoBehaviour {

    private float iceTime = 1f;
    private float carDelay = 0.01f;
    public AudioClip blizzard;
    private AudioSource blizzSource;

    //public GameObject iceEffect;
    //Vector3 effectPos;

    private void Awake()
    {
        blizzSource = GetComponentInParent<AudioSource>();
    }

    void Start()
    {
        // hides the ice block untill it is collided with
        GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<BoxCollider>().enabled = true;

        //// offset the effect slightly
        //effectPos = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);

        //// creates a particle affect where the ice block is
        //Instantiate(iceEffect, effectPos, transform.rotation);
    }

    void OnTriggerEnter(Collider collided)
    {
        // check to see if trap is colliding with a vehicle
       if (collided.CompareTag("Car") && collided.GetComponentInParent<PlayerStats>().immune == false)
        {
            // run the trap function when collided with vehicle
            blizzSource.PlayOneShot(blizzard, 0.1f);
            StartCoroutine(Trap(collided));
        }
    }

    IEnumerator Trap(Collider player)
    {

        // delay for when the car needs to stop 
        yield return new WaitForSeconds(carDelay);

        // get information from move script on the vehicles and apply the freeze and reveal the ice
        GetComponent<MeshRenderer>().enabled = true;
        PlayerStats stats = player.GetComponentInParent<PlayerStats>();
        stats.speed = 0.0f;

        // how long the player is stoped for
        yield return new WaitForSeconds(iceTime);

        // return the player to normal speed
        stats.speed = stats.defaultSpeed;

        // clean up
        Destroy(gameObject);
    }
}
