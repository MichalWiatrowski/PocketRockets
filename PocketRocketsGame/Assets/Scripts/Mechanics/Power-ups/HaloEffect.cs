using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloEffect : MonoBehaviour {

    PlayerStats stats;

    // Use this for initialization
    void Start () {
        stats = gameObject.GetComponent<PlayerStats>();  
	}
	
	// Update is called once per frame
	void Update () {
        Behaviour halo = (Behaviour)GetComponent("Halo");
        if (stats.getImmune() == true)
        {
            // GetComponent(Halo).enabled = true;

            halo.enabled = true;
        }
        else {
            halo.enabled = false;
        }
	}
}
