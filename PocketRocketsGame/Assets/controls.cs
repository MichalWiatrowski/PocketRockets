using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P has been pressed");
            GameObject.Find("Player 1").GetComponent<VehicleJump>().Jump();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject.Find("Player 2").GetComponent<PortalPowerUp>().CreatePortals();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameObject.Find("Player 1").GetComponent<Move>().MoveLeft();
        }

    }
}
