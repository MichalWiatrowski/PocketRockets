using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NextGate : MonoBehaviour {

    public Text nextGate;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        // update score to screen
        nextGate.text = "Next Gate: " + networkClientUIbuttons.networkClient.getNextGate().ToString();
    }
}
