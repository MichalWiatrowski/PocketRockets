using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindCam : MonoBehaviour {

    public Camera mainCam;

    void OnTriggerEnter(Collider collided)
    {
        // check to see if is colliding with a vehicle
        if (collided.CompareTag("Car"))
        {
            ToggleBehind();
        }
    }

    void OnTriggerExit(Collider collided)
    {
        // check to see if is colliding with a vehicle
        if (collided.CompareTag("Car"))
        {
            UnToggleBehind();
        }
    }

    void ToggleBehind()
    {
        Follow cam = mainCam.GetComponent<Follow>();
        cam.behindCam = true;
    }

    void UnToggleBehind ()
    {
        Follow cam = mainCam.GetComponent<Follow>();
        cam.behindCam = false;
    }
}
