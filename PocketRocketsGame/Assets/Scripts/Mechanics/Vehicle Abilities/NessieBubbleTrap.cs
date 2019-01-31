using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NessieBubbleTrap : MonoBehaviour {

    [SerializeField]
    private float holdTime = 3.0f;
    [SerializeField]
    private float bubbleRiseSpeed = 2.0f;
    [SerializeField]
    private float maxBubbleHeight = 8.0f;
 
    private float bubbleRiseHeight = 0.0f;
    private PlayerStats stats;
    private bool bubbleCollision = false;
    private GameObject tempCar;


    // Use this for initialization
    void Start () {
        
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
        bubbleRiseHeight = transform.position.y + maxBubbleHeight;
    }
	
	// Update is called once per frame
	void Update () {

        if (bubbleCollision == true) {
            if (stats.trappedInBubble == true)
            {
                tempCar.transform.Translate(0, bubbleRiseSpeed * Time.deltaTime, 0);

                
                //Move Car
                tempCar.GetComponent<BoxCollider>().enabled = false;
                tempCar.GetComponent<Rigidbody>().useGravity = false;
               // tempCar.transform.Translate(0, bubbleRiseSpeed * Time.deltaTime, 0);
                stats.inTheAir = true;
                //Move Bubble
                transform.Translate(0, (bubbleRiseSpeed * Time.deltaTime), 0);

                if (tempCar.transform.position.y >= maxBubbleHeight)
                {
                    stats.trappedInBubble = false;
                    tempCar.GetComponent<BoxCollider>().enabled = true;
                    tempCar.GetComponent<Rigidbody>().useGravity = true;

                    GetComponent<MeshRenderer>().enabled = false;
                    GetComponent<SphereCollider>().enabled = false;
                    bubbleCollision = false;

                }
            }
        }
       

    }

    void OnTriggerEnter(Collider collided)
    {
        if (collided.CompareTag("Player"))
        {
            if (bubbleCollision == false)
            {
                bubbleCollision = true;
                tempCar = collided.gameObject;
                stats = collided.GetComponent<PlayerStats>();
                Debug.Log("Collided with Bubble");
                stats.trappedInBubble = true;
            }
        }
        //if (collided.CompareTag("Plane"))
        //{
        //    Debug.Log("Collided with Plane");

        //    stats.inTheAir = false;
        //    //tempCar.GetComponentInParent<Rigidbody>().useGravity = false;
        //    tempCar.transform.parent.gameObject.GetComponent<Rigidbody>().useGravity = false;
        //}

    }
}
