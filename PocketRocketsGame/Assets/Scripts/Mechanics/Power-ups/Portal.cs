using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public float teleportDistance = 40.0f;

	
    void OnTriggerEnter(Collider collided)
    {

        //Check to see if power up is colliding with a car.
        if (collided.CompareTag("Car"))
        {
            //run the pick function when collided with vehicle.
            collided.transform.Translate(0, 8, teleportDistance);
            
        }
    }




    // Update is called once per frame
    void Update () {
		
	}
}
