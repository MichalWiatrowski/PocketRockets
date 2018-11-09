using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarValues : MonoBehaviour {

    public bool immune = false;
    public bool fallingThroughTeleport = false;

    public bool getFallingThroughTeleport() {

        return fallingThroughTeleport;
    }
}
