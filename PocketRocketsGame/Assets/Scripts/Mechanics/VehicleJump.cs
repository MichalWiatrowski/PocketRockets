using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleJump : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private float thrust = 15;
    private Rigidbody rb;
    private PlayerStats stats;

    void Start () {

        //stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        ManageInput();
	}

    public void Jump() {

        
        rb.AddForce(0.0f, thrust, 0.0f, ForceMode.Impulse);
    }


    public void ManageInput()
    {

        if (Input.GetKeyDown("space"))
        {
           
                Jump();
                print("Input Recieved");
            
        }
    }
}
