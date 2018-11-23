using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloEffect : MonoBehaviour {

    PlayerStats imm;

    // Use this for initialization
    void Start () {
        imm = gameObject.GetComponent<PlayerStats>();  
	}
	
	// Update is called once per frame
	void Update () {
        Behaviour halo = (Behaviour)GetComponent("Halo");
        if (imm.immune == true)
        {
            // GetComponent(Halo).enabled = true;

            halo.enabled = true;
        }
        else {
            halo.enabled = false;
        }
	}
}
