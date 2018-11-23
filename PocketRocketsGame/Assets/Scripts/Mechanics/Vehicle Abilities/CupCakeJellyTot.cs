using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCakeJellyTot : MonoBehaviour {
    public float slow = 2.0f;
	// Use this for initialization
	void Start () {
        GetComponent<MeshRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collided)
    {

        if (collided.CompareTag("Car") && collided.GetComponentInParent<PlayerStats>().immune == false)
        {
            collided.GetComponentInParent<PlayerStats>().setSlowDownFactor(slow);
            
        }


    }

    void OnTriggerExit(Collider collided) {
        collided.GetComponentInParent<PlayerStats>().setSlowDownFactor(1.0f);
    }


}
