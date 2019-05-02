using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

    public float boost = 1.4f;
    public float boostTime = 1.5f;

    //void OnTriggerEnter (Collider collided)
    //{
    //    // check to see if power up is colliding with a vehicle
    //    if (collided.CompareTag("Car"))
    //    {
    //        // run the pickup function when collided with vehicle
    //      StartCoroutine(  Pickup(collided));
    //    }
    //}
    public void activateSpeedBoost()
    {
        StartCoroutine(speedBoost());

    }

    //IEnumerator Pickup(Collider player)
    //{
    //    // get information from move script on the vehicles and apply boost
    //    PlayerStats stats = player.GetComponent<PlayerStats>();
    //    stats.speed *= boost;

    //    // "removes" the power up before the timer
    //    GetComponent<MeshRenderer>().enabled = false;
    //    GetComponent<SphereCollider>().enabled = false;

    //    yield return new WaitForSeconds(boostTime);

    //    // return vehicle speed to normal after boost
    //    stats.speed /= boost;

    //    // clean up
    //    Destroy(gameObject);
    //}
    IEnumerator speedBoost()
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        stats.setSlowDownFactor(boost);

        yield return new WaitForSeconds(boostTime);

        // return vehicle speed to normal after boost
        //stats.speed /= boost;
        //stats.setSpeed(stats.getSpeed() / boost);
        stats.resetSlowDownFactor();

    }
}
