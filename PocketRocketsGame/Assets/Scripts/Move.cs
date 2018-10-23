using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float speed = 4f;

	// Update is called once per frame
	void Update () {

        // movement
        transform.Translate(0f, 0f, speed * Time.deltaTime);
		
	}
}
