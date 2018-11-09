using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float speed = 4f;
    private CarValues values;
    void Start()
    {
        values = gameObject.GetComponent<CarValues>();

    }
    // Update is called once per frame
    void Update () {

        // movement
        if (values.fallingThroughTeleport == false)
        {
            transform.Translate(0f, 0f, speed * Time.deltaTime);
        }
	}
}
