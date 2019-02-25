using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelRotation : MonoBehaviour {

    private float rotationSpeed = 15.0f;
	// Use this for initialization
	void Start () {
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
	}
}
