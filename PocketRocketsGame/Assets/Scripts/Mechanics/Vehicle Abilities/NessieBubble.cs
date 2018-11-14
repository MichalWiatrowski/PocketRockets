using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NessieBubble : MonoBehaviour {

    public float holdTime = 3.0f;
    private PlayerStats stats;
    private Vector3 trackValues;

    public GameObject Bubble;
    private GameObject exitInstance;
    private Vector3 BubblePosition;

    private GameObject tempGate;
    private Vector3 gateValues;

    // Use this for initialization
    void Start () {

        stats = GetComponent<PlayerStats>();
        GameObject tempPlane = GameObject.Find("Plane");
        trackValues = new Vector3(tempPlane.transform.position.x, tempPlane.transform.position.y, tempPlane.transform.position.z);

    }

    public void CreateBubble(GameObject gate) {

        tempGate = gate;
        gateValues = new Vector3(tempGate.transform.position.x, tempGate.transform.position.y, tempGate.transform.position.z);
        //BubblePosition = new Vector3()

    }

    void OnTriggerEnter(Collider collided)
    {
        if (collided.CompareTag("Bubble"))
        {
            Debug.Log("Collided with Bubble");
            
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}
