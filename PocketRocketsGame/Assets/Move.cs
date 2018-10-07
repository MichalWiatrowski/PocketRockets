using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float speed;

    // Use this for initialization
    void Start () {
        speed = 1f;
	}
	
	// Update is called once per frame
	void Update () {

        // movement
        transform.Translate(0f, 0f, speed * Time.deltaTime);
		
	}
}
