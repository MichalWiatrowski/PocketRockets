using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSwitch : MonoBehaviour {

    private PlayerStats stats;

    private void Start()
    {
        stats = gameObject.GetComponentInParent<PlayerStats>();
    }

    void OnTriggerEnter(Collider box)
    {
        if (box.CompareTag("Car"))
        {
            stats.switchLeft = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        stats.switchLeft = true;    
    }
}
