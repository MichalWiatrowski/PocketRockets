using System.Collections;
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
       if (collided.CompareTag("Player") && collided.GetComponent<PlayerStats>().getImmune() == false)
        {
            GetComponent<MeshRenderer>().enabled = true;
            // run the trap function when collided with vehicle
            blizzSource.PlayOneShot(blizzard, 0.1f);
            StartCoroutine(Trap(collided));
        }
    }

    IEnumerator Trap(Collider player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        gameObject.GetComponent<BoxCollider>().enabled = false;

        stats.setTrapped(true);
        // send switch state to the client
        stats.setSwitchStateL(0);
        networkServerUIbuttons.networkServer.sendSwitchStateL();
        stats.setSwitchStateR(0);
        networkServerUIbuttons.networkServer.sendSwitchStateR();

        // delay for when the car needs to stop 
        yield return new WaitForSeconds(carDelay);

        // get information from move script on the vehicles and apply the freeze and reveal the ice

        //GetComponent<MeshRenderer>().enabled = true;
        

        GetComponent<MeshRenderer>().enabled = true;

        stats.setSpeed(0.0f);

        // how long the player is stoped for
        yield return new WaitForSeconds(iceTime);

        stats.setSwitchStateL(1);
        networkServerUIbuttons.networkServer.sendSwitchStateL();
        stats.setSwitchStateR(1);
        networkServerUIbuttons.networkServer.sendSwitchStateR();

        // return the player to normal speed
        stats.resetSpeed();
        stats.setTrapped(false);

        // clean up
        Destroy(gameObject);
    }
}
