using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPowerUp : MonoBehaviour {


    private float teleportDistance = 40;
    public AudioClip teleportClip;

    private float fallingSpeed = -0.35f;
    private float fallingDistance = 2.0f;
    private PlayerStats stats;
    private Transform transf;
    private bool isPortalActive = false;
    private Vector3 trackValues;
    private float fallDistanceUnderTrack = 0.0f;
    private AudioSource portalSource;
    public GameObject exitPortal;
    public GameObject entrancePortal;

    private GameObject exitInstance;
    private GameObject entranceInstance;

    private Vector3 PortalEntrancePosition;
    private Vector3 PortalExitPosition;

    void Awake() {

       // portalSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        transf = GetComponent<Transform>();
        GameObject tempPlane = GameObject.Find("Plane");
        trackValues = new Vector3(tempPlane.transform.position.x, tempPlane.transform.position.y, tempPlane.transform.position.z);

    }
    
   public void CreatePortals()
    {
        if (isPortalActive == false)
        {
            isPortalActive = true;
            PortalEntrancePosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z + 5.0f);
            entranceInstance = (GameObject)Instantiate(entrancePortal, PortalEntrancePosition, transform.rotation);

            PortalExitPosition = new Vector3(PortalEntrancePosition.x, PortalEntrancePosition.y + 8.0f, PortalEntrancePosition.z + teleportDistance);
            exitInstance = (GameObject)Instantiate(exitPortal, PortalExitPosition, transform.rotation);
            exitInstance.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider collided)
    {
        if (collided.CompareTag("Portal"))
        {
            //portalSource.PlayOneShot(teleportClip);
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
           

            GetComponent<BoxCollider>().enabled = false;
            if (transform.position.y <= fallDistanceUnderTrack)
            {

                
               // transform.parent.gameObject.transform.Translate(0, 0, 25); //good parent child relation
              


                transform.Translate(0, 8 + fallingDistance, teleportDistance);
                stats.fallingThroughTeleport = false;

           
                GetComponent<BoxCollider>().enabled = true;
                StartCoroutine(PortalCleanup());
            }
        }

    }

    IEnumerator PortalCleanup()
    {
        yield return new WaitForSeconds(2.0f);

        

        isPortalActive = false;
        Debug.Log("Destroying objects and isPortalActive is: " + isPortalActive);

        Destroy(entranceInstance, 1.0f);
        Destroy(exitInstance, 1.0f);


    }

   public void ManageInput() {

        if (Input.GetButtonDown("Fire1"))
        {
            CreatePortals();
        }
    }
}
