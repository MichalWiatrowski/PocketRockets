using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessfulJump : MonoBehaviour {

    [SerializeField]
    private float speedIncrementValue = 1.0f;

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
            collided.GetComponent<PlayerStats>().incrementSpeedStack(speedIncrementValue);
            Debug.Log("Player Collided with Successful collider");
        }
    }

    }
