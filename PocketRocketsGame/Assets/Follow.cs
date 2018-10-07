using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public List<Transform> cars;

    public float smoothing;
    public float minZoom;
    public float maxZoom;
    public float zoomLimit;

    public Vector3 offset;

    private Vector3 velocity;
    private Camera cam;

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();

        // Camera smoothing value
        smoothing = 0.2f;

        // FOV settings
        minZoom = 40f;
        maxZoom = 15f;
        zoomLimit = 50;
        offset = new Vector3(30, 7, -9);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        GetEncapsulatingBounds();
        MoveCam();
        Zoom();
	}

    void MoveCam()
    {
        // find centre of the bouding box
        Vector3 camCenter = GetEncapsulatingBounds().center;

        // find the new camera position with the offset
        Vector3 newPos = camCenter + offset;

        // move camera to new position
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothing);
    }

    void Zoom()
    {
        // calculate the optimal FOV based on distance of vehicles then set it
        float activeZoom = Mathf.Lerp(maxZoom, minZoom, GetEncapsulatingBounds().size.z / zoomLimit);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, activeZoom, Time.deltaTime);
    }

    Bounds GetEncapsulatingBounds()
    {
        // creating the bounding box for the players
        var boundsBox = new Bounds(cars[0].position, Vector3.zero);

        for (int x = 0; x < cars.Count; x++)
        {
            boundsBox.Encapsulate(cars[x].position);
        }
        return boundsBox;
    }
}
