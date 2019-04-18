using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Name : MonoBehaviour {

    public GameObject player;
    public Text name;

    private void Start()
    {
    }

    void Update()
    {
        name.text = player.GetComponent<PlayerStats>().getPlayerName();
    }
}
