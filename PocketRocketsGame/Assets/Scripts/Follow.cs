﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public List<Transform> cars;

    public float smoothing = 0.2f;
    public float minZoom = 150f;
    public float maxZoom = 50f;
    public float zoomLimit = 50;
    public enum camNumber {Back, BackZoom, Side, SideZoom};
    public camNumber camNum = camNumber.Back;

    public bool behindCam;

    public Vector3 normalOffset = new Vector3(5, 6, -6);
    public Vector3 behindOffset = new Vector3(0, 3, -6);
    public Vector3 normalRotation = new Vector3(20, 60, 0);
    public Vector3 behindRotation = new Vector3(0, 0, 0);

    private Vector3 newPos;
    private Vector3 velocity;
    private Camera cam;

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();

        behindCam = false;
	}

	// Update is called once per frame
	void LateUpdate () {
        switch (camNum)
        {
            case camNumber.Back :
                behindCam = true;
                Back();
                break;
            case camNumber.BackZoom :
                behindCam = true;
                BackZoom();
                break;
            case camNumber.Side :
                behindCam = false;
                Side();
                break;
            case camNumber.SideZoom :
                behindCam = false;
                SideZoom();
                break;
        }
        GetEncapsulatingBounds();
        MoveCam();
        Zoom();
	}

    void MoveCam()
    {
        // find centre of the bouding box
        Vector3 camCenter = GetEncapsulatingBounds().center;

        // find the new camera position with the offset
        if (behindCam)
        {
            newPos = camCenter + behindOffset;
            transform.rotation = Quaternion.Euler(behindRotation);
        }
        else
        {
            newPos = camCenter + normalOffset;
            transform.rotation = Quaternion.Euler(normalRotation);
        }

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

    void Back()
    {
        GetEncapsulatingBounds();
        MoveCam();
    }

    void BackZoom()
    {
        GetEncapsulatingBounds();
        MoveCam();
        Zoom();
    }

    void Side()
    {
        GetEncapsulatingBounds();
        MoveCam();
    }

    void SideZoom()
    {
        GetEncapsulatingBounds();
        MoveCam();
        Zoom();
    }
}
