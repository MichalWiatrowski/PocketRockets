using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSwitchRight : MonoBehaviour {

    private PlayerStats stats;

    private void Start()
    {
        stats = gameObject.GetComponentInParent<PlayerStats>();
    }

    void OnTriggerEnter(Collider box)
    {
        if (box.CompareTag("Player"))
        {
            stats.setSwitchRight(false);
        }
    }

    void OnTriggerExit(Collider box)
    {
        if (box.CompareTag("Player"))
        {
            stats.setSwitchRight(true);
        }
    }
}
