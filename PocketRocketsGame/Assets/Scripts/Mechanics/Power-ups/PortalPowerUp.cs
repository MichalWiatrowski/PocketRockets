using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPowerUp : MonoBehaviour {


    public float teleportDistance = 25.0f;

    private float fallingSpeed = -0.35f;
    private float fallingDistance = 2.0f;
    private PlayerStats stats;
    private Transform transf;
    private bool isPortalActive = false;
    private Vector3 trackValues;
    private float fallDistanceUnderTrack = 0.0f;

    public GameObject exitPortal;
    public GameObject entrancePortal;

    private GameObject exitInstance;
    private GameObject entranceInstance;

    private Vector3 PortalEntrancePosition;
    private Vector3 PortalExitPosition;

    void Start()
    {
        stats = GetComponentInParent<PlayerStats>();
        transf = GetComponentInParent<Transform>();
        GameObject tempPlane = GameObject.Find("Plane");
        trackValues = new Vector3(tempPlane.transform.position.x, tempPlane.transform.position.y, tempPlane.transform.position.z);

    }

   public void CreatePortals()
    {
        if (isPortalActive == false)
        {
            isPortalActive = true;
            PortalEntrancePosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z + 5.0f);
            entranceInstance = Instantiate(entrancePortal, PortalEntrancePosition, transform.rotation) as GameObject;

            PortalExitPosition = new Vector3(PortalEntrancePosition.x, PortalEntrancePosition.y + 8.0f, PortalEntrancePosition.z + teleportDistance);
            exitInstance = Instantiate(exitPortal, PortalExitPosition, transform.rotation) as GameObject;
            exitInstance.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    //TODO 
    //Create Portal when touch screen button is pressed on Phone.

    void OnTriggerEnter(Collider collided)
    {
        if (collided.CompareTag("Portal"))
        {
            Debug.Log("Collided with portal");
            stats.fallingThroughTeleport = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isPortalActive == false)
        {
            ManageInput();
        }
        
        fallDistanceUnderTrack = trackValues.y - fallingDistance;

        if (stats.fallingThroughTeleport == true)
        {
            transform.Translate(0, fallingSpeed * Time.deltaTime, 0);
            //transform.Translate(0, fallingSpeed * Time.deltaTime, 0);
           // GetComponentInChildren<BoxCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            if (transform.position.y <= fallDistanceUnderTrack)
            {

                // transform.Translate(0, 8 + fallingDistance, teleportDistance);
                // GetComponentsInChildren<Transform>.transform.Translate(0, 8 + fallingDistance, teleportDistance);
                transform.parent.gameObject.transform.Translate(0, 0, 25); //good parent child relation
               // transf.transform.Translate(0, 0, 25);
                transform.Translate(0, 8 + fallingDistance, 0);
                stats.fallingThroughTeleport = false;

           
                //GetComponentInChildren<BoxCollider>().enabled = true;
                GetComponent<BoxCollider>().enabled = true;
                StartCoroutine(PortalCleanup());
            }
        }

    }

    IEnumerator PortalCleanup()
    {
        yield return new WaitForSeconds(2.0f);

        //entranceInstance.GetComponent<MeshRenderer>().enabled = false;
       // entranceInstance.GetComponent<CapsuleCollider>().enabled = false;

        ///exitInstance.GetComponent<MeshRenderer>().enabled = false;
        //exitInstance.GetComponent<CapsuleCollider>().enabled = true;

        Debug.Log("Destroying objects");

        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");

        //foreach (GameObject portal in portals)
        //    GameObject.Destroy(portal);
        //Destroy(GameObject.FindGameObjectsWithTag("Portal"));




        DestroyImmediate(entranceInstance, true);
        DestroyImmediate(exitInstance, true);
        isPortalActive = false;
    }

    void ManageInput() {

        if (Input.GetButtonDown("Fire1"))
        {
            PortalPowerUp power = GameObject.Find("Player 1").GetComponent<PortalPowerUp>();
            power.CreatePortals();
            //CreatePortals();
        }

    }
}
