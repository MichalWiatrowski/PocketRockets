using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NessieBubble : MonoBehaviour {

    public float holdTime = 3.0f;
    public float bubbleRiseSpeed = 2.0f;
    private float maxBubbleHeight = 8.0f;
    private float bubbleRiseHeight = 0.0f;
    private PlayerStats stats;
    private Vector3 trackValues;

    public GameObject bubble;
    private GameObject bubbleInstance;
    private Vector3 BubblePosition;

    private GameObject tempGate;
    private Vector3 gateValues;

    // Use this for initialization
    void Start () {

        stats = GetComponentInParent<PlayerStats>();
        GameObject tempPlane = GameObject.Find("Plane");
        trackValues = new Vector3(tempPlane.transform.position.x, tempPlane.transform.position.y, tempPlane.transform.position.z);
        
    }

    public void CreateBubble(GameObject gate, int playerTarget) {

        tempGate = gate;
        gateValues = new Vector3(tempGate.transform.position.x, tempGate.transform.position.y, tempGate.transform.position.z);
        GameObject tempTarget = GameObject.Find("Player " + playerTarget);
        BubblePosition = new Vector3(tempTarget.transform.position.x, trackValues.y + 1.40f, gate.transform.position.z);

        bubbleInstance = Instantiate(bubble, BubblePosition, transform.rotation) as GameObject;
    }

    void OnTriggerEnter(Collider collided)
    {
        if (collided.CompareTag("Bubble"))
        {
            Debug.Log("Collided with Bubble");
            stats.trappedInBubble = true;
            
        }
         if (collided.CompareTag("Plane"))
        {
            Debug.Log("Collided with Plane");

            stats.inTheAir = false;
        }

    }
   
    // Update is called once per frame
    void Update () {
        ManageInput();

        bubbleRiseHeight = trackValues.y + maxBubbleHeight;

        if (stats.trappedInBubble == true) {

            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            transform.Translate(0, bubbleRiseSpeed * Time.deltaTime, 0);
            //bubbleInstance.GetComponent<Transform>().transform.Translate(0, bubbleRiseSpeed * Time.deltaTime, 0);
            bubbleInstance.transform.Translate(0, bubbleRiseSpeed * Time.deltaTime, 0);
            stats.inTheAir = true;

            if (transform.position.y >= maxBubbleHeight) {

                stats.trappedInBubble = false;
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<Rigidbody>().useGravity = true;
                bubbleInstance.GetComponent<MeshRenderer>().enabled = false;
                bubbleInstance.GetComponent<SphereCollider>().enabled = false;
                DestroyImmediate(bubbleInstance, true);
                
            }
        }

	}

    void ManageInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //CreatePortals();
            

            if (this == GameObject.Find("Player " + 1)) {
                GameObject tempTarget = GameObject.Find("Player " + 1);
                tempTarget.GetComponentInChildren<NessieBubble>().CreateBubble(GameObject.Find("Gate1"), 2);

            }
        }
    }
}
