using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCakeJellyTot : MonoBehaviour {
    public float slow = 0.5f;
	// Use this for initialization
	void Start () {
        GetComponent<MeshRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collided)
    {

        if (collided.CompareTag("Player") && collided.GetComponent<PlayerStats>().getImmune() == false)
        {
            GetComponent<MeshRenderer>().enabled = true;
            collided.GetComponent<PlayerStats>().setSlowDownFactor(slow);
            
        }


    }

    void OnTriggerExit(Collider collided) {
        collided.GetComponent<PlayerStats>().setSlowDownFactor(1.0f);
    }


}
