using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnsuccessfulJump : MonoBehaviour {

    [SerializeField]
    private float speedDecrementValue = 1.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider collided)
    {
        if (collided.CompareTag("Player"))
        {
            collided.GetComponent<PlayerStats>().decrementSpeed(speedDecrementValue);
            Debug.Log("Player Collided with Unsuccessful collider");
        }
    }
}
