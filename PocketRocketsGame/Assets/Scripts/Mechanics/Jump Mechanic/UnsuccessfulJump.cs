using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnsuccessfulJump : MonoBehaviour {

    [SerializeField]
    private float speedDecrementValue = 1.0f;
    private ParticleSystem sparks;

    //Called before first frame
    void Awake()
    {
        sparks = GetComponent<ParticleSystem>();
    }

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
            sparks.Play();
            Debug.Log("Player Collided with Unsuccessful collider");
        }
    }
}
