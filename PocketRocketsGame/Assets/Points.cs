using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Points : MonoBehaviour {

    public Text points;


	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        // update score to screen
        points.text = networkClientUIbuttons.networkClient.GetPoints().ToString();
    }
}
