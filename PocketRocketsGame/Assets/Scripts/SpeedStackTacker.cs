using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpeedStackTacker : MonoBehaviour {

    public Text SpeedStack;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        SpeedStack.text = "Stack : " + networkClientUIbuttons.networkClient.getSpeedStack().ToString();
        
    }
}
