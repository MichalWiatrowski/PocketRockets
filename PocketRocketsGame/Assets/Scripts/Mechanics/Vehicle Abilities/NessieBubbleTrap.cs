using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NessieBubbleTrap : MonoBehaviour {

    [SerializeField]
    private float holdTime = 3.0f;
    //[SerializeField]
    private float bubbleRiseSpeed = 8.0f;
    [SerializeField]
    private float maxBubbleHeight = 8.0f;
 
    private float bubbleRiseHeight = 0.0f;
    private PlayerStats stats;
    private bool bubbleCollision = false;
    private GameObject tempCar;


    // Use this for initialization
    void Start () {
        
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        bubbleRiseHeight = transform.position.y + maxBubbleHeight;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (bubbleCollision == true) {

            stats.setSwitchStateL(0);
            networkServerUIbuttons.networkServer.sendSwitchStateL();
            stats.setSwitchStateR(0);
            networkServerUIbuttons.networkServer.sendSwitchStateR();

            if (stats.getTrappedInBubble() == true)
            {
                tempCar.transform.Translate(0, bubbleRiseSpeed * Time.deltaTime, 0);

                
                //Move Car
                tempCar.GetComponent<BoxCollider>().enabled = false;
                tempCar.GetComponent<Rigidbody>().useGravity = false;
               // tempCar.transform.Translate(0, bubbleRiseSpeed * Time.deltaTime, 0);
                stats.setInTheAir(true);
                //Move Bubble
                transform.Translate(0, (bubbleRiseSpeed * Time.deltaTime), 0);

                if (tempCar.transform.position.y >= maxBubbleHeight)
                {
                    stats.setSwitchStateL(1);
                    networkServerUIbuttons.networkServer.sendSwitchStateL();
                    stats.setSwitchStateR(1);
                    networkServerUIbuttons.networkServer.sendSwitchStateR();

                    stats.setTrappedInBubble(false);
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
            stats = collided.GetComponent<PlayerStats>();
            if (bubbleCollision == false && !stats.getImmune())
            {
                GetComponent<MeshRenderer>().enabled = true;
                bubbleCollision = true;
                tempCar = collided.gameObject;
                Debug.Log("Collided with Bubble");
                stats.setTrappedInBubble(true);
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
