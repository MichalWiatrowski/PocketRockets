using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : MonoBehaviour {

    public float wallTime = 0.4f;
    public float wallSpeed = 1f;
    public float carDelay = 0.1f;
    public bool moving = false;
    public AudioClip crash;
    private AudioSource crashSource;

    private void Awake()
    {

        crashSource = GetComponentInParent<AudioSource>();
    }

    void OnTriggerEnter(Collider collided)
    {
        // check to see if trap is colliding with a vehicle
        if (collided.CompareTag("Player") && collided.GetComponent<PlayerStats>().immune == false)
        {
            crashSource.PlayOneShot(crash, 0.2f);
            // run the trap function when collided with vehicle
            PlayerStats stats = collided.GetComponent<PlayerStats>();
            stats.speed = 0f;
            StartCoroutine( Trap(stats));
        }
    }

    IEnumerator Trap(PlayerStats stats)
    {
        // start moving the wall up
        moving = true;

        // delay for when the car needs to stop 
        yield return new WaitForSeconds(carDelay); 

        // get information from move script on the vehicles and apply boost
        //PlayerStats stats = player.GetComponentInParent<PlayerStats>();
        //stats.speed = 0f;

        // how long the player is stoped for
        yield return new WaitForSeconds(wallTime);

        // return the player to normal speed
        stats.speed = stats.defaultSpeed;

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
