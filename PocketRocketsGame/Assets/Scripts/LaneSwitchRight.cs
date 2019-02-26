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
            stats.setSwitchStateR(0);
            networkServerUIbuttons.networkServer.sendSwitchStateR();
        }
    }

    void OnTriggerExit(Collider box)
    {
        if (box.CompareTag("Player"))
        {
            stats.setSwitchRight(true);
            stats.setSwitchStateR(1);
            networkServerUIbuttons.networkServer.sendSwitchStateR();
        }
    }
}
